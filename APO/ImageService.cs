using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


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
    }
}
