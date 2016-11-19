using System;
using System.IO;

using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public static class Utilities
    {
        public static string BackgroundSyncHome()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var directoryname = Path.Combine(documents, "BackgroundSync");

            if (!Directory.Exists(directoryname))
            {
                Directory.CreateDirectory(directoryname);
            }

            return directoryname;
        }

        public static string BackgroundSyncFilePath()
        {
            var directoryname = Path.Combine(BackgroundSyncHome(), "BackgroundSyncFiles");

            if (!Directory.Exists(directoryname))
            {
                Directory.CreateDirectory(directoryname);
            }

            return directoryname;
        }
    }
}

