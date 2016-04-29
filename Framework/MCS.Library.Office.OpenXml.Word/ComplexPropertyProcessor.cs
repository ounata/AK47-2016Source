using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MCS.Library.SOA.DocServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Office.OpenXml.Word
{
    public class ComplexPropertyProcessor : GeneralDataProcessor
    {
        protected DCTComplexProperty DataProperty
        {
            get
            {
                return (DCTComplexProperty)dataProperty;
            }
        }

        public ComplexPropertyProcessor(WordprocessingDocument document, DCTComplexProperty dataProperty)
            : base(document, dataProperty)
        {
        }

        public override void Process()
        {
            var titleRow = document.MainDocumentPart.Document.Body
                .Descendants<TableRow>().Where(o => o.Descendants<BookmarkStart>().Any(mark => mark.Name == DataProperty.TagID)).FirstOrDefault();

            var sdtElements = titleRow.Descendants<SdtElement>().Where(o => o.SdtProperties.Descendants<SdtAlias>().Any(a => a.Val != null)).ToList();

            var bookmarkStart = titleRow.Descendants<BookmarkStart>().Where(o => o.Name == DataProperty.TagID).FirstOrDefault();

            List<string> properties = new List<string>();
            for (int i = 0; i < sdtElements.Count; i++)
            {
                properties.Add(sdtElements[i].Descendants<SdtAlias>().FirstOrDefault().Val.Value);
            }

            TableRow curRow = titleRow;

            foreach (DCTWordDataObject wordDataObj in DataProperty.DataObjects)
            {
                TableRow newRow = curRow.InsertAfterSelf<TableRow>((TableRow)curRow.NextSibling<TableRow>().CloneNode(true));

                TableCell firstCell = newRow.GetFirstChild<TableCell>();
                int columnFirst = Int32Value.ToInt32(bookmarkStart.ColumnFirst);
                //== default(Int32Value) ? bookmarkStart.ColumnFirst.Value : 0;
                int columnLast = Int32Value.ToInt32(bookmarkStart.ColumnLast);

                for (int j = columnFirst; j <= columnLast; j++)
                {
                    DCTDataProperty dataProperty = wordDataObj.PropertyCollection[properties[j - columnFirst]];

                    if (dataProperty is DCTSimpleProperty)
                    {
                        DCTSimpleProperty simpleProperty = dataProperty as DCTSimpleProperty;
                        TableCell cell = GetCellByIndex(firstCell, j);
                        Paragraph p = cell.GetFirstChild<Paragraph>();

                        string[] rows = GetMultiRowsValues(simpleProperty);

                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (i == 0)
                            {
                                Run runElement = GetRunElement(p);
                                FillText(ref runElement, GeneralFormatter.ToString(rows[i], simpleProperty.FormatString));
                            }
                            else
                            {
                                Paragraph addrow = p.CloneNode(true) as Paragraph;
                                Run addrunelement = GetRunElement(addrow);
                                //addrow.GetFirstChild<Run>();
                                FillText(ref addrunelement, GeneralFormatter.ToString(rows[i], simpleProperty.FormatString));
                                p.InsertAfterSelf<Paragraph>(addrow);
                            }
                        }
                    }
                }

                curRow = newRow;
            }
        }

        private TableCell GetCellByIndex(TableCell firstCell, int index)
        {
            TableCell result = firstCell;
            for (int i = 0; i < index; i++)
            {
                result = result.NextSibling<TableCell>();
            }
            return result;
        }

        /*
        private void FillText(ref Run runElement, string strFormat)
        {
            if (runElement.HasChildren)
            {
                runElement.GetFirstChild<Text>().Text = strFormat;
                //runElement.RemoveAllChildren<Text>();
                //runElement.AppendChild<Text>(new Text(GeneralFormatter.ToString(rows[i], property.FormatString)));
            }
            else
            {
                runElement.AppendChild<Text>(new Text(strFormat));
            }
        } */

        private Run GetRunElement(Paragraph p)
        {
            var runs = p.Descendants<Run>();
            Run runElement = null;

            if (runs.Count() > 0)
            {
                runElement = p.Descendants<Run>().First();
            }
            else
            {
                runElement = p.AppendChild<Run>(new Run());
            }

            return runElement;
        }
    }
}
