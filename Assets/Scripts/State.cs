using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class State
{
    public string StateName;

    public abstract void Start();
    public abstract void Loop();
    public abstract void Stop();

}

public class OfflineMenu : State
{
    private bool host;

    public OfflineMenu()
    {
        StateName = "OfflineMenu";
        host = true;
    }

    public override void Start()
    {
        Debug.Log("OfflineMenu Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OfflineMenu Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OfflineMenu Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        /*Código
        if (host)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[1]);
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[2]);
        }
        */

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlineWaiting : State
{
    bool foundplayers;

    public OnlineWaiting()
    {
        StateName = "OnlineWaiting";
        foundplayers = false;
    }

    public override void Start()
    {
        Debug.Log("OfflineWaiting Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OfflineWaiting Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OfflineWaiting Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        if (foundplayers)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[3]);
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[0]);
        }

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlineConnecting : State
{
    bool foundhost;

    public OnlineConnecting()
    {
        StateName = "OnlineConnecting";
        foundhost = false;
    }

    public override void Start()
    {
        Debug.Log("OfflineConnecting Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OfflineConnecting Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OfflineConnecting Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        if (foundhost)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[3]);
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[0]);
        }

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlinePlay : State
{
    bool pause;

    public OnlinePlay()
    {
        StateName = "OnlinePlay";
        pause = false;
    }

    public override void Start()
    {
        Debug.Log("OnlinePlay Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OnlinePlay Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OnlinePlay Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        if (pause)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[4]);
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[5]);
        }

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlinePause : State
{
    bool resume;

    public OnlinePause()
    {
        StateName = "OnlinePause";
        resume = false;
    }

    public override void Start()
    {
        Debug.Log("OnlinePause Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OnlinePause Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OnlinePause Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        if (resume)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[3]);
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[5]);
        }

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlineEnd : State
{
    public OnlineEnd()
    {
        StateName = "OnlineEnd";
    }

    public override void Start()
    {
        Debug.Log("OnlineEnd Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OnlineEnd Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OnlineEnd Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[0]);

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}