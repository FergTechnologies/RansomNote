using Leptonica;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
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

        public void GetCharacterImages()
        {
            Out(new ExtractionEventArgs("Preparing to initialize tesseract."));
            var localDict = new Dictionary<char, Image>();
            var dPath = "./tessdata/";
            var lang = "eng";
            var api = new TessBaseAPI();
            OcrEngineMode oem = OcrEngineMode.TESSERACT_LSTM_COMBINED;
            PageSegmentationMode psm = PageSegmentationMode.SINGLE_CHAR;
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
            var symbCount = 0;
            var iterator = api.GetIterator().GetPageIterator();
            iterator.Begin();
            using (var iter = iterator)
            {
                do
                {
                    do
                    {
                        do
                        {
                            if (iter.IsAtBeginningOf(PageIteratorLevel.RIL_BLOCK))
                            {
                                // do whatever you need to do when a block (top most level result) is encountered.
                            }
                            if (iter.IsAtBeginningOf(PageIteratorLevel.RIL_PARA))
                            {
                                // do whatever you need to do when a paragraph is encountered.
                            }
                            if (iter.IsAtBeginningOf(PageIteratorLevel.RIL_TEXTLINE))
                            {
                                // do whatever you need to do when a line of text is encountered is encountered.
                            }
                            if (iter.IsAtBeginningOf(PageIteratorLevel.RIL_WORD))
                            {
                                // do whatever you need to do when a word is encountered is encountered.
                            }

                            // get bounding box for symbol

                            if (iter.BoundingBox(pageItLevel, out int left, out int top, out int right, out int bottom))
                            {
                                var rect = new Rectangle(left, top, right - left, bottom - top);
                                var img = Crop(ima, rect);
                                Out(new ExtractionEventArgs($"Cropped Character For Box {symbCount}."));
                                var path = $@"{_savePath}\eng.mycoiocr.{symbCount}.png";
                                img.Save(path, ImageFormat.Png);
                                Out(new ExtractionEventArgs($"Saving Cropped Image as {path}"));
                                symbCount++;
                            }
                        } while (iter.Next(PageIteratorLevel.RIL_WORD));
                    } while (iter.Next(PageIteratorLevel.RIL_TEXTLINE));
                } while (iter.Next(PageIteratorLevel.RIL_PARA));
            }
           
            Out(new ExtractionEventArgs($"Character Images Retrieved. Please see the folder at path {_savePath}."));

        }

        private Image Crop(Image img, Rectangle rect)
        {
            var bMap  = new Bitmap(rect.Width, rect.Height);
            using (var g = System.Drawing.Graphics.FromImage(bMap))
            {
                g.DrawImage(img, new Rectangle(0, 0, bMap.Width, bMap.Height), rect, GraphicsUnit.Pixel);
            }
            return bMap;
        }

        
    }
}
