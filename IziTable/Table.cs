using IziTable.Styles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IziTable.Extensions;

namespace IziTable
{
    public class Table
    {
        private List<string> headerColumns = new List<string>();
        private List<string[]> rows = new List<string[]>();

        private int headersCount => headerColumns.Count;
        private int rowsCount => rows.Count;

        private ICssStyle style;


        public Table AddHeader(string headerName)
        {
            if (rowsCount != 0)
                throw new Exception("Cannot add more columns when rows are defined");

            headerColumns.Add(headerName);

            return this;
        }

        public Table AddRow(params object[] contents)
        {
            if (headersCount != contents.Count())
                throw new Exception("Row columns must equal header columns count");

            var row = new string[headersCount];

            for (int i = 0; i < row.Count(); ++i)
            {
                row[i] = contents[i]?.ToString() ?? "";
            }

            rows.Add(row);

            return this;
        }

        /// <summary>
        /// Sets CSS style that will be inserted before table tag.
        /// </summary>
        public Table SetStyle(ICssStyle style)
        {
            this.style = style;
            return this;
        }

        public StringBuilder GenerateTable()
        {
            StringBuilder content = new StringBuilder();

            if (style != null)
                content.Append(renderStyle(style));
            content.Append(renderTable(headerColumns, rows));

            return content;
        }

        private static StringBuilder renderStyle(ICssStyle style) => new StringBuilder($"<style  type=\"text/css\">{style.GetStyle()}</style>");


        protected virtual StringBuilder renderTable(List<string> headers, List<string[]> rows)
        {
            StringBuilder table = new StringBuilder();

            table.Append("<table>");

            table.Append(renderHeader(headers));
            table.Append(renderBody(rows));

            table.Append("</table>");

            return table;
        }

        protected virtual StringBuilder renderHeader(List<string> headers)
        {
            if (headers.Count == 0)
                return null;

            StringBuilder header = new StringBuilder();

            header.Append("<thead><tr>");

            foreach (var column in headers)
                header.Append($"<th>{column}</th>");

            header.Append("</thead></tr>");

            return header;
        }

        protected virtual StringBuilder renderBody(List<string[]> rows)
        {
            if (rows.Count == 0)
                return null;

            StringBuilder body = new StringBuilder();

            body.Append("<tbody>");
            foreach (var row in rows)
                body.Append(renderRow(row));
            body.Append("</tbody>");

            return body;
        }

        protected virtual StringBuilder renderRow(string[] row)
        {
            StringBuilder content = new StringBuilder();

            content.Append("<tr>");
            foreach (var str in row)
                content.Append($"<td>{str}</td>");
            content.Append("</tr>");

            return content;
        }
    }
}
