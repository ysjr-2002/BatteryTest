using BIDataAccess;
using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILogic
{
    public class DictonaryService
    {
        public List<Dictonary> QueryDictionary(string type)
        {
            return new DictionaryDal().QueryDictionary(type);
        }
    }
}
