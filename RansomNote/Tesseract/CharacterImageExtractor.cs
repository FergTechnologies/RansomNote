using Leptonica;
using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace RansomNote.Tesseract
{
    public class CharacterImageExtractor
    {
        public event EventHandler<ExtractionEventArgs> ConsoleOut;
        public event EventHandler<UnhandledExceptionEventArgs> Error;
        protected virtual void Out(ExtractionEventArgs e) => ConsoleOut?.Invoke(this, e);
        protected virtual void Err(UnhandledExceptionEventArgs e) => Error?.Invoke(this, e);
        private Pix pix = null;
        private Image ima = null;
        private string _savePath = "";
        public CharacterImageExtractor(Image image, string SaveFolderPath)
        {
            Out(new ExtractionEventArgs($"Loading image into Memory and validating the save path."));
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                pix = Pix1.pixReadFromMemoryStream(ms);
                ima = image;
            }
            if (pix == null)
            {
                Err(new UnhandledExceptionEventArgs(new Exception("Could not convert the image to a Leptonica Pix Object."), true));
            }
            if (!Directory.Exists(SaveFolderPath))
            {
                Err(new UnhandledExceptionEventArgs(new DirectoryNotFoundException($"Could not locate a folder with path {SaveFolderPath}."), true));
            }
            _savePath = SaveFolderPath;
            Out(new ExtractionEventArgs("Loading complete."));
        }

        public void GetCharacterImages(string prefix)
        {
            Out(new ExtractionEventArgs("Preparing to initialize tesseract."));
            var localDict = new Dictionary<char, Image>();
            var dPath = "./tessdata/";
            var lang = "eng";
            var api = new TessBaseAPI();
            OcrEngineMode oem = OcrEngineMode.TESSERACT_LSTM_COMBINED;
            PageSegmentationMode psm = PageSegmentationMode.RAW_LINE;
            if (!api.Init(dPath, lang, oem))
            {
                Err(new UnhandledExceptionEventArgs(new Exception("Could not initialize tesseract."), true));
            }
            api.SetPageSegMode(psm);
            Out(new ExtractionEventArgs("Setting Image..."));
            api.SetImage(pix);
            Out(new ExtractionEventArgs("Recoginizing Characters..."));
            api.Recognize();
            
            var pageItLevel = PageIteratorLevel.RIL_SYMBOL;
            var iterator = api.GetIterator();
            iterator.Begin();
            var l = new List<CharacterMap>();
            var tsks = new List<Task>();
            using (var iter = iterator)
            {
                do
                {
                    do
                    {
                        do
                        {
                            
                            // get bounding box for symbol
                            var resultIterator = iterator.GetPageIterator();
                            var symbCount = 0;
                            tsks.Add(Task.Factory.StartNew(() =>
                            {
                                do
                                {
                                   
                                    resultIterator.BoundingBox(pageItLevel, out int left, out int top, out int right, out int bottom);
                                    var ch = iter.GetUTF8Text(pageItLevel);
                                    var rect = new Rectangle(left, top, right - left, bottom - top);

                                    var img = Crop(ima, rect);
                                    if (img == null)
                                    {
                                        Out(new ExtractionEventArgs($"Unable to crop image with width: {rect.Width} height: {rect.Height}"));
                                        continue;
                                    }
                                    Out(new ExtractionEventArgs($"Cropped Character For Box {symbCount}."));
                                    var path = $@"{_savePath}\{prefix}.{symbCount}.png";
                                    img.Save(path, ImageFormat.Png);
                                    l.Add(new CharacterMap
                                    {
                                        FilePath = path,
                                        Text = ch
                                    });
                                    Out(new ExtractionEventArgs($"Saving Cropped Image as {path}"));
                                    Interlocked.Increment(ref symbCount);
                                } while (resultIterator.Next(pageItLevel));
                            }));
                            Task.WaitAll(tsks.ToArray());
                           
                        } while (iter.Next(PageIteratorLevel.RIL_WORD));
                    } while (iter.Next(PageIteratorLevel.RIL_TEXTLINE));
                } while (iter.Next(PageIteratorLevel.RIL_PARA));
            }

            Out(new ExtractionEventArgs("Serializing Character Data to JSON..."));
            var json = JsonConvert.SerializeObject(l);
            var rPath = $@"{_savePath}\{prefix}.results.json";
            Out(new ExtractionEventArgs($"Writing JSON to {rPath}..."));
            File.WriteAllText(rPath, json);
            Out(new ExtractionEventArgs($"Results saved to {rPath}."));
            Out(new ExtractionEventArgs($"Character Images Retrieved. Please see the folder at path {_savePath}."));

        }

        private Image Crop(Image img, Rectangle rect)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return null;
            }
            var bMap  = new Bitmap(rect.Width, rect.Height);
            using (var g = System.Drawing.Graphics.FromImage(bMap))
            {
                g.DrawImage(img, new Rectangle(0, 0, bMap.Width, bMap.Height), rect, GraphicsUnit.Pixel);
            }
            return bMap;
        }

        private struct CharacterMap
        {
            public string FilePath;
            public string Text;
        }

        
    }
}
