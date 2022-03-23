using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vulnerable : MonoBehaviour // Förälderklass till unik underklass
{
    [SerializeField] protected List<string> damagingTags = new List<string>(); // Lista över taggar som kan / inte kan skada "this" gameObject

    public abstract int MaxHealth { get; set; }
    public abstract int CurrentHealth { get; set; }
    
    protected void Start()
    {
        CurrentHealth = MaxHealth;
    }
    private void OnTriggerEnter(Collider other) // Tar skada hanteras via collider samt taggar och vapnets skada subtraheras från gameObjects HP
    {
        var otherWeapon = other.gameObject.GetComponent<Weapon>();
        if (otherWeapon == null || !damagingTags.Contains(other.gameObject.tag))
        {
            return;
        }
        CurrentHealth -= otherWeapon.Damage;
        print(otherWeapon.Damage); // Print för att kunna slå på en kub och se om man lagt colliders rätt med mera.
        print(CurrentHealth);
    }
}
