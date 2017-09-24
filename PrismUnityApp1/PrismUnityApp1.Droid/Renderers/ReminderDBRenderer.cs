using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using PrismUnityApp1.Droid.Renderers;

[assembly: Dependency(typeof(ReminderDBRenderer))]
namespace PrismUnityApp1.Droid.Renderers
{
    public class ReminderDBRenderer : IReminder
    {
        public ReminderDBRenderer()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var filename = "ReminderItem.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, filename);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}