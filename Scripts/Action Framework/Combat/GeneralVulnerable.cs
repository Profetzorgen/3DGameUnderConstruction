using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Denna klass hanterar med hjälp av EventHandlern vad som händer med objektets HP aka livpoäng.
public class GeneralVulnerable : Vulnerable
{
    [SerializeField] private int maxHealth;

    private int currentHealth;
    public override int MaxHealth // Maxhealth är högre vid högre level, ej potionrelaterad
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
    public override int CurrentHealth // CurrentHealth är högst vid fullt liv, vid noll är man död etc, denna är potionrelaterad
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); // Clamp är smidig för att hantera värden mellan 2 värden, HP i detta fall.
            if (CurrentHealthUpdated != null)
            {
                CurrentHealthUpdated.Invoke(this, new EventArgTemplate<int>(currentHealth));
            }
        }
    }

    public EventHandler<EventArgTemplate<int>> MaxHealthUpdated; // Dessa samordnar uppdaterad HP-data tillsammans med eventhandlern
    public EventHandler<EventArgTemplate<int>> CurrentHealthUpdated;
}
