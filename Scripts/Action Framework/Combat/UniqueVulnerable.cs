using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueVulnerable : Vulnerable // en egen separat klass som ärver från vulnerable, man vill väl inte att alla fiender 
    // har samma livmätare eller att alla fienderna dör samtidigt bara för att en gör det?
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
