using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public AlertState alert;
    public RestState rest;
    public AtackState atack;
    public DeadState dead;

    public bool detectaMovimiento;
    public bool encuentraCadaver;
    public bool escuchaAlarma;

    public bool completaRuta;

    public bool esAtacado;


    public override State RunCurrentState()
    {
        if (detectaMovimiento || encuentraCadaver || escuchaAlarma) return alert;
        else if (completaRuta) return rest;
        else if (esAtacado) return atack;
        else return this;
    }

    private void detectarMov() 
    {
        //Detectar movimiento
        detectaMovimiento = true;
    }
}
