using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Collections;

namespace PPTS.Services.UnionPay.ProcessCSVFile
{
    public static class AnalyzeCSVFile
    {
        /// <summary>
        /// 获取指定路径的文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static FileInfo[] GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            FileInfo[] files = directoryInfo.GetFiles("*.csv");

            return files;
        }

        private static void SortAsFilesCreationTimeDesc(ref FileInfo[] files)
        {
            Array.Sort(files, (FileInfo x, FileInfo y) => { return x.CreationTime.CompareTo(y.CreationTime); });
        }

        private static FileInfo GetFirstAsCreationTimeDesc(FileInfo[] files)
        {
            return files.FirstOrDefault();
        }

        public static string[][] ReadFile(string strConfigPath)
        {
            string path = ConfigurationManager.AppSettings[strConfigPath];
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            FileInfo[] files = GetFiles(path);

            SortAsFilesCreationTimeDesc(ref files);

            FileInfo file = GetFirstAsCreationTimeDesc(files);

            FileStream fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);

            //读取文件流，编码UTF-8
            StreamReader reader = new StreamReader(fileStream, Encoding.UTF8);

            string strCsv = reader.ReadToEnd();

            string[][] result = SplitCsv(strCsv);

            return result;
        }

        private static string[][] SplitCsv(string strCsv)
        {
            if (string.IsNullOrEmpty(strCsv))
            {
                return new string[0][] { };
            }
            ArrayList lines = new ArrayList();
            ArrayList cells = new ArrayList();
            StringBuilder stringBuilder = new StringBuilder();

            bool beginWithQuote = false;
            int maxColumns = 0;
            //双引号个数
            int QuoteNums = 0;

            for (int i = 0; i < strCsv.Length; i++)
            {
                char ch = strCsv[i];
                switch (ch)
                {
                    case '\r':
                        if (beginWithQuote)
                        {
                            stringBuilder.Append(ch);
                        }
                        else
                        {
                            if (i < strCsv.Length - 1 && strCsv[i + 1] == '\n')
                            {
                                ++i;
                            }
                            cells.Add(stringBuilder.ToString());
                            stringBuilder.Clear();
                            maxColumns = cells.Count > maxColumns ? cells.Count : maxColumns;
                            lines.Add(cells);
                            cells = new ArrayList();
                        }
                        break;
                    case '\n':
                        if (beginWithQuote)
                        {
                            stringBuilder.Append(ch);
                        }
                        else
                        {
                            if (i < strCsv.Length - 1 && strCsv[i + 1] == '\r')
                            {
                                ++i;
                            }
                            cells.Add(stringBuilder.ToString());
                            stringBuilder.Clear();
                            maxColumns = cells.Count > maxColumns ? cells.Count : maxColumns;
                            lines.Add(cells);
                            cells = new ArrayList();
                        }
                        break;
                    case ',':
                        if (beginWithQuote)
                        {
                            stringBuilder.Append(ch);
                        }
                        else
                        {
                            cells.Add(stringBuilder.ToString());
                            stringBuilder.Clear();
                        }
                        break;
                    case '\"':
                        ++QuoteNums;
                        if (beginWithQuote)
                        {
                            int j = i + 1;
                            if (j == strCsv.Length)
                            {
                                cells.Add(stringBuilder.ToString());
                                stringBuilder.Clear();
                            }
                            else
                            {
                                char chr = strCsv[j];
                                if (chr == ',' && QuoteNums % 2 == 0)
                                {
                                    i = j;
                                    cells.Add(stringBuilder.ToString());
                                    stringBuilder.Clear();
                                    beginWithQuote = false;
                                    QuoteNums = 0;
                                }
                                else
                                {
                                    stringBuilder.Append(ch);
                                }
                            }
                        }
                        else if (stringBuilder.Length == 0)
                        {
                            beginWithQuote = true;
                        }
                        else if (stringBuilder.Length < i + 1)
                        {
                            stringBuilder.Append(ch);
                        }
                        break;
                    default:
                        stringBuilder.Append(ch);
                        break;
                }
            }
            strCsv = strCsv.TrimEnd('\r', '\n');
            if (stringBuilder.Length > 0 && strCsv[strCsv.Length - 1] != '\"' && beginWithQuote)
            {
                throw new Exception("字符串未已正确方式结束：双引号开始，但未已双引号结束");
            }
            else
            {
                if (stringBuilder.Length > 0)
                {
                    cells.Add(stringBuilder.ToString());
                    maxColumns = cells.Count > maxColumns ? cells.Count : maxColumns;
                    lines.Add(cells);
                }
            }

            string[][] ret = new string[lines.Count][];
            for (int i = 0; i < lines.Count; i++)
            {
                cells = (ArrayList)lines[i];
                ret[i] = new string[maxColumns];
                for (int j = 0; j < maxColumns; j++)
                {
                    ret[i][j] = cells[j].ToString();
                }
            }
            return ret;
        }
    }
}