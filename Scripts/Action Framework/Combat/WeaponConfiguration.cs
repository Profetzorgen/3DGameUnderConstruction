using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[
CreateAssetMenu(
fileName = "WeaponConfiguration.asset",
menuName = "Action Framework/Weapon Configuration")
]
public class WeaponConfiguration : ScriptableObject
{
    [SerializeField] private int damage;

    public int Damage
    {
        get { return damage; }
    }
}


