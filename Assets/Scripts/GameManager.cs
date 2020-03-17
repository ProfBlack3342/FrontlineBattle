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
            Destroy(this);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        machine = new StateMachine();

        states[0] = new OfflineMenu();
        states[1] = new OnlineWaiting();
        states[2] = new OnlineConnecting();
        states[3] = new OnlinePlay();
        states[4] = new OnlinePause();
        states[5] = new OnlineEnd();

        endstateflag = false;

        machine.ChangeCurrent(states[0]);
        machine.ExecuteState();
    }

    private void Update()
    {
        if (!endstateflag)
        {
            Debug.Log("GameManager Update() calling for ExecuteState()");
            machine.ExecuteState();
        }
        else
        {
            Debug.Log("GameManager Update() calling for EndState()");
            machine.EndState();
        }
    }
}
