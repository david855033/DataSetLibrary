using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLibrary
{
    public class Criteria
    {
        private Dictionary<string, int> indexTable;
        int getIndex(string lookUpString)
        {
            return indexTable[lookUpString];
        }
        internal int fieldIndex;
        internal string shouldEqual;
        public Criteria(string field, string shouldEqual, Dictionary<string, int> indexTable)
        {
            this.indexTable = indexTable;
            this.fieldIndex = getIndex(field);
            this.shouldEqual = shouldEqual;
        }
        public Criteria(int fieldIndex, string shouldEqual, Dictionary<string, int> indexTable)
        {
            this.indexTable = indexTable;
            this.fieldIndex = fieldIndex;
            this.shouldEqual = shouldEqual;
        }
    }
}
