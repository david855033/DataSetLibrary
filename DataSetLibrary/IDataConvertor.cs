using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLibrary
{
    public interface IDaterConvertor
    {
        string getFieldContent(string[] row, Dictionary<string, int> index);
    }

}
