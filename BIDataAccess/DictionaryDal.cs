using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class DictionaryDal
    {
        public List<Dictonary> QueryDictionary(string type)
        {
            try
            {
                using (var db = BatteryDBContext.GetConnect())
                {
                    return db.Dictonary.Where(s => s.Type == type).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Dictonary>();
            }
        }
    }
}
