using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RansomNote.Imaging.Extensions
{
	public static class FormatExtensions
	{
		public static Image ToPixelFormat(this Image img, PixelFormat pixelFormat)
		{
			var clone = new Bitmap(img.Width, img.Height, pixelFormat);
			using (var g = Graphics.FromImage(clone))
			{
				g.DrawImage(img, new Rectangle(0, 0, clone.Width, clone.Height));
			}
			return clone;
		}
	}
}
