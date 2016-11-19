using System;
using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public class DownloadTableViewSource : UITableViewSource
    {
        private DownloadViewController downloadViewController;

        public DownloadTableViewSource(DownloadViewController downloadViewController)
        {
            this.downloadViewController = downloadViewController;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return downloadViewController.SortedDownloads.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (DownloadTableViewCell)tableView.DequeueReusableCell(DownloadTableViewCell.Key);

            if (cell == null)
            {
                cell = DownloadTableViewCell.Create();
            }

            var download = downloadViewController.SortedDownloads[indexPath.Row];

            if (download.Status == (int)SyncStatus.Completed)
            {
                cell.Name.TextColor = UIColor.Green;
            }
            else if (download.Status == (int)SyncStatus.Failed)
            {
                cell.Name.TextColor = UIColor.Red;
            }
            else if (download.Status == (int)SyncStatus.Started)
            {
                cell.Name.TextColor = UIColor.Yellow;
            }
            else if (download.Status == (int)SyncStatus.Stopped)
            {
                cell.Name.TextColor = UIColor.Black;
            }

            cell.Name.Text = string.Format("{0}", download.Id);
            cell.Progress.Progress += (float)download.SyncProgress;
            cell.Percentage.Text = Math.Round(download.SyncProgress, 2).ToString() + "%";

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }
    }
}

