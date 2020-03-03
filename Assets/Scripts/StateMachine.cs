using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentstate;
    public int stateofstate = 0; //0 = em espera, 1 = inicializando, 2 = em loop, 3 = encerrando;

    public void ChangeCurrent(State newcurrent)
    {
        currentstate = newcurrent;
    }

    public void ExecuteState()
    {
        switch (stateofstate)
        {
            case 0:
                currentstate.Play();
                break;
            case 1:
                Debug.Log("Busy starting a state");
                break;
            case 2:
                currentstate.Loop();
                break;
            case 3:
                Debug.Log("Busy ending a state");
                break;
        }
    }

    public void EndState()
    {
        if()
    }
}
