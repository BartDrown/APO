using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO
{
    public partial class InputTresholdDouble : Form
    {
        private Image image;
        private Form parent;
        private List<ImageService> imagesList;
        public InputTresholdDouble(Image image, Form parent, List<ImageService> imagesList)
        {
            InitializeComponent();
            this.image = image;
            this.parent = parent;
            this.imagesList = imagesList;
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            //Up
            if (Int32.Parse(textBox1.Text) > 255 || Int32.Parse(textBox1.Text) < 0)
            {
                return;
            }
            //Down
            if (Int32.Parse(textBox3.Text) > 255 || Int32.Parse(textBox3.Text) < 0)
            {
                return;
            }
            //FinalValue
            if (Int32.Parse(textBox4.Text) > 255 || Int32.Parse(textBox4.Text) < 0)
            {
                return;
            }
            // Inclusive/exclusive
            if (Int32.Parse(textBox2.Text) != 0 && Int32.Parse(textBox2.Text) != 1)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(this.image);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.TresholdDouble(Int32.Parse(textBox1.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox2.Text));
            imageService.Create();
            imageService.imageView.MdiParent = this.parent;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

    }
}
