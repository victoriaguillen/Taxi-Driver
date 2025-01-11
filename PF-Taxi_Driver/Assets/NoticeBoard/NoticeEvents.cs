using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class NoticeEvents
{
    // Delegado para manejar los mensajes
    public delegate void NoticeMessage(string message);

    // Evento estático al que otros pueden suscribirse
    public static event NoticeMessage OnNotice;

    // Método para invocar el evento
    public static void RaiseNotice(string message)
    {
        OnNotice?.Invoke(message); // Verifica si hay suscriptores y lanza el evento
    }
}
