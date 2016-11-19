using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private NotificationManager notificationManager = null;
        private SyncManager syncManager = null;

        public override UIWindow Window
        {
            get;
            set;
        }

        public NotificationManager NotificationManager
        {
            get
            {
                return notificationManager;
            }
        }

        public SyncManager SyncManager
        {
            get
            {
                return syncManager;
            }
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Create Notification Manager
            if (notificationManager == null)
            {
                notificationManager = new NotificationManager();
            }

            // Create Sync Manager
            if (syncManager == null)
            {
                syncManager = new SyncManager();
            }

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message)
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive.
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            if (application.ApplicationState == UIApplicationState.Inactive)
            {
                // GO TO SOME SPECIFIC SCREEN
            }

            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            Window.RootViewController.PresentViewController(okayAlertController, true, null);
        }

        public void SyncCompleted(NSUrlSessionTask sessionTask)
        {
            try
            {
                if (sessionTask.Response == null || string.IsNullOrEmpty(sessionTask.Response.ToString()))
                {
                    Console.WriteLine("Success, But no response found.");
                }
                else
                {
                    var resp = (NSHttpUrlResponse)sessionTask.Response;
                    var statusCode = resp.StatusCode;
                    var taskId = Convert.ToInt32(sessionTask.TaskIdentifier);

                    if (sessionTask.State == NSUrlSessionTaskState.Completed)
                    {
                        if ((int)statusCode == 200)
                        {
                            InvokeOnMainThread(delegate
                            {
                                this.NotificationManager.ShowUploadNotification(true, "Success For Task ID :" + taskId);
                            });

                            SyncManager.UpdateSyncStatus(taskId, SyncStatus.Completed);
                        }
                        else
                        {
                            InvokeOnMainThread(delegate
                            {
                                this.NotificationManager.ShowUploadNotification(false, "Failed For Task ID :" + taskId);
                            });

                            SyncManager.UpdateSyncStatus(taskId, SyncStatus.Failed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProcessCompletedTask Ex: {0}", ex.Message);
            }
        }
    }
}


