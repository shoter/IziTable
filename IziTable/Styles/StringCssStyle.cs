using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziTable.Styles
{
    public class StringCssStyle : ICssStyle
    {
        private readonly string style;
        public StringCssStyle(string style)
        {
            this.style = style;
        }
        public string GetStyle()
        {
            return style;
        }
    }
}
