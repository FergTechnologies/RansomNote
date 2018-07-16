using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RansomNote.GUI
{
    public class TextBoxStreamWriter : TextWriter
    {
        [DllImport("user64.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
            _output.ReadOnly = true;
            _output.GotFocus += _output_GotFocus;
            _output.Cursor = Cursors.Arrow;
        }

        private void _output_GotFocus(object sender, EventArgs e)
        {
            HideCaret(_output.Handle);
        }



        public override void WriteLine(string text)
        {
            _output.InvokeIfRequired(() =>
            {
                _output.AppendText(_output.Text.Length == 0 ? text : $"\r\n{text}");
            });
            
        }
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
