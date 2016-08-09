using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLibrary
{
    public static class DataJoiner
    {
        static public DataSet joinData(this DataSet originDataSet, DataSet joinDataSet)
        {
            DataSet destineDataSet = new DataSet();
            if (originDataSet.indexTable.Count > 0)
            {
                destineDataSet.copyIndexFromDataSet(originDataSet);
            }
            else
            {
                destineDataSet.copyIndexFromDataSet(joinDataSet);
            }

            foreach (var row in originDataSet.dataRow)
            {
                destineDataSet.addDataRow(row);
            }
            foreach (var row in joinDataSet.dataRow)
            {
                destineDataSet.addDataRow(row);
            }
            Console.WriteLine($"data join: {originDataSet.dataRow.Count} + {joinDataSet.dataRow.Count} => {destineDataSet.dataRow.Count}");
            return destineDataSet;
        }
    }
}
