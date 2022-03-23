using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 // Denna kod finns som grund f�r att hantera events, s� som ta skada eller liknande
public class EventArgTemplate<T> : EventArgs
{
    public T Attachment { get; private set; }
    public EventArgTemplate(T attachment) { Attachment = attachment; }
}
