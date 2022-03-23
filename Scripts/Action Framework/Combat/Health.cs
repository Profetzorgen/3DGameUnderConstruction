using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// F�r att kunna v�lja ett objekts HP-information osv i editorn:
[
    CreateAssetMenu(
    fileName = "Health.asset",
    menuName = "Action Framework/Health")
]
// All kod som beh�vs f�r att hantera objekt x's HP, andra funktioner/skript mm som finns f�r vapen eller liknande
// kan samarbeta med Health och komma �verens om hur d�d man egentligen �r!

public class Health : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int maxHealth; // S�h�r mycket HP kan objekt x ha som mest
    [NonSerialized] private int currentHealth; // S�h�r mycket HP har objekt x just nu
    [NonSerialized] private int runtimeMaxHealth; // Utr�knings-hp/buffert f�r ettor och nollor

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

    public Action<int> CurrentHealthUpdated; // eventhandlerns br�nsle
    public Action<int> MaxHealthUpdated;
    public void OnBeforeSerialize()
    {
        // denna beh�vdes f�r att till�ta IE-CB-recievern men anv�nds inte �nnu i mitt program
    }
    public void OnAfterDeserialize()
    {
        CurrentHealth = maxHealth;
        runtimeMaxHealth = maxHealth;
    }
}

