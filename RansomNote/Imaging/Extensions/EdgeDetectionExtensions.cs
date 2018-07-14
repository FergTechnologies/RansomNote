using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RansomNote.Imaging.Extensions
{
	public static class EdgeDetectionExtensions
	{
		public static Image Canny(this Image img) => new CannyEdgeDetector().Apply((Bitmap)img);
		public static Image Sobel(this Image img) => new SobelEdgeDetector().Apply((Bitmap)img);
		public static Image Kirsch(this Image img) => new KirschEdgeDetector().Apply((Bitmap)img);
		public static Image Robinson(this Image img) => new RobinsonEdgeDetector().Apply((Bitmap)img);

	}
}
