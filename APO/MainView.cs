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
            Bitmap originalBitmap = new Bitmap(image);
            Size newSize = new Size((int)(originalBitmap.Width / 4), (int)(originalBitmap.Height / 4));
            Bitmap bitmap = new Bitmap(originalBitmap, newSize);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void magnify50_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap originalBitmap = new Bitmap(image);
            Size newSize = new Size((int)(originalBitmap.Width / 2), (int)(originalBitmap.Height / 2));
            Bitmap bitmap = new Bitmap(originalBitmap, newSize);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void magnify150_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap originalBitmap = new Bitmap(image);
            Size newSize = new Size((int)(originalBitmap.Width * 1.5), (int)(originalBitmap.Height * 1.5));
            Bitmap bitmap = new Bitmap(originalBitmap, newSize);
            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void magnify200_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap originalBitmap = new Bitmap(image);
            Size newSize = new Size((int)(originalBitmap.Width * 2), (int)(originalBitmap.Height * 2));
            Bitmap bitmap = new Bitmap(originalBitmap, newSize);
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
            //elsew
            //{
            //    this.txtResult.Text = "Cancelled";
            //}
            //testDialog.Dispose();
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void progToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            Bitmap grayScale = new Bitmap(image.Width, image.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    Color c = bitmap.GetPixel(x, y);

                    Int32 gs = (Int32)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);

                    grayScale.SetPixel(x, y, Color.FromArgb(gs, gs, gs));
                }
            }
            ImageService imageService = new ImageService(grayScale, "copy");
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);
            imageService.Show();
        }

        private void negationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.Negate();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            Form form = new InputTreshold(getActiveChild().bitmap, this, imagesList);
            form.MdiParent = this;
            form.Show();

        }

        private void singleEqulizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new InputTresholdEqualize(getActiveChild().bitmap, this, imagesList);
            form.MdiParent = this;
            form.Show();
        }

        private void doubleEqualizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new InputTresholdDouble(getActiveChild().bitmap, this, imagesList);
            form.MdiParent = this;
            form.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.laplace0_5_0();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.laplace1_5_1();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void linearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.blur();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void gausianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.gaussianblur();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void averagingWithWeightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.blurWeighted();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void n9nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.laplacen_9_n();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelEdge();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void laplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.laplaceEdge();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.cannyEdge();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelN();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void nEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelNE();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelE();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void sEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelSE();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelS();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void sWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelSW();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void wToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelW();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void nWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.sobelNW();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.median3x3 ();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.median5x5();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.median7x7();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void constant5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.borderContant();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void borderReflectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.borderReflect();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.borderContantBlack();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void borderWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = getActiveChild().bitmap;
            Bitmap bitmap = new Bitmap(image);

            ImageService imageService = new ImageService(bitmap, "copy");
            imageService.borderWrap();
            imageService.Create();
            imageService.imageView.MdiParent = this;
            imageService.imageView.Text = "copy";
            imagesList.Add(imageService);

            imageService.Show();
        }

        private void otsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new InputTresholdOtsu(getActiveChild().bitmap, this, imagesList);
            form.MdiParent = this;
            form.Show();
        }

        private void adaptiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new InputThresholdAdaptive(getActiveChild().bitmap, this, imagesList);
            form.MdiParent = this;
            form.Show();
        }
    }
}
