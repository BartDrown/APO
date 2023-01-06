using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using OpenCvSharp;
//using Emgu.CV.Structure;
using OpenCvSharp.Extensions;
using System.Deployment.Application;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Mat = OpenCvSharp.Mat;
using Emgu.CV.Structure;
using Moments = OpenCvSharp.Moments;
using Point = OpenCvSharp.Point;
using System.Xml.Linq;
//using Emgu.CV;
//using Emgu;
//using Emgu.CV.Structure;
//using Emgu.CV;
//using Mat = OpenCvSharp.Mat;
using System.IO;
using System.Text;


namespace APO {
    public class ImageService {
        public ImageView imageView;
        public Bitmap bitmap;

        private string name;
        private int height;
        private int width;

        public int[] lut_table = new int[256];
        public int[] luminance_rgb = new int[256];
        public int[] hist_rgb_table = new int[256];

        public int[] r_table = new int[256];
        public int[] g_table = new int[256];
        public int[] b_table = new int[256];

        private bool color = false;
        private bool grayscale = false;
        private bool binary = false;

        /** Constructor */
        public ImageService(Image image, string name = "default") {
            this.bitmap = new Bitmap(image);
            this.height = bitmap.Height;
            this.width = bitmap.Width;
            this.describeProperties();
            this.generateLookupTables();
            if(name != "default") {
                this.name = name;
            }
        }

        public ImageView Create() {
            this.imageView = new ImageView(this.bitmap);
            return imageView;
        }

        public void UpdateSelf()
        {
            this.imageView.setImage(this.bitmap);
            this.imageView.Update();
        }

        public void Update(Bitmap bitmap)
        {
            this.imageView.setImage(bitmap);
            this.imageView.Update();
        }

        public void Close()
        {
            this.imageView.Close();
        }

        public void Show() {
            this.imageView.Show();
        }

        void describeProperties() {
            if (this.checkColor()) {
                this.color = true;
                return;
            }
            if (checkBinary()) {
                this.binary = true;
                return;
            }
            this.grayscale = true;
        }

        private bool checkColor() {
            for (int i = 0; i < this.height; ++i) {
                for (int j = 0; j < this.width; ++j) {
                    byte redValue = this.bitmap.GetPixel(j, i).R;
                    byte greenValue = this.bitmap.GetPixel(j, i).G;
                    byte blueValue = this.bitmap.GetPixel(j, i).B;

                    if (!(greenValue == redValue && redValue== blueValue)) {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkBinary(){
            for (int i = 0; i < this.height; ++i){
                for (int j = 0; j < this.width; ++j){
                    byte greenValue = this.bitmap.GetPixel(j, i).G;
                    if ((greenValue != 0 && greenValue != 255))
                        return false;
                }
            }
            return true;
        }

        private void generateLookupTables() {
            if (!this.color) {
                this.generateGrayscaleLookupTable();
                return;
            }
            this.generateColorLookupTable();
            this.generateColorHistogramTable();

        }

        public void generateGrayscaleLookupTable() {
            int[] temp_table = new int[256];

            for (int i = 0; i < this.height; ++i) {
                for (int j = 0; j < this.width; ++j) {
                    byte greenValue = this.bitmap.GetPixel(j, i).G;
                    temp_table[greenValue]++;
                }
            }
            lut_table = temp_table;
        }
        public void generateColorLookupTable() {
            int[] temp_lut_table = new int[256];
            int[] temp_rgb_table = new int[256];
            for (int i = 0; i < this.height; ++i) {

                for (int j = 0; j < this.width; ++j) {
                    var pixel = this.bitmap.GetPixel(j, i);
                    byte valueRed = pixel.R;
                    byte valueGreen = pixel.G;
                    byte valueBlue = pixel.B;
                    byte temp_pixel = (byte)((valueRed + valueGreen + valueBlue) / 3);

                    temp_lut_table[valueRed]++;
                    temp_lut_table[valueGreen]++;
                    temp_lut_table[valueBlue]++;
                    r_table[valueRed]++;
                    g_table[valueGreen]++;
                    b_table[valueBlue]++;

                    temp_rgb_table[temp_pixel]++;
                }
            }
            lut_table = temp_lut_table;
            hist_rgb_table = temp_rgb_table;
        }

        public void generateColorHistogramTable() {
            int[] temp_table = new int[256];

            for (int i = 0; i < this.height; ++i) {
                for (int j = 0; j < this.width; ++j) {
                    byte redValue = this.bitmap.GetPixel(j, i).R;
                    byte greenValue = this.bitmap.GetPixel(j, i).G;
                    byte blueValue = this.bitmap.GetPixel(j, i).B;
                    byte temp_pixel = (byte)((0.3 * redValue + 0.59 * greenValue + 0.11 * blueValue));
                    temp_table[temp_pixel]++;
                }
            }
            luminance_rgb = temp_table;
        }

        public Bitmap addImage(ImageService newImage, int mode=0)
        {
            //Modes 
            // 0 - with saturation
            // 1 - without saturation
            // 2 - substract

            int[,] bitmapArray = new int[(int)newImage.width, (int)newImage.height];

            int[] temp_table = new int[256];

            Bitmap bitmap = new Bitmap((int)newImage.width, (int)newImage.height);

            Bitmap grayscale = new Bitmap((int)newImage.width, (int)newImage.height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

            for (int i = 0; i < this.height; ++i)
            {
                for (int j = 0; j < this.width; ++j)
                {
                    byte redValue = this.bitmap.GetPixel(j, i).R;
                    byte greenValue = this.bitmap.GetPixel(j, i).G;
                    byte blueValue = this.bitmap.GetPixel(j, i).B;

                    byte redValue2 = newImage.bitmap.GetPixel(j, i).R;
                    byte greenValue2 = newImage.bitmap.GetPixel(j, i).G;
                    byte blueValue2 = newImage.bitmap.GetPixel(j, i).B;

                    byte temp_pixel; 

                    if (mode == 0 || mode == 1)
                    {
                        temp_pixel = (byte)((0.3 * redValue + 0.59 * greenValue + 0.11 * blueValue) + (0.3 * redValue2 + 0.59 * greenValue2 + 0.11 * blueValue2));

                        if (mode == 1)
                        {
                            temp_pixel /= 2;
                        }

                        if (temp_pixel > 255)
                        {
                            temp_pixel = 255;
                        }
                    }
                    else
                    {
                         temp_pixel = (byte)((0.3 * redValue + 0.59 * greenValue + 0.11 * blueValue) - (0.3 * redValue2 + 0.59 * greenValue2 + 0.11 * blueValue2));

                        if (temp_pixel < 0)
                        {
                            temp_pixel = 0;
                        }

                    }

                    Color newColor = Color.FromArgb(temp_pixel , temp_pixel, temp_pixel);

                    bitmap.SetPixel(j, i, newColor);
                }
            }

            grayscale = bitmap;

            return grayscale;
        }



        public void Negate()
        {
            ImageService newImage = this;
            Bitmap grayScale = new Bitmap(newImage.bitmap.Width, newImage.bitmap.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {

                    int newColor = 255 - bitmap.GetPixel(x, y).G;
                    Color c = Color.FromArgb(newColor, newColor, newColor);
                    grayScale.SetPixel(x, y, c);
                }
            }
            this.bitmap = grayScale;

        }

        public void Treshold(int treshold, Bitmap bitmap = null)
        {
            if (bitmap == null) {
                bitmap = this.bitmap;
            }

            ImageService newImage = this;
            Bitmap grayScale = new Bitmap(newImage.bitmap.Width, newImage.bitmap.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    int grayValue = bitmap.GetPixel(x, y).G;
                    if ( grayValue < treshold)
                    {
                        Color c = Color.FromArgb(0, 0, 0);
                        grayScale.SetPixel(x, y, c);
                    }
                    else
                    {
                        Color c = Color.FromArgb(255, 255, 255);
                        grayScale.SetPixel(x, y, c);
                    }

                }
            }
            this.bitmap = grayScale;

        }

        public void TresholdEqualize(int treshold, int direction, Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                bitmap = this.bitmap;
            }

            ImageService newImage = this;
            Bitmap grayScale = new Bitmap(newImage.bitmap.Width, newImage.bitmap.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    int grayValue = bitmap.GetPixel(x, y).G;
                    if (grayValue < treshold)
                    {
                        if (direction == 1)
                        {
                            Color c = Color.FromArgb(treshold, treshold, treshold);
                            grayScale.SetPixel(x, y, c);
                        }
                    }
                    else
                    {
                        if (direction == 0)
                        {
                            Color c = Color.FromArgb(treshold, treshold, treshold);
                            grayScale.SetPixel(x, y, c);
                        }
                    }

                }
            }
            this.bitmap = grayScale;

        }

        public void TresholdDouble(int tresholdTop, int tresholdDown, int value ,int mode, Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                bitmap = this.bitmap;
            }

            ImageService newImage = this;
            Bitmap grayScale = new Bitmap(newImage.bitmap.Width, newImage.bitmap.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    int grayValue = bitmap.GetPixel(x, y).G;
                    switch (mode)
                    {
                        case 0:
                            if (grayValue < tresholdDown || grayValue > tresholdDown)
                            {
                                Color c = Color.FromArgb(value, value, value);
                                grayScale.SetPixel(x, y, c);
                            }
                            else
                            {
                                Color c = Color.FromArgb(grayValue, grayValue, grayValue);
                                grayScale.SetPixel(x, y, c);

                            }
                            break;
                        case 1:
                            if (grayValue > tresholdDown && grayValue < tresholdDown)
                            {
                                Color c = Color.FromArgb(value, value, value);
                                grayScale.SetPixel(x, y, c);
                            }
                            else
                            {
                                Color c = Color.FromArgb(grayValue, grayValue, grayValue);
                                grayScale.SetPixel(x, y, c);
                            }

                            break;
                    }
                }
            }
            this.bitmap = grayScale;

        }

        public void TresholdOtsu(int threshold, int maxVal, Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                bitmap = this.bitmap;
            }


            Mat mat = this.bitmap.ToMat();

            if (mat.Channels() != 1){
                OpenCvSharp.Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            }

            OpenCvSharp.Cv2.Threshold(mat, mat, threshold, maxVal, ThresholdTypes.Otsu);

            this.bitmap = mat.ToBitmap();
        }

        public void TresholdAdaptive(int blockSize, int maxVal, int constant, int mode, Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                bitmap = this.bitmap;
            }


            Mat mat = bitmap.ToMat();

            if (mat.Channels() != 1)
            {
                OpenCvSharp.Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            }

            if(blockSize % 2 == 0)
            {
                blockSize += 1;
            }

            if (blockSize == 1)
            {
                blockSize = 3;
            }

            if (mode == 0)
            {
                OpenCvSharp.Cv2.AdaptiveThreshold(mat, mat, maxVal, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, blockSize, constant);
            }
            else
            {
                OpenCvSharp.Cv2.AdaptiveThreshold(mat, mat, maxVal, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, blockSize, constant);
            }

            this.bitmap = mat.ToBitmap();
        }


        public void blur()
        {
            Bitmap image = this.bitmap;
            
            int[,] blur_matrix = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R; var s2 = image.GetPixel(j + 1, i).R; var s3 = image.GetPixel(j + 2, i).R;
                    var s4 = image.GetPixel(j, i + 1).R; var s5 = image.GetPixel(j + 1, i + 1).R; var s6 = image.GetPixel(j + 2, i + 1).R;
                    var s7 = image.GetPixel(j, i + 2).R; var s8 = image.GetPixel(j + 1, i + 2).R; var s9 = image.GetPixel(j + 2, i + 2).R;

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 9;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void gaussianblur()
        {

            Bitmap image = this.bitmap;

            //int[,] blur_matrix = { {   0,  0,  -1,  0,  0 }, 
            //                        {  0, -1,  -2, -1,  0 }, 
            //                        { -1, -2,  16,-2,  -1 }, 
            //                        {  0,  -1, -2, -1,  0 }, 
            //                        {  0,   0, -1,  0,  0 } 
            //};
            int[,] blur_matrix = { {   1,  1,  1,  1, 1 },
                                    {  1, 1,  1, 1,  1 },
                                    { 1, 1,  1,1,  1 },
                                    {  1,  1, 1, 1,  1 },
                                    {  1,   1, 1,  1,  1 }
            };
            // chad sigma
            double sigma = 1.5;
            double RealBlulVal(int x, int y)
            {
                double val = (1 / ((2 * 3.1415926535) * sigma * sigma)) * Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                return val;
            }

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 2; i < image.Height - 4; ++i)
            {

                for (int j = 2; j < image.Width - 4; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix[0, 0] * RealBlulVal(-2, -2); var s2 = image.GetPixel(j + 1, i).R * blur_matrix[0, 1] * RealBlulVal(-1, 2); var s3 = image.GetPixel(j + 2, i).R * blur_matrix[0, 2] * RealBlulVal(0, 2); var s4 = image.GetPixel(j + 3, i).R * blur_matrix[0, 3] * RealBlulVal(1, 2); var s5 = image.GetPixel(j + 4, i).R * blur_matrix[0, 4] * RealBlulVal(2, 2);
                    var s6 = image.GetPixel(j, i + 1).R * blur_matrix[1, 0] * RealBlulVal(-2, 1); var s7 = image.GetPixel(j + 1, i + 1).R * blur_matrix[1, 1] * RealBlulVal(-1, 1); var s8 = image.GetPixel(j + 2, i + 1).R * blur_matrix[1, 2] * RealBlulVal(0, 1); var s9 = image.GetPixel(j + 3, i + 1).R * blur_matrix[1, 3] * RealBlulVal(1, 1); var s10 = image.GetPixel(j + 4, i + 1).R * blur_matrix[1, 4] * RealBlulVal(2, 1);
                    var s11 = image.GetPixel(j, i + 2).R * blur_matrix[2, 0] * RealBlulVal(-2, 0); var s12 = image.GetPixel(j + 1, i + 2).R * blur_matrix[2, 1] * RealBlulVal(-1, 0); var s13 = image.GetPixel(j + 2, i + 2).R * blur_matrix[2, 2] * RealBlulVal(0, 0); var s14 = image.GetPixel(j + 3, i + 2).R * blur_matrix[2, 3] * RealBlulVal(1, 0); var s15 = image.GetPixel(j + 4, i + 2).R * blur_matrix[2, 4] * RealBlulVal(2, 0);
                    var s16 = image.GetPixel(j, i + 3).R * blur_matrix[3, 0] * RealBlulVal(-2, -1); var s17 = image.GetPixel(j + 1, i + 3).R * blur_matrix[3, 1] * RealBlulVal(-1, -1); var s18 = image.GetPixel(j + 2, i + 3).R * blur_matrix[3, 2] * RealBlulVal(0, -1); var s19 = image.GetPixel(j + 3, i + 3).R * blur_matrix[3, 3] * RealBlulVal(1, -1); var s20 = image.GetPixel(j + 4, i + 3).R * blur_matrix[3, 4] * RealBlulVal(2, -1);
                    var s21 = image.GetPixel(j, i + 4).R * blur_matrix[4, 0] * RealBlulVal(-2, -2); var s22 = image.GetPixel(j + 1, i + 4).R * blur_matrix[4, 1] * RealBlulVal(-1, -2); var s23 = image.GetPixel(j + 2, i + 4).R * blur_matrix[4, 2] * RealBlulVal(0, -2); var s24 = image.GetPixel(j + 3, i + 4).R * blur_matrix[4, 3] * RealBlulVal(1, -2); var s25 = image.GetPixel(j + 4, i + 4).R * blur_matrix[4, 4] * RealBlulVal(2, -1);
                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 + s21 + s22 + s23 + s24 + s25) / 1; // dlaczego nie dzielimy przez 25 ? ponieważ jeżeli podzielimy przez 25 to ściemni nam obraz a mi chodziło tylko na przemnożeniu wartości przez 1 bo nie chciałem usuwać tego wszstkiego bo miałem już dość
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }

        public void blurWeighted()
        {
            Bitmap image = this.bitmap;

            int[,] blur_matrix = { { 1, 1, 1 }, { 1, 9, 1 }, { 1, 1, 1 } };

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R; var s2 = image.GetPixel(j + 1, i).R; var s3 = image.GetPixel(j + 2, i).R;
                    var s4 = image.GetPixel(j, i + 1).R; var s5 = image.GetPixel(j + 1, i + 1).R; var s6 = image.GetPixel(j + 2, i + 1).R;
                    var s7 = image.GetPixel(j, i + 2).R; var s8 = image.GetPixel(j + 1, i + 2).R; var s9 = image.GetPixel(j + 2, i + 2).R;

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 9;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }

        public void laplace0_5_0()
        {
            int[,] blur_matrix_0_5_0 = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];
                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9);
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;

                    


                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void laplacen_9_n()
        {
            int[,] blur_matrix_n_9_n = { { -1, -1, -1 }, { -1, 9, -1 }, { 1, -1, -1 } };
            Bitmap image = this.bitmap;


            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_n_9_n[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_n_9_n[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_n_9_n[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_n_9_n[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_n_9_n[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_n_9_n[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_n_9_n[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_n_9_n[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_n_9_n[2, 2];
                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9);
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }
        public void laplace1_5_1()
        {
            Bitmap image = this.bitmap;

            int[,] blur_matrix_1_5_1 = { { 1, -2, 1 }, { -2, 5, -2 }, { 1, -2, 1 } };

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_1_5_1[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_1_5_1[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_1_5_1[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_1_5_1[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_1_5_1[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_1_5_1[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_1_5_1[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_1_5_1[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_1_5_1[2, 2];
                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9);
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }

        public void laplaceEdge()
        {
            Bitmap image = this.bitmap;

            int[,] laplac_x_kernel = { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
            int[,] laplace_y_kernel = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
            Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1x = image.GetPixel(j, i).R * laplac_x_kernel[0, 0]; var s2x = image.GetPixel(j + 1, i).R * laplac_x_kernel[0, 1]; var s3x = image.GetPixel(j + 2, i).R * laplac_x_kernel[0, 2];
                    var s4x = image.GetPixel(j, i + 1).R * laplac_x_kernel[1, 0]; var s5x = image.GetPixel(j + 1, i + 1).R * laplac_x_kernel[1, 1]; var s6x = image.GetPixel(j + 2, i + 1).R * laplac_x_kernel[1, 2];
                    var s7x = image.GetPixel(j, i + 2).R * laplac_x_kernel[2, 0]; var s8x = image.GetPixel(j + 1, i + 2).R * laplac_x_kernel[2, 1]; var s9x = image.GetPixel(j + 2, i + 2).R * laplac_x_kernel[2, 2];

                    var s1y = image.GetPixel(j, i).R * laplace_y_kernel[0, 0]; var s2y = image.GetPixel(j + 1, i).R * laplace_y_kernel[0, 1]; var s3y = image.GetPixel(j + 2, i).R * laplace_y_kernel[0, 2];
                    var s4y = image.GetPixel(j, i + 1).R * laplace_y_kernel[1, 0]; var s5y = image.GetPixel(j + 1, i + 1).R * laplace_y_kernel[1, 1]; var s6y = image.GetPixel(j + 2, i + 1).R * laplace_y_kernel[1, 2];
                    var s7y = image.GetPixel(j, i + 2).R * laplace_y_kernel[2, 0]; var s8y = image.GetPixel(j + 1, i + 2).R * laplace_y_kernel[2, 1]; var s9y = image.GetPixel(j + 2, i + 2).R * laplace_y_kernel[2, 2];

                    var sum_1 = s1x + s2x + s3x + s4x + s5x + s6x + s7x + s8x + s9x;
                    var sum_2 = s1y + s2y + s3y + s4y + s5y + s6y + s7y + s8y + s9y;

                    //var final_sum = sum_1 + sum_2;
                    var final_sum = (int)Math.Sqrt(sum_1 * sum_1 + sum_2 * sum_2);
                    if (final_sum < 0) final_sum = 0;
                    if (final_sum > 255) final_sum = 255;

                    image.SetPixel(j, i, Color.FromArgb(255, final_sum, final_sum, final_sum));
                }
            }
        }


        public void cannyEdge()
        {
            Bitmap image = this.bitmap;

            int[,] laplac_x_kernel = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] laplace_y_kernel = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1x = image.GetPixel(j, i).R * laplac_x_kernel[0, 0]; var s2x = image.GetPixel(j + 1, i).R * laplac_x_kernel[0, 1]; var s3x = image.GetPixel(j + 2, i).R * laplac_x_kernel[0, 2];
                    var s4x = image.GetPixel(j, i + 1).R * laplac_x_kernel[1, 0]; var s5x = image.GetPixel(j + 1, i + 1).R * laplac_x_kernel[1, 1]; var s6x = image.GetPixel(j + 2, i + 1).R * laplac_x_kernel[1, 2];
                    var s7x = image.GetPixel(j, i + 2).R * laplac_x_kernel[2, 0]; var s8x = image.GetPixel(j + 1, i + 2).R * laplac_x_kernel[2, 1]; var s9x = image.GetPixel(j + 2, i + 2).R * laplac_x_kernel[2, 2];

                    var s1y = image.GetPixel(j, i).R * laplace_y_kernel[0, 0]; var s2y = image.GetPixel(j + 1, i).R * laplace_y_kernel[0, 1]; var s3y = image.GetPixel(j + 2, i).R * laplace_y_kernel[0, 2];
                    var s4y = image.GetPixel(j, i + 1).R * laplace_y_kernel[1, 0]; var s5y = image.GetPixel(j + 1, i + 1).R * laplace_y_kernel[1, 1]; var s6y = image.GetPixel(j + 2, i + 1).R * laplace_y_kernel[1, 2];
                    var s7y = image.GetPixel(j, i + 2).R * laplace_y_kernel[2, 0]; var s8y = image.GetPixel(j + 1, i + 2).R * laplace_y_kernel[2, 1]; var s9y = image.GetPixel(j + 2, i + 2).R * laplace_y_kernel[2, 2];

                    var sum_1 = s1x + s2x + s3x + s4x + s5x + s6x + s7x + s8x + s9x;
                    var sum_2 = s1y + s2y + s3y + s4y + s5y + s6y + s7y + s8y + s9y;

                    //var final_sum = sum_1 + sum_2;
                    var final_sum = (int)Math.Sqrt(sum_1 * sum_1 + sum_2 * sum_2);
                    if (final_sum < 0) final_sum = 0;
                    if (final_sum > 255) final_sum = 255;

                    image.SetPixel(j, i, Color.FromArgb(255, final_sum, final_sum, final_sum));
                }
            }


        }




        public void sobelEdge()
        {
            Bitmap image = this.bitmap;

            int[,] sobel_x_kernel = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobel_y_kernel = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {
                    var s1 = image.GetPixel(j, i).R * sobel_x_kernel[0, 0]; var s2 = image.GetPixel(j + 1, i).R * sobel_x_kernel[0, 1]; var s3 = image.GetPixel(j + 2, i).R * sobel_x_kernel[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * sobel_x_kernel[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * sobel_x_kernel[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * sobel_x_kernel[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * sobel_x_kernel[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * sobel_x_kernel[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * sobel_x_kernel[2, 2];

                    var sum_1 = s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9;

                    //var final_sum = sum_1;
                    if (sum_1 < 0) sum_1 = 0;
                    if (sum_1 > 255) sum_1 = 255;

                    temp_btm.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {
                    var s1 = image.GetPixel(j, i).R * sobel_y_kernel[0, 0]; var s2 = image.GetPixel(j + 1, i).R * sobel_y_kernel[0, 1]; var s3 = image.GetPixel(j + 2, i).R * sobel_y_kernel[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * sobel_y_kernel[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * sobel_y_kernel[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * sobel_y_kernel[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * sobel_y_kernel[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * sobel_y_kernel[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * sobel_y_kernel[2, 2];


                    var sum_1 = s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9;
                    //var final_sum = sum_1;
                    if (sum_1 < 0) sum_1 = 0;
                    if (sum_1 > 255) sum_1 = 255;

                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }

        public void sobelE()
        {
            int[,] blur_matrix_0_5_0 = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                
            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void sobelW()
        {
            int[,] blur_matrix_0_5_0 = { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }

        public void sobelNE()
        {
            int[,] blur_matrix_0_5_0 = { { 0, 1, 2 }, { -1, 0, 1 }, { -2, -1, 0 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void sobelNW()
        {
            int[,] blur_matrix_0_5_0 = { { 2, 1, 0 }, { 1, 0, -1 }, { 0, -1, -2 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }

        public void sobelN()
        {
            int[,] blur_matrix_0_5_0 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void sobelS()
        {
            int[,] blur_matrix_0_5_0 = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }

        }
        public void sobelSE()
        {
            int[,] blur_matrix_0_5_0 = { { -2, -1, 0 }, { -1, 0, 1 }, { 0, 1, 2 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }
        public void sobelSW()
        {
            int[,] blur_matrix_0_5_0 = { { 0, -1, -2 }, { 1, 0, -1 }, { 2, 1, 0 } };

            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;


                    var s1 = image.GetPixel(j, i).R * blur_matrix_0_5_0[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix_0_5_0[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix_0_5_0[0, 2];
                    var s4 = image.GetPixel(j, i + 1).R * blur_matrix_0_5_0[1, 0]; var s5 = image.GetPixel(j + 1, i + 1).R * blur_matrix_0_5_0[1, 1]; var s6 = image.GetPixel(j + 2, i + 1).R * blur_matrix_0_5_0[1, 2];
                    var s7 = image.GetPixel(j, i + 2).R * blur_matrix_0_5_0[2, 0]; var s8 = image.GetPixel(j + 1, i + 2).R * blur_matrix_0_5_0[2, 1]; var s9 = image.GetPixel(j + 2, i + 2).R * blur_matrix_0_5_0[2, 2];

                    var sum_1 = (int)(s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9) / 1;
                    if (sum_1 > 255) sum_1 = 255;
                    if (sum_1 < 0) sum_1 = 0;

                    //var final_sum = sum_1 + sum_2;



                    image.SetPixel(j, i, Color.FromArgb(255, sum_1, sum_1, sum_1));
                }
            }
        }
        public void median3x3()
        {
            Bitmap image = this.bitmap;

            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 1; i < image.Height - 2; ++i)
            {

                for (int j = 1; j < image.Width - 2; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;



                    int s1 = image.GetPixel(j, i).R; int s2 = image.GetPixel(j + 1, i).R; int s3 = image.GetPixel(j + 2, i).R;
                    int s4 = image.GetPixel(j, i + 1).R; int s5 = image.GetPixel(j + 1, i + 1).R; int s6 = image.GetPixel(j + 2, i + 1).R;
                    int s7 = image.GetPixel(j, i + 2).R; int s8 = image.GetPixel(j + 1, i + 2).R; int s9 = image.GetPixel(j + 2, i + 2).R;
                    int[] arr = { s1, s2, s3, s4, s5, s6, s7, s8, s9 };
                    Array.Sort(arr);

                    int npixel = arr[4];


                    //var final_sum = sum_1 + sum_2;

                    image.SetPixel(j, i, Color.FromArgb(255, npixel, npixel, npixel));
                }
            }
        }

        public void median5x5()
        {
            Bitmap image = this.bitmap;

            int[,] blur_matrix = { {   1,  1,  1,  1, 1 },
                                    {  1, 1,  1, 1,  1 },
                                    { 1, 1,  1,1,  1 },
                                    {  1,  1, 1, 1,  1 },
                                    {  1,   1, 1,  1,  1 }
            };
            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 2; i < image.Height - 4; ++i)
            {

                for (int j = 2; j < image.Width - 4; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;
                    var s1 = image.GetPixel(j, i).R * blur_matrix[0, 0]; var s2 = image.GetPixel(j + 1, i).R * blur_matrix[0, 1]; var s3 = image.GetPixel(j + 2, i).R * blur_matrix[0, 2]; var s4 = image.GetPixel(j + 3, i).R * blur_matrix[0, 3]; var s5 = image.GetPixel(j + 4, i).R * blur_matrix[0, 4];
                    var s6 = image.GetPixel(j, i + 1).R * blur_matrix[1, 0]; var s7 = image.GetPixel(j + 1, i + 1).R * blur_matrix[1, 1]; var s8 = image.GetPixel(j + 2, i + 1).R * blur_matrix[1, 2]; var s9 = image.GetPixel(j + 3, i + 1).R * blur_matrix[1, 3]; var s10 = image.GetPixel(j + 4, i + 1).R * blur_matrix[1, 4];
                    var s11 = image.GetPixel(j, i + 2).R * blur_matrix[2, 0]; var s12 = image.GetPixel(j + 1, i + 2).R * blur_matrix[2, 1]; var s13 = image.GetPixel(j + 2, i + 2).R * blur_matrix[2, 2]; var s14 = image.GetPixel(j + 3, i + 2).R * blur_matrix[2, 3]; var s15 = image.GetPixel(j + 4, i + 2).R * blur_matrix[2, 4];
                    var s16 = image.GetPixel(j, i + 3).R * blur_matrix[3, 0]; var s17 = image.GetPixel(j + 1, i + 3).R * blur_matrix[3, 1]; var s18 = image.GetPixel(j + 2, i + 3).R * blur_matrix[3, 2]; var s19 = image.GetPixel(j + 3, i + 3).R * blur_matrix[3, 3]; var s20 = image.GetPixel(j + 4, i + 3).R * blur_matrix[3, 4];
                    var s21 = image.GetPixel(j, i + 4).R * blur_matrix[4, 0]; var s22 = image.GetPixel(j + 1, i + 4).R * blur_matrix[4, 1]; var s23 = image.GetPixel(j + 2, i + 4).R * blur_matrix[4, 2]; var s24 = image.GetPixel(j + 3, i + 4).R * blur_matrix[4, 3]; var s25 = image.GetPixel(j + 4, i + 4).R * blur_matrix[4, 4];

                    int[] arr = { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25 };
                    Array.Sort(arr);

                    int npixel = arr[12];


                    //var final_sum = sum_1 + sum_2;

                    image.SetPixel(j, i, Color.FromArgb(255, npixel, npixel, npixel));
                }
            }
        }
        public void median7x7()
        {
            Bitmap image = this.bitmap;

            int[,] blur_matrix = { {   1,  1,  1,  1, 1 },
                                    {  1, 1,  1, 1,  1 },
                                    { 1, 1,  1,1,  1 },
                                    {  1,  1, 1, 1,  1 },
                                    {  1,   1, 1,  1,  1 }
            };
            //Bitmap temp_btm = new Bitmap(image.Width, image.Height);

            for (int i = 3; i < image.Height - 7; ++i)
            {

                for (int j = 3; j < image.Width - 7; ++j)
                {

                    //var greenpixel = image.GetPixel(j, i).R;
                    var s1 = image.GetPixel(j, i).R; var s2 = image.GetPixel(j + 1, i).R; var s3 = image.GetPixel(j + 2, i).R; var s4 = image.GetPixel(j + 3, i).R; var s5 = image.GetPixel(j + 4, i).R; var s6 = image.GetPixel(j + 5, i).R; var s7 = image.GetPixel(j + 6, i).R;
                    var s8 = image.GetPixel(j, i + 1).R; var s9 = image.GetPixel(j + 1, i + 1).R; var s10 = image.GetPixel(j + 2, i + 1).R; var s11 = image.GetPixel(j + 3, i + 1).R; var s12 = image.GetPixel(j + 4, i + 1).R; var s13 = image.GetPixel(j + 5, i + 1).R; var s14 = image.GetPixel(j + 6, i + 1).R;
                    var s15 = image.GetPixel(j, i + 2).R; var s16 = image.GetPixel(j + 1, i + 2).R; var s17 = image.GetPixel(j + 2, i + 2).R; var s18 = image.GetPixel(j + 3, i + 2).R; var s19 = image.GetPixel(j + 4, i + 2).R; var s20 = image.GetPixel(j + 5, i + 2).R; var s21 = image.GetPixel(j + 6, i + 2).R;
                    var s22 = image.GetPixel(j, i + 3).R; var s23 = image.GetPixel(j + 1, i + 3).R; var s24 = image.GetPixel(j + 2, i + 3).R; var s25 = image.GetPixel(j + 3, i + 3).R; var s26 = image.GetPixel(j + 4, i + 3).R; var s27 = image.GetPixel(j + 5, i + 3).R; var s28 = image.GetPixel(j + 6, i + 3).R;
                    var s29 = image.GetPixel(j, i + 4).R; var s30 = image.GetPixel(j + 1, i + 4).R; var s31 = image.GetPixel(j + 2, i + 4).R; var s32 = image.GetPixel(j + 3, i + 4).R; var s33 = image.GetPixel(j + 4, i + 4).R; var s34 = image.GetPixel(j + 5, i + 4).R; var s35 = image.GetPixel(j + 6, i + 4).R;
                    var s36 = image.GetPixel(j, i + 5).R; var s37 = image.GetPixel(j + 1, i + 5).R; var s38 = image.GetPixel(j + 2, i + 5).R; var s39 = image.GetPixel(j + 3, i + 5).R; var s40 = image.GetPixel(j + 4, i + 5).R; var s41 = image.GetPixel(j + 5, i + 5).R; var s42 = image.GetPixel(j + 6, i + 5).R;
                    var s43 = image.GetPixel(j, i + 6).R; var s44 = image.GetPixel(j + 1, i + 6).R; var s45 = image.GetPixel(j + 2, i + 6).R; var s46 = image.GetPixel(j + 3, i + 6).R; var s47 = image.GetPixel(j + 4, i + 6).R; var s48 = image.GetPixel(j + 5, i + 6).R; var s49 = image.GetPixel(j + 6, i + 6).R;


                    int[] arr = {
                    s1,
                    s2,
                    s3,
                    s4,
                    s5,
                    s6,
                    s7,
                    s8,
                    s9,
                    s10,
                    s11,
                    s12,
                    s13,
                    s14,
                    s15,
                    s16,
                    s17,
                    s18,
                    s19,
                    s20,
                    s21,
                    s22,
                    s23,
                    s24,
                    s25,
                    s26,
                    s27,
                    s28,
                    s29,
                    s30,
                    s31,
                    s32,
                    s33,
                    s34,
                    s35,
                    s36,
                    s37,
                    s38,
                    s39,
                    s40,
                    s41,
                    s42,
                    s43,
                    s44,
                    s45,
                    s46,
                    s47,
                    s48,
                    s49
                     };


                    Array.Sort(arr);

                    int npixel = arr[24];




                    image.SetPixel(j, i, Color.FromArgb(255, npixel, npixel, npixel));
                }
            }
        }


        public void borderContant()
        {
            Mat mat = this.bitmap.ToMat();

            mat = mat.CopyMakeBorder(25, 25, 25, 25, OpenCvSharp.BorderTypes.Constant);

            this.bitmap = mat.ToBitmap();

        }

        public void borderContantBlack()
        {
            Mat mat = this.bitmap.ToMat();

            mat = mat.CopyMakeBorder(25, 25, 25, 25, OpenCvSharp.BorderTypes.Constant, new Scalar(255, 0, 0));

            this.bitmap = mat.ToBitmap();

        }
        public void borderReflect()
        {
            Mat mat = this.bitmap.ToMat();

            mat = mat.CopyMakeBorder(25, 25, 25, 25, OpenCvSharp.BorderTypes.Reflect);

            this.bitmap = mat.ToBitmap();

        }
        public void borderWrap()
        {
            Mat mat = this.bitmap.ToMat();

            mat = mat.CopyMakeBorder(25, 25, 25, 25, OpenCvSharp.BorderTypes.Wrap);

            this.bitmap = mat.ToBitmap();

        }
        
        public void morphErode(){
            Mat kernel = OpenCvSharp.Cv2.GetStructuringElement(MorphShapes.Cross, new OpenCvSharp.Size(3, 3));

            Mat mat = this.bitmap.ToMat();

            mat = mat.Erode(kernel, iterations: 5);

            this.bitmap = mat.ToBitmap();
        }

        public void morphDilate(){
            Mat kernel = OpenCvSharp.Cv2.GetStructuringElement(MorphShapes.Cross, new OpenCvSharp.Size(3, 3));

            Mat mat = this.bitmap.ToMat();

            mat = mat.Dilate(kernel, iterations: 5);

            this.bitmap = mat.ToBitmap();
        }

        public void morphOpen(){
            Mat kernel = OpenCvSharp.Cv2.GetStructuringElement(MorphShapes.Cross, new OpenCvSharp.Size(3, 3));

            Mat mat = this.bitmap.ToMat();

            mat = mat.MorphologyEx(MorphTypes.Open, kernel, iterations: 5);

            this.bitmap = mat.ToBitmap();
        }

        public void morphClose(){
            Mat kernel = OpenCvSharp.Cv2.GetStructuringElement(MorphShapes.Cross, new OpenCvSharp.Size(3, 3));

            Mat mat = this.bitmap.ToMat();

            mat = mat.MorphologyEx(MorphTypes.Close, kernel, iterations: 5);

            this.bitmap = mat.ToBitmap();
        }

        public void saveMomentsData()
        {
            Mat mat = this.bitmap.ToMat();

            if (mat.Channels() != 1)
            {
                OpenCvSharp.Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            }

            Cv2.Threshold(mat, mat, 100, 255, ThresholdTypes.Binary);

            Moments moments = Cv2.Moments(mat, true);

            OpenCvSharp.Point[][] contours;
            OpenCvSharp.HierarchyIndex[] hierarchy;

            Cv2.FindContours(mat, out contours, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String file = path + "\\Output.csv";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "Area", "Circumference", "Aspect Ratio", "Extent", "Solidity", "Equivalent Diameter",
            "M00", "M01", "M02", "M03", "M10", "M11", "M12", "M20", "M21", "M30", "Mu02", "Mu03","Mu11","Mu12", "Mu20", "Mu21", "Mu30", "Nu02", "Nuo3", "Nu11", "Nu12", "Nu20", "Nu21", "Nu30"};
            output.AppendLine(string.Join(separator, headings));

            foreach (Point[] contour in contours)
            {
                Moments currentMoments = Cv2.Moments(contour, true);

                // #1
                double area = Cv2.ContourArea(contour);

                Rect rect =  Cv2.BoundingRect(contour);

                double arcLength = OpenCvSharp.Cv2.ArcLength(contour, true);


                // #2
                float aspectRatio = ((float)rect.Width) / rect.Height;

                float rectArea = rect.Width * rect.Height;
                // #3 
                float  extent = rectArea / rectArea;


                Point[] hull = Cv2.ConvexHull(contour);

                double hull_area = Cv2.ContourArea(hull);
                // #4 
                double solidity = area / hull_area;

                // #5
                double equi_diameter = Math.Sqrt(4 * area / Math.PI);

                
                String[] newLine = { area.ToString(), arcLength.ToString(), aspectRatio.ToString(), extent.ToString(), solidity.ToString(), equi_diameter.ToString(),
                currentMoments.M00.ToString(), currentMoments.M01.ToString(), currentMoments.M02.ToString(), currentMoments.M03.ToString(),
                currentMoments.M10.ToString(), currentMoments.M11.ToString(), currentMoments.M12.ToString(),
                currentMoments.M20.ToString(), currentMoments.M21.ToString(),
                currentMoments.M30.ToString(), currentMoments.Mu02.ToString(), currentMoments.Mu03.ToString(), currentMoments.Mu11.ToString(), currentMoments.Mu12.ToString(), currentMoments.Mu20.ToString(),
                currentMoments.Mu21.ToString(), currentMoments.Mu30.ToString(), currentMoments.Mu30.ToString(),
                currentMoments.Nu02.ToString(), currentMoments.Nu03.ToString(), currentMoments.Nu11.ToString(), currentMoments.Nu12.ToString(), currentMoments.Nu20.ToString(), currentMoments.Nu21.ToString(), currentMoments.Nu30.ToString()
                };
                output.AppendLine(string.Join(separator, newLine));
            
            
            }
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }


            this.bitmap = mat.ToBitmap();
        }

    }
}
