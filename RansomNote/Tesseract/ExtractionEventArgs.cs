using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RansomNote.Tesseract
{
    public class ExtractionEventArgs : EventArgs
    {
        public string EventMessage;
        public dynamic AdditionalObject;
        public ExtractionEventArgs(string EventMessage, dynamic AdditionalObject = null)
        {
            this.EventMessage = EventMessage;
            this.AdditionalObject = AdditionalObject;
        }
        public ExtractionEventArgs()
        {

        }
    }
}
