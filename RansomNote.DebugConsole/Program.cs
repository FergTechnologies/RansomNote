using System;
using RansomNote.Imaging.Extensions;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace RansomNote.DebugConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			
			var version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			Console.WriteLine($"v {version}");
			RunRegistration(args);

		}


		private static void RunRegistration(string[] args)
		{
			Console.WriteLine($"Opening image at path {args[0]}");
			var img = Image.FromFile(args[0]);
			img = img.ToPixelFormat(PixelFormat.Format24bppRgb);
			Console.WriteLine("Converting to Grayscale...");
			img = img.Grayscale();
			Console.WriteLine("Binaraizing...");
			img = img.Threshold();
			
			Console.WriteLine("Please specify a full file path to save the registered Image to.");
			var savePath = Console.ReadLine();
			Console.WriteLine($"Saving Registered Image To {savePath}...");
			img.Save(savePath);
			Console.WriteLine("Doing Edge Detections...");
			var cannImg = img.Canny();
			var kirschImg = img.Kirsch();
			var sobelImg = img.Sobel();
			var robinsonImg = img.Robinson();
			Console.WriteLine("Please specify a path to save the 4 edge Images.");
			var fld = Console.ReadLine();
			cannImg.Save($@"{fld}\canny.png", ImageFormat.Png);
			kirschImg.Save($@"{fld}\kirsch.png", ImageFormat.Png);
			sobelImg.Save($@"{fld}\sobel.png", ImageFormat.Png);
			robinsonImg.Save($@"{fld}\robinson.png", ImageFormat.Png);
			Console.WriteLine("Edge images saved.");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
