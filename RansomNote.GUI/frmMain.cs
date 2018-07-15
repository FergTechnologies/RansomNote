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
using System.Collections.Concurrent;

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
                ofb.InitialDirectory = (Directory.Exists(txtFolderPath.Text)) ? txtFolderPath.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            catch (Exception ex)
            {
                ofb.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            ofb.Filter = "Image Files(*.png;*.bmp;*.jpg;*.gif;*.tif;*.tiff)|*.png;*.bmp;*.jpg;*.gif;*.tif;*.tiff|PDF Files(*.pdf)|*.pdf|All files (*.*)|*.*";
            ofb.Title = "Select an File To Extract Characters From.";
            if (ofb.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofb.FileName;
            }

        }

        private void Export(string imgFile)
        {

                if (!File.Exists(imgFile))
                {
                    throw new FileNotFoundException($"Could not locate image with filename {imgFile}.");
                }
                var chk = new FileTyper.FileTypeChecker();

               
                var typ = chk.GetFileType(new MemoryStream(File.ReadAllBytes(imgFile)));
                var lstImages = new ConcurrentBag<Image>();
                if (typ.Extension == ".pdf")
                {
                    writer.WriteLine($"Getting Images From PDF {imgFile}...");
                    var pages = new PDF.PDFProcessor(File.ReadAllBytes(imgFile)).ParsePDF();
                pages.OrderBy(a => a.Key).Select(a => a.Value).ToList().ForEach(a =>
                {
                    lstImages.Add(a);
                });

                
                    writer.WriteLine($"Preprocessing {pages.Count} images...");
                Parallel.ForEach(pages, (p) =>
                {
                    var cie = new CharacterImageExtractor(p.Value, (Directory.Exists(txtFolderPath.Text)) ? txtFolderPath.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    cie.ConsoleOut += Cie_ConsoleOut;
                    cie.Error += Cie_Error;
                    var path = imgFile;
                    Cursor = Cursors.WaitCursor;
                    Task.Factory.StartNew(() =>
                    {
                        cie.GetCharacterImages("eng.mycoiocr");
                    }).ContinueWith((a) =>
                    {
                        Cursor = Cursors.Default;
                    });
                });
                }
                else
                {
                    var img = Image.FromFile(imgFile);
                    writer.WriteLine($"Preprocessing image...");
                        lstImages.Add(Preprocess(img));
                }
            var bag = new ConcurrentBag<Image>();

                Parallel.ForEach(lstImages, (i) =>
                {
                    bag.Add(i);
                    var cie = new CharacterImageExtractor(i, (Directory.Exists(txtFolderPath.Text)) ? txtFolderPath.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    cie.ConsoleOut += Cie_ConsoleOut;
                    cie.Error += Cie_Error;
                    var path = imgFile;
                    Cursor = Cursors.WaitCursor;
                    Task.Factory.StartNew(() =>
                    {
                        cie.GetCharacterImages("eng.mycoiocr");
                    }).ContinueWith((a) =>
                    {
                        this.InvokeIfRequired(() =>
                        {
                            Cursor = Cursors.Default;
                        });
                        
                    });
                });
            
        }
        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
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
                writer.WriteLine($"ERROR: {((Exception)e.ExceptionObject).Message}");
            });
        }

        private void Cie_ConsoleOut(object sender, ExtractionEventArgs e)
        {
            txtOutput.InvokeIfRequired(() =>
            {
                writer.WriteLine($"{e.EventMessage}");
            });
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            var fDialog = new FolderBrowserDialog();
            fDialog.Description = "Select a folder to save your results.";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = fDialog.SelectedPath;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            Export(txtImagePath.Text);
        }
    }
}
