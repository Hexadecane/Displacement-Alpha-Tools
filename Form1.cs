using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplacementAlphaTools {

    public struct DispInfo {
        // This is used to store info pertaining to a particular displacement. 
        // Its coordinates, and the rows of alpha painting information.
        public int _x;
        public int _y;
        public int _width;
        public int _height;
        public List<string> _rows;
    }

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            // Other initialization stuff:

            PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            PreviewPictureBox.BackColor = Color.FromArgb(255, 127, 127, 127);
            // These are disabled when nothing is loaded:
            SaveButton.Enabled = false;
            ResizeImageCheckbox.Enabled = false;
        }

        public static Bitmap ImageData { get; set; }
        public static Bitmap ImageDataOriginal { get; set; }
        public static string VMFData { get; set; }
        // True = image, false = VMF.
        public static bool OpenedFileIsImage;


        // A pow 3 displacement holds 9x9 vertices of color information. We should try to fit the entire image
        // into a value less than or equal to the number of required displacements rounded up.

        // Since displacements share an edge (e.g. (9, 0) will be shared by both the first and second brush!),
        // the required length will be...

        // A 09x09 image = 1x1 displacement(s).
        // A 17x17 image = 2x2 displacement(s).
        // A 25x25 image = 3x3 displacement(s).
        // A 33x33 image = 4x4 displacement(s).
        // A 41x41 image = 5x5 displacement(s).

        // Starting from an index of one, the pattern is simply:
        // size = 8(n) + 1
        // Starting from an index of zero, the pattern is simply:
        // size = 8(n+1) + 1

        // Extending this to other displacement powers, the pattern is just:
        // p = 2^displacementPower + 1  (for the number of sides)
        // size = (2^displacementPower)*(n) + 1  (for the resulting image size)

        static int xRequired;
        static int yRequired;
        static int imgWidth;
        static int imgHeight;

        // sRGB luminance(Y) values
        public const double rY = 0.212655;
        public const double gY = 0.715158;
        public const double bY = 0.072187;


        // Inverse of sRGB "gamma" function. (approx 2.2)
        static double InverseGamma_sRGB(int ic) {
            double c = ic / 255.0;
            if (c <= 0.04045) {
                return c / 12.92;
            }
            else {
                return Math.Pow(((c + 0.055) / (1.055)), 2.4);
            }
        }


        // sRGB "gamma" function (approx 2.2)
        static int Gamma_sRGB(double v) {
            if (v <= 0.0031308) {
                v *= 12.92;
            }
            else {
                v = 1.055 * Math.Pow(v, 1.0 / 2.4) - 0.055;
            }
            return Convert.ToInt32(v * 255 + 0.5);
        }


        // GRAY VALUE (luminance)
        static int CalculatePixelLuminance(Color c) {
            return Gamma_sRGB(
                rY * InverseGamma_sRGB(c.R) +
                gY * InverseGamma_sRGB(c.G) +
                bY * InverseGamma_sRGB(c.B)
            );
        }


        // Credits to user "mpen" of Stack Overflow for this function.
        // March 2021,
        // https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        // https://stackoverflow.com/users/65387/mpen
        private static Bitmap ResizeImage(Image image, int width, int height) {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        static string SidePlaneArrayToString(int[,] SP, int x, int y) {
            string res = "";
            for (int i = 0; i < 3; i++) {
                res += "(";
                for (int j = 0; j < 3; j++) {
                    int val = SP[i, j];
                    if (j == 0) {
                        val += x;
                    }
                    else if (j == 1) {
                        val -= y;
                    }
                    res += Convert.ToString(val);
                    if (j < 2) {
                        res += " ";
                    }
                }
                res += ")";
                if (i < 2) {
                    res += " ";
                }
            }

            return res;
        }


        static void GetMinCoordForPlane(Match planeCoords, ref int minX, ref int minY) {
            for (int cn = 1; cn < 4; cn++) {
                string[] cnCurr = planeCoords.Groups[cn].ToString().Split(' ');
                int currX = Convert.ToInt32(cnCurr[0]);
                int currY = Convert.ToInt32(cnCurr[1]);
                if (minX == int.MaxValue || Convert.ToInt32(cnCurr[0]) < minX) {
                    minX = currX;
                }
                if (minY == int.MaxValue || Convert.ToInt32(cnCurr[1]) < minY) {
                    minY = currY;
                }
            }
            Console.WriteLine("---\nMinX and MinY for this plane are:");
            Console.WriteLine(minX);
            Console.WriteLine(minY);
            Console.WriteLine("---\n");
        }

        
        static int GetPlaneWidth(Match planeCoords) {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            //Console.WriteLine("---");
            for (int cn = 1; cn < 4; cn++) {
                string[] cnCurr = planeCoords.Groups[cn].ToString().Split(' ');
                int currX = Convert.ToInt32(cnCurr[0]);
                if (minX == int.MaxValue || Convert.ToInt32(cnCurr[0]) < minX) {
                    minX = currX;
                }
                if (maxX == int.MinValue || Convert.ToInt32(cnCurr[0]) > maxX) {
                    maxX = currX;
                }
            }
            //Console.WriteLine(Math.Abs(maxX - minX));
            //Console.WriteLine("---\n");
            return Math.Abs(maxX - minX);
        }


        static Image CreatePreviewImage(Image img) {
            int finalWidth = 8 * xRequired + 1;
            int finalHeight = 8 * yRequired + 1;

            Bitmap imgTemp = new Bitmap(img);
            Bitmap bottomLayer = new Bitmap(finalWidth, finalHeight, PixelFormat.Format24bppRgb);

            for (int y = 0; y < imgTemp.Height; y++) {
                for (int x = 0; x < imgTemp.Width; x++) {
                    Color oldPixel = imgTemp.GetPixel(x, y);
                    int lum = CalculatePixelLuminance(oldPixel);
                    Color newPixel = Color.FromArgb(oldPixel.A, lum, lum, lum);
                    imgTemp.SetPixel(x, y, newPixel);
                }
            }

            Bitmap finalImage = new Bitmap(finalWidth, finalHeight, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(finalImage)) {
                graphics.CompositingMode = CompositingMode.SourceOver;

                graphics.DrawImage(bottomLayer, 0, 0);
                graphics.DrawImage(imgTemp, 0, 0);
            }

            return finalImage;
        }



        private void LoadImageToolStripMenuItem_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd1 = new OpenFileDialog()) {
                ofd1.Title = "Open Picture";
                ofd1.Filter = "Image Files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp";

                if (ofd1.ShowDialog() == DialogResult.OK) {
                    SaveButton.Text = "Save as .vmf";
                    SaveButton.Enabled = true;
                    ResizeImageCheckbox.Enabled = true;
                    ResizeImageCheckbox.Checked = false;
                    PreviewLabel.Text = "- Preview (Image):";
                    OpenedFileIsImage = true;

                    Image tempImg = Image.FromFile(ofd1.FileName);
                    // These values need to be assigned before the preview image and ImageData can proceed.
                    imgWidth = tempImg.Width;
                    imgHeight = tempImg.Height;
                    // Inverse of 8n + 1 is (n - 1) / 8.
                    xRequired = (imgWidth < 9) ? 1 : Convert.ToInt32(Math.Ceiling(Convert.ToDouble(imgWidth - 1) / 8.0));
                    yRequired = (imgHeight < 9) ? 1 : Convert.ToInt32(Math.Ceiling(Convert.ToDouble(imgHeight - 1) / 8.0));

                    Console.WriteLine($"Image size is {imgWidth}x{imgHeight}");
                    Console.WriteLine($"xRequired: {xRequired}");
                    Console.WriteLine($"yRequired: {yRequired}");
                    Console.WriteLine($"Required alpha painting size is {8*xRequired + 1}x{8*yRequired + 1}");

                    PreviewPictureBox.Image = CreatePreviewImage(tempImg);
                    ImageData = (Bitmap)PreviewPictureBox.Image;
                    ImageDataOriginal = (Bitmap)tempImg;
                }
            }
        }


        private void LoadVMFToolStripMenuItem_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd1 = new OpenFileDialog()) {
                ofd1.Title = "Open Valve Map File";
                ofd1.Filter = "Valve Map File (*.vmf)|*.vmf";

                if (ofd1.ShowDialog() == DialogResult.OK) {
                    SaveButton.Text = "Save as image";
                    SaveButton.Enabled = true;
                    ResizeImageCheckbox.Enabled = false;
                    ResizeImageCheckbox.Checked = false;
                    PreviewLabel.Text = "- Preview (VMF):";

                    OpenedFileIsImage = false;

                    VMFData = File.ReadAllText(ofd1.FileName);

                    // Where the data for each respective displacement will be stored...
                    List<DispInfo> displacements = new List<DispInfo>();

                    Regex sideStartPattern = new Regex(@"side\s+({)");
                    List<string> displacementSides = new List<string>();
                    MatchCollection displacementSideStartMatches = sideStartPattern.Matches(VMFData);
                    for (int i = 0; i < displacementSideStartMatches.Count; i++) {
                        // Get the index of the match and fetch everything from its starting brace to its closing brace.
                        int start = displacementSideStartMatches[i].Groups[1].Index;
                        int end;
                        int braceCount = 1;
                        int currIndex = start;
                        //Console.WriteLine($"Starting index of brace: {start}, character at index is: {VMFData[start]}");
                        while (braceCount > 0) {
                            char currChar = VMFData[++currIndex];
                            if (currChar == '{') {
                                braceCount++;
                            }
                            else if (currChar == '}') {
                                braceCount--;
                            }
                        }
                        end = currIndex;
                        string sideString = VMFData.Substring(start, end - start + 1);
                        if (sideString.IndexOf("dispinfo") != -1) {
                            //Console.WriteLine("---\n" + sideString + "\n---");
                            displacementSides.Add(sideString);
                        }
                    }
                    Console.WriteLine($"Total displacement count detected is {displacementSides.Count}");

                    // Matching the alpha arrays of the dispinfo section in a .vmf file...
                    Regex alphaPattern = new Regex(@"alphas\s*\{([a-z0-9.""\s]*)\}");
                    // Matching the rows of a given displacement's alpha array...
                    Regex rowContentsPattern = new Regex(@"""row\d""\s""([\d.\s]+)""");
                    // Matching the plane coordinates of the side...
                    Regex coordinatesPattern = new Regex(@"""plane""\s""\(([\d\D]+)\)\s\(([\d\D]+)\)\s\(([\d\D]+)\)""");

                    for (int i = 0; i < displacementSides.Count; i++) {
                        // Get the current alpha array in the given side.
                        string dsmStr = displacementSides[i];

                        string currAlphaArray = alphaPattern.Match(dsmStr).Groups[1].ToString();
                        // Get the contents of the rows in the alpha array as discrete regex matches.
                        MatchCollection currRowContents = rowContentsPattern.Matches(currAlphaArray);

                        List<string> currChunk = new List<string>();
                        // Iterate over them and populate a list with their contents.
                        for (int j = 0; j < currRowContents.Count; j++) {
                            currChunk.Add(currRowContents[j].Groups[1].ToString());
                        }
                        // Get the plane coordinates to extract an X and Y value.
                        Match planeCoords = coordinatesPattern.Match(dsmStr);
                        // The minimum value of the X and Y of these three XYZ coordinates is what's needed. It'll serve as the top left corner.
                        int minX = int.MaxValue;
                        int minY = int.MaxValue;
                        GetMinCoordForPlane(planeCoords, ref minX, ref minY);
                        int wh = GetPlaneWidth(planeCoords);  // For now, lets assume the width and height are the same.
                        DispInfo d = new DispInfo {
                            _rows = currChunk,
                            _x = minX,
                            _y = minY,
                            _width = wh,
                            _height = wh,
                        };
                        displacements.Add(d);
                    }

                    // Generate preview image...

                    // We need to convert the information contained within each displacement into a 9x9 chunk of pixels! (assuming a pow 3 displacement)

                    //DispInfo currDisp = displacements[0];
                    //Console.WriteLine(currDisp._rows[0]);

                    int[] posMin = new int[2] { int.MaxValue, int.MaxValue };
                    int[] posMax = new int[2] { int.MinValue, int.MinValue };
                    foreach (var i in displacements) {
                        // Mins:
                        if (i._x < posMin[0]) {
                            posMin[0] = i._x;
                        }
                        if (i._y < posMin[1]) {
                            posMin[1] = i._y;
                        }
                        // Maxes:
                        if (i._x > posMax[0]) {
                            posMax[0] = i._x;
                        }
                        if (i._y > posMax[1]) {
                            posMax[1] = i._y;
                        }
                    }
                    // For now, lets assume the width and height of all the displacements is the same!
                    Console.WriteLine("---\nSpan in hammer units is:");
                    Console.WriteLine(Math.Abs(posMax[0] - posMin[0]));
                    Console.WriteLine(Math.Abs(posMax[1] - posMin[1]));
                    xRequired = Math.Abs(posMax[0] - posMin[0]) / displacements[0]._width + 1;
                    yRequired = Math.Abs(posMax[1] - posMin[1]) / displacements[0]._height + 1;
                    Console.WriteLine("---\nRequired sizes are:");
                    Console.WriteLine(xRequired);
                    Console.WriteLine(yRequired);

                    // 8(n) + 1
                    imgWidth = 8 * (xRequired) + 1;
                    imgHeight = 8 * (yRequired) + 1;
                    Console.WriteLine("---\nFinal image size:");
                    Console.WriteLine(imgWidth);
                    Console.WriteLine(imgHeight);

                    // We need this for getting the respective X and Y "tile" position of the displacement.
                    // Increments every time it advances a row/col.
                    int posX = 0;
                    int posY = 0;

                    Bitmap tempImage = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    Console.WriteLine($"Number of displacements is {displacements.Count}");

                    foreach (DispInfo d in displacements) {

                        // Figure out the position of the given displacement.
                        posX = Math.Abs(posMin[0] - d._x) / d._width;
                        posY = yRequired - Math.Abs(posMin[1] - d._y) / d._width - 1;

                        for (int y = 0; y < 9; y++) {
                            for (int x = 0; x < 9; x++) {
                                string[] vals = d._rows[y].Split(' ');
                                int cl = Convert.ToInt32(Convert.ToDouble(vals[x]));  // Current luminance = cl.
                                // Since an edge is shared, subtract posX/posY's value after adding it multiplied by the displacement's vertex width.
                                tempImage.SetPixel(x + posX*9 - posX, (8 - y) + posY*9 - posY, Color.FromArgb(255, cl, cl, cl));
                            }
                        }
                    }

                    PreviewPictureBox.Image = CreatePreviewImage(tempImage);
                }
            }
        }


        private void SaveButton_Click(object sender, EventArgs e) {

            if (OpenedFileIsImage) {
                string sOutput = "";
                Random rng = new Random();
                int solidPosX = 0;
                int solidPosY = 0;
                int solidID = 1;

                // Get the boilerplate for the beginning:
                sOutput += File.ReadAllText("boilerplate_start.txt") + "\n";
                string boilerplateSolid = File.ReadAllText("boilerplate_solid.txt");

                int bSize = 128;  // Brush size.

                // SP = Side Plane.
                int[,] SP1 = { { 0, -bSize, bSize / 2 }, { 0, 0, bSize / 2 }, { bSize, 0, bSize / 2 } };
                int[,] SP2 = { { 0, 0, 0 }, { 0, -bSize, 0 }, { bSize, -bSize, 0 } };
                int[,] SP3 = { { 0, -bSize, 0 }, { 0, 0, 0 }, { 0, 0, bSize / 2 } };
                int[,] SP4 = { { bSize, 0, 0 }, { bSize, -bSize, 0 }, { bSize, -bSize, bSize / 2 } };
                int[,] SP5 = { { 0, 0, 0 }, { bSize, 0, 0 }, { bSize, 0, bSize / 2 } };
                int[,] SP6 = { { bSize, -bSize, 0 }, { 0, -bSize, 0 }, { 0, -bSize, bSize / 2 } };
                List<int[,]> SPList = new List<int[,]>() {
                    SP1,
                    SP2,
                    SP3,
                    SP4,
                    SP5,
                    SP6
                };

                List<string> solids = new List<string>();

                for (int y = 0; y < yRequired; y++) {
                    for (int x = 0; x < xRequired; x++) {
                        string newSolid = boilerplateSolid;
                        newSolid = newSolid
                            .Replace("$ID", Convert.ToString(solidID))
                            .Replace("$COLOR", $"{rng.Next(96, 256)} {rng.Next(96, 256)} {rng.Next(96, 256)}")
                            .Replace("$START_POS", $"{x * bSize} {-bSize * (y + 1)} 0");

                        for (int i = 0; i < SPList.Count; i++) {
                            newSolid = newSolid.Replace($"$PLANE_{i + 1}", SidePlaneArrayToString(SPList[i], x * bSize, y * bSize));
                            newSolid = newSolid.Replace($"$SIDEID_{i + 1}", Convert.ToString(solidID));
                            solidID++;
                        }

                        solidID++;
                        solids.Add(newSolid);
                    }
                }

                for (int n = 0; n < solids.Count; n++) {
                    int col = n % xRequired;
                    int row = n / xRequired;  // What row the given n value would put us on.
                    string alphaPaintData = "";
                    // Iterate over the Y coord values of a displacement brush:
                    for (int y = 0; y < 9; y++) {
                        alphaPaintData += $"\t\t\t\t\t\"row{y}\" \"";
                        // Iterate over the X coord values of a displacement brush:
                        for (int x = 0; x < 9; x++) {
                            int actualX = x + col * 9 - col;
                            // Y should be inverted or the displacement chunks for a given row will be upside down!
                            int actualY = (8 - y) + row * 9 - row;
                            int alphaValue = 0;
                            if (actualX < ImageData.Width && actualY < ImageData.Height) {
                                Color currPixel = ImageData.GetPixel(actualX, actualY);
                                alphaValue = CalculatePixelLuminance(currPixel) * currPixel.A / 255;
                            }
                            alphaPaintData += Convert.ToString(alphaValue) + " ";
                        }
                        alphaPaintData += "\"\n";
                    }
                    solids[n] = solids[n].Replace("$ALPHAS", alphaPaintData);
                    sOutput += solids[n] + "\n";
                }

                // Write ending boilerplate to newContents:
                sOutput += File.ReadAllText("boilerplate_end.txt");

                // Save to a file:
                /*using (StreamWriter sw = new StreamWriter("output.vmf")) {
                    sw.Write(sOutput);
                }*/

                SaveFileDialog sfd1 = new SaveFileDialog();
                sfd1.Filter = "Valve Map File|*.vmf";
                sfd1.Title = "Save as .vmf";
                sfd1.ShowDialog();

                if (sfd1.FileName != "") {
                    using (StreamWriter sw = new StreamWriter(sfd1.FileName)) {
                        sw.Write(sOutput);
                    }
                }
            }
            else {
                SaveFileDialog sfd1 = new SaveFileDialog();
                sfd1.Filter = "PNG |*.png";
                sfd1.Title = "Save as .png";
                sfd1.ShowDialog();

                if (sfd1.FileName != "") {
                    PreviewPictureBox.Image.Save(sfd1.FileName);
                }
            }
        }

        
        private void ResizeImageCheckbox_CheckedChanged(object sender, EventArgs e) {
            if (ResizeImageCheckbox.Checked && (ImageDataOriginal.Width != ImageData.Width || ImageDataOriginal.Height != ImageData.Height)) {
                Bitmap resizedImage = ResizeImage(ImageDataOriginal, PreviewPictureBox.Image.Width, PreviewPictureBox.Image.Height);
                PreviewPictureBox.Image = CreatePreviewImage(resizedImage);
                ImageData = (Bitmap)PreviewPictureBox.Image;
            }
            else {
                PreviewPictureBox.Image = CreatePreviewImage(ImageDataOriginal);
                ImageData = (Bitmap)PreviewPictureBox.Image;
            }
        }
    }
}
