using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// För att kunna välja ett objekts HP-information osv i editorn:
[
    CreateAssetMenu(
    fileName = "Health.asset",
    menuName = "Action Framework/Health")
]
// All kod som behövs för att hantera objekt x's HP, andra funktioner/skript mm som finns för vapen eller liknande
// kan samarbeta med Health och komma överens om hur död man egentligen är!

public class Health : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int maxHealth; // Såhär mycket HP kan objekt x ha som mest
    [NonSerialized] private int currentHealth; // Såhär mycket HP har objekt x just nu
    [NonSerialized] private int runtimeMaxHealth; // Uträknings-hp/buffert för ettor och nollor

    public int MaxHealth // Hantera max-HP
    {
        get { return runtimeMaxHealth; }
        set { 
            runtimeMaxHealth = Mathf.Max(value, 0);
            if (MaxHealthUpdated != null)
            {
                MaxHealthUpdated.Invoke(runtimeMaxHealth);
            }
        }
    }

    public int CurrentHealth // Hantera nuvarande HP
    {
        get { return currentHealth; }
        set { 
            currentHealth = Mathf.Max(value, 0); 
            if(CurrentHealthUpdated != null)
            {
                CurrentHealthUpdated.Invoke(currentHealth);
            }
        }
    }

    public Action<int> CurrentHealthUpdated; // eventhandlerns bränsle
    public Action<int> MaxHealthUpdated;
    public void OnBeforeSerialize()
    {
        // denna behövdes för att tillåta IE-CB-recievern men används inte ännu i mitt program
    }
    public void OnAfterDeserialize()
    {
        CurrentHealth = maxHealth;
        runtimeMaxHealth = maxHealth;
    }
}

