using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        maxHealth=1;
        hp = maxHealth;

        maxAmmo = 10;
        ammo = maxAmmo;
    }
    void Update()
    {
        CheckHealth();
    }
}