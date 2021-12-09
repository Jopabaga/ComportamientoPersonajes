using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoldier : SoldierStats
{
    public PatrolState patrol;
    public RestState rest;
    public AlertState alert;
    public AtackState atack;
    public DeadState dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
