using HtmlAgilityPack;
using IziTable.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IziTable.Tests
{
    public class IziTableTests
    {
        
        [Fact]
        public void GenerateTable_EmptyTable_OnlyTableTag()
        {
            Table table = new Table();
            var mainNode = getTableNode(table);

            Assert.Equal("table", mainNode.Name);
            Assert.Empty(mainNode.ChildNodes);
        }


        [Fact]
        public void GenerateTable_HeaderOnly_NoTbody()
        {
            Table table = new Table()
                .AddHeader("test header");

            var mainNode = getTableNode(table);

            Assert.Single(mainNode.ChildNodes);
            Assert.Equal("thead", mainNode.ChildNodes[0].Name);
        }

        [Theory]
        [InlineData("HeaderOne")]
        [InlineData("HeaderOne", "HeaderTwo")]
        [InlineData("HeaderOne", "HeaderTwo", "HeaderThree")]
        public void GenerateTable_AddingHeader_CorrectHeaders(params string[] headers)
        {
            Table table = new Table();
            foreach (var h in headers)
                table.AddHeader(h);

            var node = getTableNode(table);
            var thead = node.ChildNodes["thead"];
            var tr = thead.ChildNodes["tr"];

            for (int i = 0; i < headers.Length; ++i)
            {
                var header = headers[i];
                var child = tr.ChildNodes[i];
                Assert.Equal("th", child.Name);
                Assert.Equal(header, child.InnerText);
            }
        }

        [Fact]
        public void GenerateTable_AddRowWithoutHeader_Exception()
        {
            var table = new Table();

            Assert.Throws<Exception>(() => table.AddRow("testtt"));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 10)]
        public void GenerateTable_AddRowsWithOtherElementCountThanHeaderCount_Exception(int headerCount, int elementsInRowCount)
        {
            var table = new Table();

            for (int i = 0; i < headerCount; ++i)
                table.AddHeader("head");
            object[] elements = new object[elementsInRowCount];
        
            Assert.Throws<Exception>(() => table.AddRow(elements));
        }

        [Fact]
        public void GenerateTable_AddRow_CorrectRow()
        {
            string[] row = new string[] { "one", "two", "three" };

            var table = new Table();
            table.AddHeader("1").AddHeader("2").AddHeader("3");

            table.AddRow(row);

            var node = getTableNode(table);
            var tr = node.QuerySelector("tbody tr");

            for (int i = 0; i < row.Length; ++i)
            {
                var td = tr.ChildNodes[i];

                Assert.Equal("td", td.Name);
                Assert.Equal(row[i], td.InnerText);
            }
        }

        [Fact]
        public void GenerateTable_AddedStyle_Exists()
        {
            var table = new Table()
                .SetStyle(new StringCssStyle(""));

            var node = getMainNode(table);

            var style = node.ChildNodes["style"];

            Assert.NotNull(style);
            Assert.Equal(string.Empty, style.InnerHtml);
        }

        [Fact]
        public void GenerateTable_AddedStyle_CorrectElementOrder()
        {
            var table = new Table()
                .SetStyle(new StringCssStyle(""));

            var node = getMainNode(table);

            var style = node.ChildNodes[0];
            var t = node.ChildNodes[1];

            Assert.Equal("style", style.Name);
            Assert.Equal("table", t.Name);
        }

        [Fact]
        public void GenerateTable_StyleContent_InsideStyleTag()
        {
            string color = "color:black";
            var table = new Table()
                .SetStyle(new StringCssStyle(color));

            var node = getMainNode(table);

            var style = node.ChildNodes["style"];

            Assert.Equal(color, style.InnerHtml);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        public void GenerateTable_AddRowMultiple_CorrectNumberOfRows(int numberOfRows)
        {
            var table = new Table().AddHeader("a");
            for (int i = 0; i < numberOfRows; ++i)
                table.AddRow("content");

            var node = getTableNode(table);
            var rows = node.QuerySelectorAll("tbody tr");

            Assert.Equal(numberOfRows, rows.Count);
        }

        private static HtmlNode getTableNode(Table table)
        {
            return getMainNode(table).ChildNodes[0];
        }

        private static HtmlNode getMainNode(Table table)
        {
            return getMainNode(table.GenerateTable().ToString());
        }


        private static HtmlNode getMainNode(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.DocumentNode;
        }
    }
}
