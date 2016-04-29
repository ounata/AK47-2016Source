using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MCS.Library.Core;
using MCS.Library.SOA.DocServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.Office.OpenXml.Word
{
    public abstract class GeneralDataProcessor
    {
        protected WordprocessingDocument document;

        protected DCTDataProperty dataProperty;

        public GeneralDataProcessor(WordprocessingDocument document, DCTDataProperty dataProperty)
        {
            this.document = document;
            this.dataProperty = dataProperty;
        }

        public abstract void Process();

        public static GeneralDataProcessor CreateProcessor(WordprocessingDocument document, DCTDataProperty property)
        {
            if (property is DCTSimpleProperty)
                return new SimplePropertyProcessor(document, (DCTSimpleProperty)property);

            return new ComplexPropertyProcessor(document, (DCTComplexProperty)property);
        }

        protected string[] GetMultiRowsValues(DCTSimpleProperty property)
        {
            string[] rows = null;

            if (property.Value != null)
            {
                string template = "{0}";

                if (property.FormatString.IsNotEmpty())
                    template = "{0:" + property.FormatString + "}";

                string strAlltext = string.Format(template, property.Value);
                string[] splitArray = new string[] { "\r\n" };
                rows = strAlltext.Split(splitArray, StringSplitOptions.None);
            }
            else
                rows = StringExtension.EmptyStringArray;

            return rows;
        }

        protected void FillText(ref Run runElement, string strFormat)
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
        }
    }
}
