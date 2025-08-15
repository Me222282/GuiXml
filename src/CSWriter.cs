using System;
using System.IO;
using System.Linq;

namespace GuiXml
{
    public class CSWriter : StreamWriter
    {
        public CSWriter(Stream stream)
            : base(stream)
        {
            
        }
        
        private int _indentation = 0;
        public int Spaces { get; set; } = 4;
        private bool _nl = false;
        private bool _sc = false;
        
        private void AddIndentationNL()
        {
            // new line
            base.Write(CoreNewLine, 0, CoreNewLine.Length);
            
            int size = Spaces * _indentation;
            char[] buffer = Enumerable.Repeat(' ', size).ToArray();
            base.Write(buffer, 0, size);
        }
        // ending semicolon
        private void AddSC() => base.Write([';'], 0, 1);
        
        public void OpenContext()
        {
            if (_nl) { AddIndentationNL(); }
            _sc = false;
            base.Write(['{'], 0, 1);
            _indentation++;
            AddIndentationNL();
            _nl = false;
        }
        public void CloseContext()
        {
            _indentation--;
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
            }
            _sc = false;
            base.Write(['}'], 0, 1);
            _nl = true;
        }
        
        public override void Write(char[] buffer, int index, int count)
        {
            ReadOnlySpan<char> span = buffer.AsSpan(index, count);
            
            // add new line from previous
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
            }
            
            int nls = CoreNewLine.Length;
            int nl = span.IndexOf(CoreNewLine);
            int last = 0;
            while (true)
            {
                if (nl != 0)
                {
                    base.Write(buffer, last, nl);
                    AddSC();
                }
                
                last += nl + nls;
                nl = span.Slice(nl + nls).IndexOf(CoreNewLine);
                
                // continue whilst there are still new lines with content after
                if (nl >= 0 && buffer.Length != last + nl + nls)
                {
                    AddIndentationNL();
                    continue;
                }
                
                break;
            }
            
            // last new line is end of buffer
            if (nl >= 0)
            {
                _nl = true;
                _sc = false;
                return;
            }
            
            _nl = false;
            
            int left = buffer.Length - last;
            if (left != 0)
            {
                base.Write(buffer, last, left);
                _sc = true;
                return;
            }
            
            _sc = false;
        }
    }
}