using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentstate;
    public bool running = false;    //Rodando estado
    public bool loop = false;       //Já rodou Start() uma vez;

    public void ChangeCurrent(State newcurrent) //Chamar somente dentro de um estado
    {
        currentstate = newcurrent;
    }

    public void ExecuteState()
    {
        if(!running)
        {
            if (!loop)
            {
                currentstate.Start();
            }
            else
            {
                currentstate.Loop();
            }
        }
    }

    public void EndState()
    {
        if (!running)
        {
            currentstate.Stop();
        }
    }
}
