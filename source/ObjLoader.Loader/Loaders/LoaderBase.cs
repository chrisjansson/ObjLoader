using System;
using System.IO;

namespace ObjLoader.Loader.Loaders
{
    public abstract class LoaderBase
    {
        private StreamReader _lineStreamReader;

        public void Load(Stream lineStream)
        {
            _lineStreamReader = new StreamReader(lineStream);

            BeforeLoad();

            while (!_lineStreamReader.EndOfStream)
            {
                ParseLine();
            }

            AfterLoad();
        }

        private void ParseLine()
        {
            var currentLine = _lineStreamReader.ReadLine();

            if (string.IsNullOrEmpty(currentLine) || currentLine[0] == '#')
            {
                return;
            }

            var fields = currentLine.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            var keyword = fields[0];
            var data = fields[1];

            ParseLine(keyword, data);
        }

        protected abstract void BeforeLoad();
        protected abstract void ParseLine(string keyword, string data);
        protected abstract void AfterLoad();
    }
}