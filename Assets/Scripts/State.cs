using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

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
    public bool stop;
    public HUDRef HUD;

    public OfflineMenu(HUDRef HUD)
    {
        StateName = "OfflineMenu";
        host = true;
        stop = false;
        this.HUD = HUD;
    }

    public override void Start()
    {
        Debug.Log("OfflineMenu Start");

        GameManager.singleton.machine.running = true;

        if(HUD ==  null)
        {
            HUD = GameObject.Find("HUD Controller").GetComponent<HUDRef>();
        }

        HUD.HostButton.gameObject.SetActive(true);
        HUD.JoinButton.gameObject.SetActive(true);
        HUD.BackButton.gameObject.SetActive(false);
        HUD.ConnectButton.gameObject.SetActive(false);
        HUD.input.gameObject.SetActive(false);
        HUD.Host = HUD.Join = HUD.Back = HUD.Connect = false;

        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OfflineMenu Loop");

        GameManager.singleton.machine.running = true;


        if (HUD.Host)
        {
            host = true;
            stop = true;
        }
        if(HUD.Join)
        {
            HUD.HostButton.gameObject.SetActive(false);
            HUD.JoinButton.gameObject.SetActive(false);
            HUD.BackButton.gameObject.SetActive(true);
            HUD.ConnectButton.gameObject.SetActive(true);
            HUD.input.gameObject.SetActive(true);
            HUD.Join = false;
        }
        if(HUD.Back)
        {
            HUD.HostButton.gameObject.SetActive(true);
            HUD.JoinButton.gameObject.SetActive(true);
            HUD.BackButton.gameObject.SetActive(false);
            HUD.ConnectButton.gameObject.SetActive(false);
            HUD.input.gameObject.SetActive(false);
            HUD.Back = false;
        }
        if(HUD.Connect)
        {
            host = false;
            stop = true;
        }

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }


        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OfflineMenu Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        if (host)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[1]);
            NetworkManager.singleton.StartHost();
        }
        else
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[2]);
            NetworkManager.singleton.StartClient();
            NetworkManager.singleton.networkAddress = HUD.IP;
        }
        GameManager.singleton.machine.running = false;
        GameManager.singleton.endstateflag = false;
    }
}

public class OnlineWaiting : State
{
    public bool foundplayer;
    public bool stop;

    public OnlineWaiting()
    {
        StateName = "OnlineWaiting";
        foundplayer = false;
        stop = false;
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

        //Code

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OfflineWaiting Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código
        if (foundplayer)
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
    public bool foundhost;
    public bool stop;

    public OnlineConnecting()
    {
        StateName = "OnlineConnecting";
        foundhost = false;
        stop = false;
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

        if (NetworkClient.isConnected && !ClientScene.ready)
        {
            ClientScene.Ready(NetworkClient.connection);

            if (ClientScene.localPlayer == null)
            {
                ClientScene.AddPlayer();
                stop = true;
            }
        }

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }

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
    public bool pausetogame;
    public bool stop;

    public OnlinePlay()
    {
        StateName = "OnlinePlay";
        pausetogame = true;
        stop = false;
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

        //Code

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OnlinePlay Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código

        if (pausetogame)
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
    private bool resumetogame;
    private bool stop;

    public OnlinePause()
    {
        StateName = "OnlinePause";
        resumetogame = false;
        stop = false;
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

        //Code

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }

        GameManager.singleton.machine.running = false;
    }

    public override void Stop()
    {
        Debug.Log("OnlinePause Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;

        //Código

        if (resumetogame)
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
    private bool stop;

    public OnlineEnd()
    {
        StateName = "OnlineEnd";
        stop = false;
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

        //Code

        if (stop)
        {
            GameManager.singleton.endstateflag = true;
        }

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
