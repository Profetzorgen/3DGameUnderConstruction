using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueVulnerable : Vulnerable // en egen separat klass som �rver fr�n vulnerable, man vill v�l inte att alla fiender 
    // har samma livm�tare eller att alla fienderna d�r samtidigt bara f�r att en g�r det?
{
    [SerializeField] private Health health;
    public override int MaxHealth
    {
        get { return health.MaxHealth; }
        set { health.MaxHealth = value; }
    }
    public override int CurrentHealth
    {
        get { return health.CurrentHealth; }
        set { health.CurrentHealth = value; }
    }
}
