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

public class Pre : State
{
    public Pre()
    {
        StateName = "Pre";
    }

    public override void Start()
    {
        Debug.Log("Pre Start");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("Pre Loop");

        GameManager.singleton.machine.running = true;

        //Código

        GameManager.singleton.endstateflag = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("Pre Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        SceneManager.LoadScene(1);

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OfflineMenu : State
{
    public OfflineMenu()
    {
        StateName = "OfflineMenu";
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

        //Código

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OfflineWaiting : State
{
    public OfflineWaiting()
    {
        StateName = "OfflineWaiting";
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

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OfflineConnecting : State
{
    public OfflineConnecting()
    {
        StateName = "OfflineConnecting";
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

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlinePlay : State
{
    public OnlinePlay()
    {
        StateName = "OnlinePlay";
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

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlinePause : State
{
    public OnlinePause()
    {
        StateName = "OnlinePause";
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

        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}