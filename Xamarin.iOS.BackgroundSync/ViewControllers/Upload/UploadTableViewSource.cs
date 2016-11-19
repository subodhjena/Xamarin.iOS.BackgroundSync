using System;
using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public class UploadTableViewSource : UITableViewSource
    {
        private UploadViewController uploadViewController;

        public UploadTableViewSource(UploadViewController uploadViewController)
        {
            this.uploadViewController = uploadViewController;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return uploadViewController.SortedUploads.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (UploadTableViewCell)tableView.DequeueReusableCell(UploadTableViewCell.Key);

            if (cell == null)
            {
                cell = UploadTableViewCell.Create();
            }

            var upload = uploadViewController.SortedUploads[indexPath.Row];

            if (upload.Status == (int) SyncStatus.Completed)
            {
                cell.Name.TextColor = UIColor.Green;
            }
            else if (upload.Status == (int)SyncStatus.Failed)
            {
                cell.Name.TextColor = UIColor.Red;
            }
            else if (upload.Status == (int)SyncStatus.Started)
            {
                cell.Name.TextColor = UIColor.Yellow;
            }
            else if (upload.Status == (int)SyncStatus.Stopped)
            {
                cell.Name.TextColor = UIColor.Black;
            }

            cell.Name.Text = string.Format("{0}", upload.Id);
            cell.Progress.Progress += (float)upload.SyncProgress;
            cell.Percentage.Text = Math.Round(upload.SyncProgress, 2).ToString()+"%";
      
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }
    }
}

