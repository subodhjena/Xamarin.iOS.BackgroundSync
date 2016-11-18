using System;

using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public class NotificationManager
    {
        private UILocalNotification notification = null;

        public NotificationManager()
        {
            if (this.notification == null)
            {
                this.notification = new UILocalNotification();
            }

            // Ask for Local Notification Permission
            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);

            // Set the sound to be the default sound
            this.notification.SoundName = UILocalNotification.DefaultSoundName;
        }

        public void ShowUploadNotification(bool syncSuccess, string fileName)
        {
            if (syncSuccess)
            {
                this.notification.AlertAction = "Sync Success";
                this.notification.AlertTitle = "Sync Success";
            }
            else
            {
                this.notification.AlertAction = "Sync Failed";
                this.notification.AlertTitle = "Sync Failed";
            }

            this.notification.AlertBody = fileName;

            this.notification.FireDate = NSDate.Now;
            UIApplication.SharedApplication.ScheduleLocalNotification(this.notification);
        }
    }
}