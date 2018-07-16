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
using Tesseract;

namespace RansomNote.GUI
{
    public partial class frmMain : Form
    {
        TextBoxStreamWriter writer;

        private const string dPath = "./tessdata/";
        private const string lang = "eng";
        private TessBaseAPI api;
        public frmMain()
        {
            InitializeComponent();
            writer = new TextBoxStreamWriter(txtOutput);
            api = new TessBaseAPI();
            OcrEngineMode oem = OcrEngineMode.TESSERACT_LSTM_COMBINED;
            PageSegmentationMode psm = PageSegmentationMode.RAW_LINE;
            if (!api.Init(dPath, lang, oem))
            {
                throw new Exception("Could not initialize tesseract.");
            }
            api.SetPageSegMode(psm);
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
            {
                var chk = new FileTyper.FileTypeChecker();


                var typ = chk.GetFileType(new MemoryStream(File.ReadAllBytes(imgFile)));
                var lstImages = new List<Image>();
                if (typ.Extension == ".pdf")
                {
                    writer.WriteLine($"Getting Images From PDF {imgFile}...");
                    var pages = new PDF.PDFProcessor(File.ReadAllBytes(imgFile)).ParsePDF();
                    pages.OrderBy(a => a.Key).Select(a => a.Value).ToList().ForEach(a =>
                    {
                        lstImages.Add(a);
                    });
                }
                else
                {
                    var img = Image.FromFile(imgFile);
                   
                    lstImages.Add(img);
                }
                writer.WriteLine($"Preprocessing images...");
                for (var n = 0; n < lstImages.Count; n++)
                {
                    writer.WriteLine($"     Preprocessing Image {n}...");
                    lstImages[n] = Preprocess(lstImages[n]);
                }
                var tsks = new List<Task>();
                writer.WriteLine("Doing Character Extractions...");
                foreach(var i in lstImages)
                {
                    tsks.Add(Task.Factory.StartNew(() =>
                    {
                        this.InvokeIfRequired(() =>
                        {
                            var cie = new CharacterImageExtractor(i, (Directory.Exists(txtFolderPath.Text)) ? txtFolderPath.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                            cie.ConsoleOut += Cie_ConsoleOut;
                            cie.Error += Cie_Error;
                            var path = imgFile;
                            cie.GetCharacterImages(ref api, $"eng.mycoiocr{lstImages.IndexOf(i)}");
                            return;
                        });
                    }));
                }
                writer.WriteLine($"Waiting for character extraction...");
            }
            
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
