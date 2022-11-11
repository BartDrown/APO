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
    public partial class InputTresholdEqualize : Form
    {
        private Image image;
        private Form parent;
        private List<ImageService> imagesList;
        public InputTresholdEqualize(Image image, Form parent, List<ImageService> imagesList)
        {
            InitializeComponent();
            this.image = image;
            this.parent = parent;
            this.imagesList = imagesList;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Continue_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) > 255 || Int32.Parse(textBox1.Text) < 0)
            {
                return;
            }
            if (Int32.Parse(textBox2.Text) != 0 && Int32.Parse(textBox2.Text) != 1)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(this.image);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.TresholdEqualize(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
            imageService.Create();
            imageService.imageView.MdiParent = this.parent;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
