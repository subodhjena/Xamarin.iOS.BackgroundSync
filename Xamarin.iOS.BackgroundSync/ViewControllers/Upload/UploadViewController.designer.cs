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
	[Register ("UploadViewController")]
	partial class UploadViewController
	{
		[Outlet]
		UIKit.UITableView tableView { get; set; }

		[Action ("AddUpload:")]
		partial void AddUpload (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}
