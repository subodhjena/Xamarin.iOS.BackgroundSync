// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Xamarin.iOS.BackgroundSync
{
	[Register ("DownloadViewController")]
	partial class DownloadViewController
	{
		[Outlet]
		UIKit.UITableView tableView { get; set; }

		[Action ("AddDownload:")]
		partial void AddDownload (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}
