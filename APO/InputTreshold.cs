using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APO
{
    public partial class InputTreshold : Form
    {
        private Image image;
        private Form parent;
        public InputTreshold(Image image, Form parent)
        {
            InitializeComponent();
            this.image = image;
            this.parent = parent;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Continue_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) > 255 | Int32.Parse(textBox1.Text) < 0)
            {
                return;
            }
            
            Bitmap bitmap = new Bitmap(this.image);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Treshold(Int32.Parse(textBox1.Text));
            imageService.Create();
            imageService.imageView.MdiParent = this.parent;
            imageService.imageView.Text = "copy";
            imageService.Show();
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
