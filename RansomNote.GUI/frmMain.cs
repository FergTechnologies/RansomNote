using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RansomNote.Tesseract;
using System.IO;
using RansomNote.Imaging.Extensions;
using System.Drawing.Imaging;

namespace RansomNote.GUI
{
    public partial class frmMain : Form
    {
        TextBoxStreamWriter writer;

       
        public frmMain()
        {
            InitializeComponent();
            writer = new TextBoxStreamWriter(txtOutput);
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var ofb = new OpenFileDialog();
            try
            {
                ofb.InitialDirectory = shellSaveFolder.CurrentFolder.FileSystemPath;
            }
            catch (Exception ex)
            {
                ofb.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            ofb.Filter = "Image Files(*.png;*.bmp;*.jpg;*.gif;*.tif;*.tiff)|*.png;*.bmp;*.jpg;*.gif;*.tif;*.tiff|All files (*.*)|*.*";
            ofb.Title = "Select an Image File To Extract Characters From.";

            if (ofb.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(ofb.FileName))
                {
                    throw new FileNotFoundException($"Could not locate image with filename {ofb.FileName}.");
                }
                var img = Image.FromFile(ofb.FileName);
                img = Preprocess(img);
                var cie = new CharacterImageExtractor(img, shellSaveFolder.CurrentFolder.FileSystemPath);
                cie.ConsoleOut += Cie_ConsoleOut;
                cie.Error += Cie_Error;
                var path = ofb.FileName;
                Task.Factory.StartNew(() =>
                {
                    cie.GetCharacterImages("eng.mycoiocr");
                });
            }
                
           
        }

        private Image Preprocess(Image img)
        {

            img = img.ToPixelFormat(PixelFormat.Format24bppRgb);
            Console.WriteLine("Converting to Grayscale...");
            img = img.Grayscale();
            Console.WriteLine("Binaraizing...");
            img = img.Threshold();
            return img;
        }

        private void Cie_Error(object sender, UnhandledExceptionEventArgs e)
        {
            txtOutput.InvokeIfRequired(() =>
            {
                writer.Write($"\nERROR: {((Exception)e.ExceptionObject).Message}");
            });
        }

        private void Cie_ConsoleOut(object sender, ExtractionEventArgs e)
        {
            txtOutput.InvokeIfRequired(() =>
            {
                writer.Write($"\n{e.EventMessage}");
            });
        }
    }
}
