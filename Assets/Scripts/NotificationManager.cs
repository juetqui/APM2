using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    private bool isGameRunning = true;

    private void Start()
    {
        // Crear y registrar el canal de notificaciones
        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "remind_notif_ch",
            Name = "Generic Channel",
            Description = "Canal de notificaciones para el juego.",
            Importance = Importance.Default // Nivel de importancia: visible y con sonido
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        // Detecta si el juego entra en segundo plano
        if (pauseStatus)
        {
            isGameRunning = false;
            ScheduleNotifications();
        }
        else
        {
            // Si el juego vuelve al primer plano, cancela las notificaciones
            isGameRunning = true;
            AndroidNotificationCenter.CancelAllScheduledNotifications();
        }
    }

    private void OnApplicationQuit()
    {
        // Cuando se cierra la aplicación, programa las notificaciones
        isGameRunning = false;
        ScheduleNotifications();
    }

    private void ScheduleNotifications()
    {
        if (isGameRunning)
            return;

        // Programar las notificaciones solo si el juego no está funcionando
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        ScheduleNotification("Volvé a jugar", "Pasó mucho tiempo desde que jugaste la última vez, ¡volvé al juego!", 30);
        ScheduleNotification("Stamina cargada", "¡Ya podés volver a jugar!", 180);
        ScheduleNotification("Recupera stamina!", "Recuerda que puedes cargar stamina más rápido viendo anuncios!", 60);
    }

    private void ScheduleNotification(string title, string text, int delaySeconds)
    {
        var notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            LargeIcon = "main_large_icon",
            SmallIcon = "main_small_icon",
            FireTime = DateTime.Now.AddSeconds(delaySeconds)
        };

        AndroidNotificationCenter.SendNotification(notification, "remind_notif_ch");
    }
}
