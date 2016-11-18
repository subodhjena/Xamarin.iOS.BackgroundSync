using System;
using Realms;

namespace Xamarin.iOS.BackgroundSync
{
    public class UploadModel : RealmObject
    {
        public UploadModel()
        {
        }

        [PrimaryKey]
        public String Id { get; set; }
        public int TaskIdentifier { get; set; }
        public String FilePath { get; set; }
        public double UploadPercentage { get; set; }
        public int Status { get; set;}
        public int SyncType { get; set; }
    }
}

