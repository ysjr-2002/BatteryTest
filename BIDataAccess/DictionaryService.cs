using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class DictionaryService
    {
        public List<Dictonary> QueryDictionary(string type)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    return db.Dictonaries.Where(s => s.Type == type).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Dictonary>();
            }
        }
    }
}
