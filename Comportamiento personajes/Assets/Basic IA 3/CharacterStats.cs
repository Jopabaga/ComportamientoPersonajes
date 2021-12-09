using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int hp;
    public int maxHealth;

    public int ammo;
    public int maxAmmo;

    public bool isDead;

    public void CheckHealth()
    {
        if (hp >= maxHealth)
        {
            hp = maxHealth;
        }

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
        }
    }
}