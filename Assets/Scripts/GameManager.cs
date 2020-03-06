using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;

    public StateMachine machine;
    public State[] states = new State[6];

    public bool endstateflag;

    private void Awake()
    {
        if (singleton != this && singleton != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    private void Start()
    {
        machine = new StateMachine();

        states[0] = new Pre();
        states[1] = new OfflineMenu();
        states[2] = new OfflineWaiting();
        states[3] = new OfflineConnecting();
        states[4] = new OnlinePlay();
        states[5] = new OnlinePause();
        states[6] = new OnlineEnd();

        endstateflag = false;

        machine.ChangeCurrent(states[0]);
        machine.ExecuteState();
    }

    private void Update()
    {
        if (!endstateflag)
        {
            machine.ExecuteState();
        }
        else
        {
            machine.EndState();
        }
    }
}
