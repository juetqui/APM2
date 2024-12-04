using System.Collections;
using System.Collections.Generic;
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
            Description = "Una descripción del channel",
            Importance = Importance.Low
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        var notif = new AndroidNotification()
        {
            Title = "Volvé a jugar",
            Text = "Pasó mucho tiempo desde que jugaste la última vez, volvé al juego.",
            LargeIcon = "Main_Large_Icon",
            SmallIcon = "Main_Small_Icon",
            FireTime = DateTime.Now.AddSeconds(100)
        };

        AndroidNotificationCenter.SendNotification(notif, notifChannel.Id);
    }
}
