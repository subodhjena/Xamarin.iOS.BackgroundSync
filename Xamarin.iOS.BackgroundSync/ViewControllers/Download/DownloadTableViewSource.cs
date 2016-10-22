using System;
using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public class DownloadTableViewSource : UITableViewSource
    {
        public DownloadTableViewSource()
        {
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (DownloadTableViewCell)tableView.DequeueReusableCell(DownloadTableViewCell.Key);

            if (cell == null)
            {
                cell = DownloadTableViewCell.Create();
            }

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }
    }
}

