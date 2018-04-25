using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziTable.Styles
{
    public class FileCssStyle : ICssStyle
    {
        public string Filepath { get; private set; }
        public FileCssStyle(string filePath)
        {
            Filepath = filePath;
        }
        public FileCssStyle(params string[] filePath)
        {
            Filepath = Path.Combine(filePath);
        }
        public bool Exists()
        {
            return File.Exists(Filepath);
        }
        public string GetStyle()
        {
            using (var reader = new StreamReader(File.Open(Filepath, FileMode.Open)))
            {
                var style = reader.ReadToEnd();
                style = eraseNewLines(style);
                return style;
            }
        }

        private static string eraseNewLines(string style)
        {
            return style.Replace(Environment.NewLine, "");
        }
    }
}
