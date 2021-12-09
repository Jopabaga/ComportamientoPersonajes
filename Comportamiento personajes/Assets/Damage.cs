using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public EnemyStats stats;

    // Update is called once per frame
    
        void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals("bala") == true)
            {

               stats.hp=  stats.hp - 1;
               Debug.Log(stats.hp);
            }
        }
    
}
