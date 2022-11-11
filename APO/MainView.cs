using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework.Forms;
namespace APO {
    public partial class MainView : Form {
        List<ImageService> imagesList = new List<ImageService>();

        public MainView() {
            InitializeComponent();

            foreach (Control ctrl in this.Controls) {
                if (ctrl is MdiClient) {
                    ctrl.BackColor = Color.Beige;
                }
            }
        }

        private void MainView_Load(object sender, EventArgs e) {

        }


        private void OpenImage_Click_1(object sender, EventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            DialogResult status = fileDialog.ShowDialog();
            if (status == DialogResult.OK) {
                Image image = Image.FromFile(fileDialog.FileName);
                Bitmap bitmap = new Bitmap(image);
                ImageService imageService = new ImageService(bitmap, fileDialog.FileName);
                imageService.Create();
                imageService.imageView.MdiParent = this;
                imageService.imageView.Text = fileDialog.FileName;
                imagesList.Add(imageService);
                imageService.Show();


            }
        }

        

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e) {
            ImageService child = this.getActiveChild();
            HistogramService histogramService = new HistogramService(child);
            histogramService.renderHistogram("Grayscale");
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e) {
            ImageService child = this.getActiveChild();
            HistogramService histogramService = new HistogramService(child);
            histogramService.renderHistogram("Red");
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e) {
            ImageService child = this.getActiveChild();
            HistogramService histogramService = new HistogramService(child);
            histogramService.renderHistogram("Green");
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e) {
            ImageService child = this.getActiveChild();
            HistogramService histogramService = new HistogramService(child);
            histogramService.renderHistogram("Blue");
        }


        private ImageService getActiveChild() {
            foreach (ImageService item in imagesList) {
                if (item.imageView.Focused) {
                    return item;
                }
            }
            return null;
        }

        private void duplicate_Click(object sender, EventArgs e) {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void magnifyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void magnify25_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void magnify25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageService child = this.getActiveChild();
            HistogramService histogramService = new HistogramService(child);
            histogramService.renderHistogram("Grayscale");
        }

        private void notToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addSaturatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageService child = this.getActiveChild();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            DialogResult status = fileDialog.ShowDialog();
            if (status == DialogResult.OK)
            {
                Image image = Image.FromFile(fileDialog.FileName);
                Bitmap bitmap = new Bitmap(image);


                ImageService newImageService = new ImageService(bitmap, fileDialog.FileName);
                newImageService.Create();

                Bitmap added = child.addImage(newImageService, 0);


                ImageService imageService = new ImageService(added, fileDialog.FileName);

                imageService.Create();
                imageService.imageView.MdiParent = this;
                imageService.imageView.Text = fileDialog.FileName;
                imagesList.Add(imageService);
                imageService.Show();
            }


        }

        private void addNotSaturatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageService child = this.getActiveChild();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            DialogResult status = fileDialog.ShowDialog();
            if (status == DialogResult.OK)
            {
                Image image = Image.FromFile(fileDialog.FileName);
                Bitmap bitmap = new Bitmap(image);


                ImageService newImageService = new ImageService(bitmap, fileDialog.FileName);
                newImageService.Create();

                Bitmap added = child.addImage(newImageService, 1);


                ImageService imageService = new ImageService(added, fileDialog.FileName);

                imageService.Create();
                imageService.imageView.MdiParent = this;
                imageService.imageView.Text = fileDialog.FileName;
                imagesList.Add(imageService);
                imageService.Show();
            }
        }

        private void substractAbsoluteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageService child = this.getActiveChild();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            DialogResult status = fileDialog.ShowDialog();
            if (status == DialogResult.OK)
            {
                Image image = Image.FromFile(fileDialog.FileName);
                Bitmap bitmap = new Bitmap(image);


                ImageService newImageService = new ImageService(bitmap, fileDialog.FileName);
                newImageService.Create();

                Bitmap added = child.addImage(newImageService, 2);


                ImageService imageService = new ImageService(added, fileDialog.FileName);

                imageService.Create();
                imageService.imageView.MdiParent = this;
                imageService.imageView.Text = fileDialog.FileName;
                imagesList.Add(imageService);
                imageService.Show();
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form testDialog = new Form(TextBox);

            //// Show testDialog as a modal dialog and determine if DialogResult = OK.
            //if (testDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    // Read the contents of testDialog's TextBox.
            //    String value = testDialog.TextBox .Text;
            //}
            //else
            //{
            //    this.txtResult.Text = "Cancelled";
            //}
            //testDialog.Dispose();
        }
    }
}
