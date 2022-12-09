using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

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

        public void Treshold(int treshold)
        {
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

        public void TresholdEqualize(int treshold, int direction)
        {
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

        public void TresholdDouble(int tresholdTop, int tresholdDown, int value ,int mode)
        {
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

        public void blur()
        {
            Bitmap image = this.bitmap;
            
            int[,] blur_matrix = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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

            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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

            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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

            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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


            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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

            //Bitmap temp_btm = new Bitmap(image.getWidth(), image.getHeight());

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


    }
}
