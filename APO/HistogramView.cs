using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO {
    public partial class HistogramView : Form {
        private ImageService imageService;
        
        public HistogramView(ImageService imageService) {
            InitializeComponent();

            this.imageService = imageService;
        }


        public void renderHistogram(string type){
            switch (type) {
                case "Grayscale":
                    for (int i = 0; i < imageService.lut_table.Length; ++i) {
                        HistogramChart.Series["Series1"].Points.AddXY(i, imageService.lut_table[i]);
                    }
                    HistogramChart.Series["Series1"].Color = Color.Black;
                    break;
                case "Red":
                    for (int i = 0; i < imageService.r_table.Length; ++i) {
                        HistogramChart.Series["Series1"].Points.AddXY(i, imageService.r_table[i]);
                    }
                    HistogramChart.Series["Series1"].Color = Color.Red;

                    break;
                case "Green":
                    for (int i = 0; i < imageService.g_table.Length; ++i) {
                        HistogramChart.Series["Series1"].Points.AddXY(i, imageService.g_table[i]);
                    }
                    HistogramChart.Series["Series1"].Color = Color.Green;

                    break;
                case "Blue":
                    for (int i = 0; i < imageService.b_table.Length; ++i) {
                        HistogramChart.Series["Series1"].Points.AddXY(i, imageService.b_table[i]);
                    }
                    HistogramChart.Series["Series1"].Color = Color.Blue;
                    break;
            }
            HistogramChart.ChartAreas[0].AxisX.Maximum = 255;
            HistogramChart.ChartAreas[0].AxisX.Minimum = 0;
            this.Show();

        }   


    }
}
