using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuiXml
{
    public class CSWriter
    {
        public CSWriter(Stream stream)
        {
            _sw = new StreamWriter(stream);
        }
        
        private StreamWriter _sw;
        private int _indentation = 0;
        public int Spaces { get; set; } = 4;

        private bool _nl = false;
        private bool _sc = false;
        
        private void AddIndentationNL()
        {
            // new line
            _sw.Write(_sw.NewLine, 0, _sw.NewLine.Length);
            
            int size = Spaces * _indentation;
            char[] buffer = Enumerable.Repeat(' ', size).ToArray();
            _sw.Write(buffer, 0, size);
        }
        // ending semicolon
        private void AddSC() => _sw.Write([';'], 0, 1);
        
        public void OpenContext()
        {
            if (_nl) { AddIndentationNL(); }
            _sc = false;
            _sw.Write(['{'], 0, 1);
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
            _sw.Write(['}'], 0, 1);
            _nl = true;
        }
        
        public void CommentLine(string comment)
        {
            // no new lines
            if (comment.Contains('\n'))
            {
                throw new Exception("Comment cannot contain new line");
            }
            
            // add new line from previous
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
            }
            
            _nl = false;
            _sc = false;
            
            // comment prefix
            _sw.Write(['/', '/', ' '], 0, 3);
            // comment is just a writeline
            WriteLine(comment);
            // no need for semicolons
            _sc = false;
        }
        public void CommentLine()
        {
            // add new line from previous
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
            }
            
            _nl = false;
            _sc = false;
            
            // comment prefix
            _sw.Write(['/', '/', ' '], 0, 3);
            // comment is just a writeline
            WriteLine();
            // no need for semicolons
            _sc = false;
        }
        
        public void Write(ReadOnlySpan<char> span)
        {   
            // add new line from previous
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
            }
            
            int nls = _sw.NewLine.Length;
            int nl = span.IndexOf(_sw.NewLine);
            int last = 0;
            if (nl < 0 || span.Length == last + nl + nls)
            {
                goto SkipLoop;
            }
            
            while (true)
            {
                if (nl != 0)
                {
                    _sw.Write(span.Slice(last, nl));
                    AddSC();
                }
                
                last += nl + nls;
                nl = span.Slice(nl + nls).IndexOf(_sw.NewLine);
                
                // continue whilst there are still new lines with content after
                if (nl >= 0 && span.Length != last + nl + nls)
                {
                    AddIndentationNL();
                    continue;
                }
                
                break;
            }
        
        SkipLoop:
            // last new line is end of buffer
            if (nl >= 0)
            {
                _nl = true;
                _sc = false;
                return;
            }
            
            _nl = false;
            
            int left = span.Length - last;
            if (left != 0)
            {
                _sw.Write(span.Slice(last, left));
                _sc = true;
                return;
            }
            
            _sc = false;
        }
        public void WriteLine(ReadOnlySpan<char> span)
        {
            Write(span);
            WriteLine();
        }
        public void WriteLine()
        {
            if (_nl)
            {
                if (_sc) { AddSC(); }
                AddIndentationNL();
                _sc = false;
            }
            _nl = true;
        }
        
        public void Close()
        {
            _sw.Flush();
            _sw.Close();
        }
    }
}