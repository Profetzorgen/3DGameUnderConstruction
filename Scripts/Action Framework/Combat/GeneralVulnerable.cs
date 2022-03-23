using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Denna klass hanterar med hj�lp av EventHandlern vad som h�nder med objektets HP aka livpo�ng.
public class GeneralVulnerable : Vulnerable
{
    [SerializeField] private int maxHealth;

    private int currentHealth;
    public override int MaxHealth // Maxhealth �r h�gre vid h�gre level, ej potionrelaterad
    {
        get { return maxHealth; }
        set
        {
            maxHealth = Mathf.Max(0, value);
            if (MaxHealthUpdated != null)
            {
                MaxHealthUpdated.Invoke(this, new EventArgTemplate<int>(maxHealth));
            }
        }
    }
    public override int CurrentHealth // CurrentHealth �r h�gst vid fullt liv, vid noll �r man d�d etc, denna �r potionrelaterad
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); // Clamp �r smidig f�r att hantera v�rden mellan 2 v�rden, HP i detta fall.
            if (CurrentHealthUpdated != null)
            {
                CurrentHealthUpdated.Invoke(this, new EventArgTemplate<int>(currentHealth));
            }
        }
    }

    public EventHandler<EventArgTemplate<int>> MaxHealthUpdated; // Dessa samordnar uppdaterad HP-data tillsammans med eventhandlern
    public EventHandler<EventArgTemplate<int>> CurrentHealthUpdated;
}
