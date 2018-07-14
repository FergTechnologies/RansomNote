using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RansomNote.Imaging.Extensions
{
	public static class RegistrationExtensions
	{
		public static Image Grayscale(this Image img) => new Grayscale(0.2125, 0.7154, .0721).Apply((Bitmap)img);
		public static Image Threshold(this Image img, int Threshold = 100) => new Threshold(Threshold).Apply((Bitmap)img);
		public static Image OtsuThreshold(this Image img) => new OtsuThreshold().Apply((Bitmap)img);
		public static Image Invert(this Image img) => new Invert().Apply((Bitmap)img);



	}
}
