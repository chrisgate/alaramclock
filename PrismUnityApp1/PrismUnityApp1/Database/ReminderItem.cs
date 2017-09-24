using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismUnityApp1.Database
{
    public class ReminderItemDB
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Task { get; set; }
        public DateTime dateTime { get; set; }
        



    }
}
