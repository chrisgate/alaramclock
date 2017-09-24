using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismUnityApp1.Database
{
    public class ReminderItemDatabase
    {
        private SQLiteConnection _connection;

        public ReminderItemDatabase()
        {
            _connection = DependencyService.Get<IReminder>().GetConnection();
            _connection.CreateTable<ReminderItemDB>();
        }

        public IEnumerable<ReminderItemDB> GetReminders()
        {
            return (from t in _connection.Table<ReminderItemDB>() select t).ToList();
        }

        public ReminderItemDB GetReminderItem(int id)
        {
            return (_connection.Table<ReminderItemDB>().FirstOrDefault(t => t.ID == id));
        }

        public void DeleteReminder(int id)
        {
            _connection.Delete<ReminderItemDB>(id);
        }

        public void AddThought(Model.ReminderItem item)
        {
            var newReminder = new ReminderItemDB
            {
                Task = item.Message,
                dateTime = item.RemindingTime
            };

            _connection.Insert(newReminder);
        }
    }
}
