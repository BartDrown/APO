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
    public partial class InputThresholdAdaptive : Form
    {
        private Bitmap originalImage = null;
        private ImageService previewService = null;

        private Image image;
        private Form parent;
        private List<ImageService> imagesList;
        public InputThresholdAdaptive(Image image, Form parent, List<ImageService> imagesList)
        {
            InitializeComponent();
            this.image = image;
            this.parent = parent;
            this.imagesList = imagesList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Continue_Click(object sender, EventArgs e)
        {

        }

        private void Continue_Click_1(object sender, EventArgs e)
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
                imageService.TresholdAdaptive(Int32.Parse(textBox1.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox2.Text));
                imageService.Create();
                imageService.imageView.MdiParent = this.parent;
                imageService.imageView.Text = "copy";
                imagesList.Add(imageService);
                imageService.Show();

                this.previewService.Close();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.updatePreview();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.updatePreview();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.updatePreview();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.updatePreview();
        }

        private void updatePreview()
        {
            try
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
            }
            catch { return; }

            if (this.previewService == null)
            {
                Bitmap bitmap = new Bitmap(this.image);
                this.originalImage = new Bitmap(this.image);
                ImageService imageService = new ImageService(bitmap, "preview");
                this.previewService = imageService;
                imageService.TresholdAdaptive(Int32.Parse(textBox1.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox2.Text));
                imageService.Create();
                imageService.imageView.MdiParent = this.parent;
                imageService.imageView.Text = "preview";
                imagesList.Add(imageService);
                imageService.Show();
            }
            else
            {
                this.previewService.Update(this.originalImage);
                this.previewService.TresholdAdaptive(Int32.Parse(textBox1.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox2.Text), this.originalImage);
                this.previewService.UpdateSelf();
                this.previewService.Show();

            }

        }
    }
}
