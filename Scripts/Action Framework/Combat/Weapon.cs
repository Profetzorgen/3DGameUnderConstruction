using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfiguration configuration;
    private float damageMultiplier = 1.0f;

    public int Damage
    {
        get
        {
            var damageFloor = Mathf.FloorToInt(configuration.Damage *
                damageMultiplier);
            return Mathf.Max(damageFloor, 0);
        }
    }
    public float DamageMultiplier
    {
        get { return damageMultiplier; }
        set { damageMultiplier = Mathf.Max(0.0f, value); }
    }

}




