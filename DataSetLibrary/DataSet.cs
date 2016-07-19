using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLibrary
{
    public class DataSet
    {
        public Dictionary<string, int> indexTable = new Dictionary<string, int>();
        public List<string[]> dataRow = new List<string[]>();
        public DataSet()
        {

        }
        public void copyIndexFromDataSet(DataSet dataSet)
        {
            indexTable = new Dictionary<string, int>();
            foreach (var d in dataSet.indexTable)
            {
                indexTable.Add(d.Key, d.Value);
            }
        }
        public void setIndexFromTitle(string title)
        {
            string[] splitline = title.Split('\t');
            for (int i = 0; i < splitline.Length; i++)
            {
                indexTable.Add(splitline[i], i);
            }
        }
        public void addDataRow(string line)
        {
            dataRow.Add(line.Split('\t'));
        }
        public void addDataRow(string[] line)
        {
            dataRow.Add(line);
        }
        public int getIndex(string lookUpString)
        {
            return indexTable[lookUpString];
        }
        public void ExportData(string path)
        {
            using (var sw = new StreamWriter(path, false, Encoding.Default))
            {
                StringBuilder Content = new StringBuilder();
                StringBuilder title = new StringBuilder();
                foreach (var i in indexTable)
                {
                    title.Append(i.Key + "\t");
                }
                Content.AppendLine(title.ToString().TrimEnd('\t'));
                foreach (var r in dataRow)
                {
                    StringBuilder row = new StringBuilder();
                    foreach (var c in r)
                    {
                        row.Append(c + "\t");
                    }
                    Content.AppendLine(row.ToString().TrimEnd('\t'));
                }
                sw.Write(Content);
                Console.WriteLine($"Export {dataRow.Count} Data to {path}");
            }
        }
        public string outputByField(List<string> fields)
        {
            StringBuilder title = new StringBuilder();
            List<string> toRemove = new List<string>();
            foreach (var s in fields)
            {
                if (indexTable.ContainsKey(s))
                {
                    title.Append(s + "\t");
                }
                else
                {
                    toRemove.Add(s);
                }
            }
            fields.RemoveAll(x => toRemove.Contains(x));
            List<int> indexs = new List<int>();
            foreach (var s in fields)
            {
                indexs.Add(getIndex(s));
            }
            StringBuilder content = new StringBuilder();
            content.AppendLine(title.ToString().Trim('\t'));
            foreach (var row in dataRow)
            {
                StringBuilder line = new StringBuilder();
                foreach (var i in indexs)
                {
                    line.Append(row[i] + "\t");
                }
                content.AppendLine(line.ToString().Trim('\t'));
            }
            return content.ToString();
        }
        public string addField(IDaterConvertor dataConvertor, string fieldname)
        {
            if (indexTable.ContainsKey(fieldname))
                return fieldname;
            indexTable.Add(fieldname, dataRow.First().Count());
            List<string[]> newDataRow = new List<string[]>();
            foreach (var row in dataRow)
            {
                string[] newRow = new string[row.Length + 1];
                for (int i = 0; i < row.Length; i++)
                {
                    newRow[i] = row[i];
                }
                newRow[newRow.Length - 1] = dataConvertor.getFieldContent(row, indexTable);
                newDataRow.Add(newRow);
            }
            dataRow = newDataRow;
            return fieldname;
        }
    }
}
