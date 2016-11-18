using System;
using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public class UploadTableViewSource : UITableViewSource
    {
        public UploadTableViewSource()
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
            var cell = (UploadTableViewCell)tableView.DequeueReusableCell(UploadTableViewCell.Key);

            if (cell == null)
            {
                cell = UploadTableViewCell.Create();
            }

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }
    }
}

