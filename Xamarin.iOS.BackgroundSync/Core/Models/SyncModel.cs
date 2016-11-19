using System;
using Realms;

namespace Xamarin.iOS.BackgroundSync
{
    public class SyncModel : RealmObject
    {
        public SyncModel()
        {
        }

        [PrimaryKey]
        public String Id { get; set; }
        public int? TaskIdentifier { get; set; }
        public String FilePath { get; set; }
        public double SyncProgress { get; set; }
        public int Status { get; set;}
        public int SyncType { get; set; }
    }
}

