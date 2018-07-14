using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RansomNote.Models
{
    public class TextImage
    {
		public readonly Image CharacterImage;
		public readonly char Character;

		public TextImage(Image Image, char Character)
		{
			CharacterImage = Image;
			this.Character = Character;
		}
    }
}
