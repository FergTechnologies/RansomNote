using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//taken from https://codereview.stackexchange.com/questions/85054/guessing-a-file-type-based-on-its-content
namespace FileTyper
{
    public class FileType
    {
        private static readonly FileType unknown = new FileType("unknown", string.Empty, new byte?[0]);

        private readonly string name;
        private readonly string extension;
        private readonly byte?[] magicSequence;
        private readonly int maximumStartIndex;

        public string Name { get { return name; } }

        public string Extension { get { return extension; } }

        public byte?[] MagicSequence { get { return magicSequence; } }

        public int MaximumStartLocation { get { return maximumStartIndex; } }

        public static FileType Unknown { get { return unknown; } }

        public FileType(string name, string extension, byte?[] magicSequence, int maximumStartIndex = 0)
        {
            this.name = name;
            this.extension = extension;
            this.magicSequence = magicSequence;
            this.maximumStartIndex = maximumStartIndex;
        }
    }
}
