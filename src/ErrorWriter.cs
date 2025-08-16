using System;
using System.IO;
using System.Text;

namespace GuiXml
{
    public class ErrorWriter : TextWriter
    {
        public ErrorWriter(TextWriter destination)
        {
            _destination = destination;
        }
        
        private TextWriter _destination;
        
        public override void WriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _destination.WriteLine(value);
            Console.ResetColor();
        }
        
        public override Encoding Encoding => Encoding.Default;
    }
}