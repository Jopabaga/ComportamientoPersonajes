using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        maxHealth = 10;
        hp = maxHealth;

        maxAmmo = 10;
        ammo = maxAmmo;
    }
}