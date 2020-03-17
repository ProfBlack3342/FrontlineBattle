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
        Debug.Log("Changing State to " + newcurrent.StateName);
        currentstate = newcurrent;
    }

    public void ExecuteState()
    {
        if (!running)
        {
            if (!loop)
            {
                Debug.Log("Executing Start() on " + currentstate.StateName);
                currentstate.Start();
            }
            else
            {
                Debug.Log("Executing Loop() on " + currentstate.StateName);
                currentstate.Loop();
            }
        }
        else
        {
            Debug.Log(currentstate.StateName + "is still running a state");
        }
    }

    public void EndState()
    {
        if (!running)
        {
            Debug.Log("Executing Stop() on " + currentstate.StateName);
            currentstate.Stop();
        }
    }
}
