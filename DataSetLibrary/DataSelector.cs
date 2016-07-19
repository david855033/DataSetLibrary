using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLibrary
{
    public static class DataSelector
    {
        public static DataSet select(this DataSet originDataSet, List<Criteria> includeCriteria, List<Criteria> excludeCriteria)
        {
            DataSet destineDataSet = new DataSet();
            destineDataSet.indexTable = originDataSet.indexTable;
            foreach (var row in originDataSet.dataRow)
            {
                bool isMatched = false;
                if (includeCriteria != null && includeCriteria.Count!=0)
                {
                    foreach (var c in includeCriteria)
                    {
                        if (c.shouldEqual.EndsWith("*"))
                        {
                            string shouldEqual = c.shouldEqual.TrimEnd('*');
                            if (row[c.fieldIndex].StartsWith(shouldEqual))
                            {
                                isMatched = true;
                                continue;
                            }
                        }
                        else
                        {
                            if (row[c.fieldIndex] == c.shouldEqual)
                            {
                                isMatched = true;
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    isMatched = true;
                }
                if (isMatched)
                {
                    if (excludeCriteria != null && excludeCriteria.Count!=0)
                    {
                        foreach (var c in excludeCriteria)
                        {
                            if (c.shouldEqual.EndsWith("*"))
                            {
                                string shouldEqual = c.shouldEqual.TrimEnd('*');
                                if (row[c.fieldIndex].StartsWith(shouldEqual))
                                {
                                    isMatched = false;
                                    continue;
                                }
                            }
                            else
                            {
                                if (row[c.fieldIndex] == c.shouldEqual)
                                {
                                    isMatched = false;
                                    continue;
                                }
                            }
                        }
                    }
                }
                if (isMatched)
                {
                    destineDataSet.addDataRow(row);
                }
            }
            Console.WriteLine($"select {originDataSet.dataRow.Count()} into {destineDataSet.dataRow.Count()} data");
            return destineDataSet;
        }
        public static DataSet select(this DataSet originDataSet, Criteria includeCriteria, List<Criteria> excludeCriteria)
        {
            return originDataSet.select(new List<Criteria>() { includeCriteria }, excludeCriteria);
        }
        public static DataSet select(this DataSet originDataSet, List<Criteria> includeCriteria, Criteria excludeCriteria)
        {
            return originDataSet.select(includeCriteria, new List<Criteria>() { excludeCriteria });
        }
        public static DataSet select(this DataSet originDataSet, Criteria includeCriteria, Criteria excludeCriteria)
        {
            return originDataSet.select(new List<Criteria>() { includeCriteria }, new List<Criteria>() { excludeCriteria });
        }
        public static DataSet selectIn(this DataSet originDataSet, DataSet compareDataSet, IEnumerable<string> ThisColumnToMatch, IEnumerable<string> CompareColumnToMatch)
        {
            var destineDataSet = new DataSet();
            destineDataSet.copyIndexFromDataSet(originDataSet);

            List<string> matchList = new List<string>();

            foreach (var row in compareDataSet.dataRow)
            {
                string matchstring = "";
                foreach (var c in CompareColumnToMatch)
                {
                    matchstring += row[compareDataSet.getIndex(c)];
                }
                int index = matchList.BinarySearch(matchstring);
                if (index < 0) matchList.Insert(~index, matchstring);
            }

            foreach (var row in originDataSet.dataRow)
            {
                string matchstring = "";
                foreach (var c in ThisColumnToMatch)
                {
                    matchstring += row[originDataSet.getIndex(c)];
                }
                int index = matchList.BinarySearch(matchstring);
                if (index >= 0)
                {
                    destineDataSet.addDataRow(row);
                }
            }
            Console.WriteLine($"oringin group: {originDataSet.dataRow.Count}, in compare group : {compareDataSet.dataRow.Count}, => select {destineDataSet.dataRow.Count}");
            return destineDataSet;
        }

        public static DataSet selectNotIn(this DataSet originDataSet, DataSet compareDataSet, IEnumerable<string> ThisColumnToMatch, IEnumerable<string> CompareColumnToMatch)
        {
            var destineDataSet = new DataSet();
            destineDataSet.copyIndexFromDataSet(originDataSet);

            List<string> matchList = new List<string>();

            foreach (var row in compareDataSet.dataRow)
            {
                string matchstring = "";
                foreach (var c in CompareColumnToMatch)
                {
                    matchstring += row[compareDataSet.getIndex(c)];
                }
                int index = matchList.BinarySearch(matchstring);
                if (index < 0) matchList.Insert(~index, matchstring);
            }

            foreach (var row in originDataSet.dataRow)
            {
                string matchstring = "";
                foreach (var c in ThisColumnToMatch)
                {
                    matchstring += row[originDataSet.getIndex(c)];
                }
                int index = matchList.BinarySearch(matchstring);
                if (index < 0)
                {
                    destineDataSet.addDataRow(row);
                }
            }
            Console.WriteLine($"oringin group: {originDataSet.dataRow.Count}, NOT in compare group : {compareDataSet.dataRow.Count} => select {destineDataSet.dataRow.Count}");
            return destineDataSet;
        }
    }
}
