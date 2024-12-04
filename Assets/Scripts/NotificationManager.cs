using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();


        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "remind_notif_ch",
            Name = "Generic Channel",
            Description = "Una descripci�n del channel",
            Importance = Importance.Low
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        var notif = new AndroidNotification()
        {
            Title = "Volv� a jugar",
            Text = "Pas� mucho tiempo desde que jugaste la �ltima vez, volv� al juego.",
            LargeIcon = "main_large_icon",
            SmallIcon = "main_small_icon",
            FireTime = DateTime.Now.AddSeconds(100)
        };

        AndroidNotificationCenter.SendNotification(notif, notifChannel.Id);
    }
}
