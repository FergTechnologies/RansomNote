using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using iTextSharp.text.pdf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RansomNote.PDF
{
    public class PDFProcessorEventArgs : EventArgs
    {
        public string message;
    }
    public class PDFProcessor
    {

        public event EventHandler<PDFProcessorEventArgs> Out;
        protected virtual void WriteOut(PDFProcessorEventArgs e) => Out?.Invoke(this, e);
        private readonly byte[] _pdfBytes;
        public Stream PDFStream => new MemoryStream(_pdfBytes);

        public PDFProcessor(byte[] PDFBytes)
        {
            _pdfBytes = PDFBytes;
        }

        public Dictionary<int, Image> PDFToImage(int dpi)
        {
            var pgs = new ConcurrentDictionary<int, Image>();
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var guidPath = $@"{path}\PDF\gs\{Guid.NewGuid().ToString()}.dll";
            File.Copy($@"{path}\PDF\gs\gsdll64.dll", guidPath);
            GhostscriptRasterizer rasterizer = null;
            GhostscriptVersionInfo version = loadGhostScript(guidPath);
            var pages = 0;
            using (rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(PDFStream, version, false);
                pages = rasterizer.PageCount;
                rasterizer.Close();
            }
            File.Delete(guidPath);
            Parallel.For(0, pages, (i) =>
            {
                pgs.AddOrUpdate(i + 1, PDFPageToImage(dpi, i + 1), (a, b) => PDFPageToImage(dpi, i + 1));
            });
            return pgs.OrderBy(a => a.Key).ToDictionary(a => a.Key, a => a.Value);
        }

        public Image PDFPageToImage(int dpi, int pageNumber)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var guidPath = $@"{path}\{Guid.NewGuid().ToString()}.dll";
            File.Copy($@"{path}\gsdll64.dll", guidPath);
            GhostscriptRasterizer rasterizer = null;
            GhostscriptVersionInfo version = loadGhostScript(guidPath);
            using (rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(PDFStream, version, false);
                var result = rasterizer.GetPage(dpi, dpi, pageNumber);
                rasterizer.Close();
                File.Delete(guidPath);
                return result;
            }
        }

        const string pdfTextRegex = @"(\()([\s\S]*?)(\) Tj)";
        public string GetPageText(int pageNumber)
        {
            try
            {
                var reader = new PdfReader(_pdfBytes);
                var matches = Regex.Matches(Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(pageNumber))), pdfTextRegex);
                var mStrings = new ConcurrentDictionary<int, string>();
                Parallel.For(0, matches.Count - 1, (i) =>
                {
                    var strVal = matches[i].ToString();
                    var val = strVal.Substring(1, strVal.Length - 1).Replace(") Tj", "");
                    mStrings.AddOrUpdate(i, val, (a, b) => val);
                });
                return string.Join(" ", mStrings.OrderBy(a => a.Key).Select(a => a.Value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public Dictionary<int, Image> ParsePDF()
        {
            var results = new ConcurrentDictionary<int, Image>();

            var numPages = new PdfReader(_pdfBytes).NumberOfPages;
            Parallel.For(0, numPages, (i) =>
            {
                WriteOut(new PDFProcessorEventArgs
                {
                    message = $"Parsing Page {i + 1}."
                });
                using (var ms = new MemoryStream())
                {
                    var _reader = new PdfReader(_pdfBytes);
                    _reader.SelectPages((i + 1).ToString());
                    var stamper = new PdfStamper(_reader, ms);
                    if (stamper != null)
                    {
                        stamper.Close();
                    }
                    _reader.Close();
                    var pageBytes = ms.ToArray();
                    var content = PDFPageToImage(300, i + 1);

                    results.AddOrUpdate(i + 1, content, (a, b) => content);
                }
            });
            return results.ToDictionary(a => a.Key, a => a.Value);
        }

        private static Ghostscript.NET.GhostscriptVersionInfo loadGhostScript(string loadPath) => new Ghostscript.NET.GhostscriptVersionInfo(new System.Version(0, 0, 0), loadPath, string.Empty, Ghostscript.NET.GhostscriptLicense.GPL);
    }
}
