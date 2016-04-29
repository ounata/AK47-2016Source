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
    public class SimplePropertyProcessor : GeneralDataProcessor
    {
        protected DCTSimpleProperty DataProperty
        {
            get
            {
                return (DCTSimpleProperty)dataProperty;
            }
        }

        public SimplePropertyProcessor(WordprocessingDocument document, DCTSimpleProperty dataProperty)
            : base(document, dataProperty)
        {
        }

        public override void Process()
        {
            DCTSimpleProperty property = DataProperty as DCTSimpleProperty;
            if (null != property)
            {
                var containerElement = document.MainDocumentPart.Document.Body
                .Descendants<SdtElement>().Where(o => o.SdtProperties.Descendants<SdtAlias>().Any(a => a.Val == DataProperty.TagID)).FirstOrDefault();
                if (null == containerElement)
                    return;

                object text = property.Value;

                var pas = containerElement.Descendants<Paragraph>().ToList();

                Paragraph p = pas.FirstOrDefault();
                if (p == null)
                {
                    var runElement = containerElement.Descendants<Run>().First();
                    FillText(ref runElement, GeneralFormatter.ToString(text, property.FormatString));
                    /*var runElement = containerElement.Descendants<Run>().First();
                    runElement.RemoveAllChildren();
                    runElement.AppendChild<Text>(new Text(GeneralFormatter.ToString(strAlltext, property.FormatString))); */
                }
                else
                {
                    string[] rows = GetMultiRowsValues(property);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (i == 0)
                        {
                            Run runElement = GetRunElement(p);
                            FillText(runElement, GeneralFormatter.ToString(rows[i], property.FormatString));
                        }
                        else
                        {
                            Paragraph addrow;
                            if (i < pas.Count)
                                addrow = pas[i];
                            else
                                addrow = p.CloneNode(true) as Paragraph;

                            Run addrunelement = GetRunElement(addrow);

                            FillText(addrunelement, GeneralFormatter.ToString(rows[i], property.FormatString));

                            if (i >= pas.Count)
                            {
                                var lastCon = containerElement.Descendants<Paragraph>().Last();
                                lastCon.InsertAfterSelf<Paragraph>(addrow);
                            }
                        }
                    }
                }

                if (property.IsReadOnly)
                {
                    Lock lockControl = new Lock();
                    lockControl.Val = LockingValues.SdtContentLocked;
                    containerElement.SdtProperties.Append(lockControl);
                }
            }
        }

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

        private void FillText(Run runElement, string strFormat)
        {
            if (runElement.HasChildren)
            {
                runElement.GetFirstChild<Text>().Text = strFormat;
            }
            else
            {
                runElement.AppendChild<Text>(new Text(strFormat));
            }
        }
    }
}
