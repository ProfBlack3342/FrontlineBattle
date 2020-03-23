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
            HUD = GameObject.Find("HUD Listener").GetComponent<HUDRef>();
        }

        HUD.HostButton.gameObject.SetActive(true);
        HUD.JoinButton.gameObject.SetActive(true);
        HUD.BackButton.gameObject.SetActive(false);
        HUD.ConnectButton.gameObject.SetActive(false);
        HUD.input.gameObject.SetActive(false);
        HUD.ReadyButton.gameObject.SetActive(false);
        HUD.infobox.gameObject.SetActive(false);
        HUD.Host = HUD.Join = HUD.Back = HUD.Connect = HUD.Ready = false;

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
    public HUDRef HUD;
    public GameObject[] PlayerList;
    public PlayerMovement Player;

    public OnlineWaiting(HUDRef HUD)
    {
        StateName = "OnlineWaiting";
        foundplayer = false;
        stop = false;
        this.HUD = HUD;
    }

    public override void Start()
    {
        Debug.Log("OnlineWaiting Start");

        GameManager.singleton.machine.running = true;


        HUD.HostButton.gameObject.SetActive(false);
        HUD.JoinButton.gameObject.SetActive(false);
        HUD.BackButton.gameObject.SetActive(false);
        HUD.ConnectButton.gameObject.SetActive(false);
        HUD.input.gameObject.SetActive(false);


        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OnlineWaiting Loop");

        GameManager.singleton.machine.running = true;

        if (NetworkClient.isConnected && !ClientScene.ready)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if(Player != null)
                Player.enabled = false;
        }

        PlayerList = GameObject.FindGameObjectsWithTag("Player");

        if (PlayerList.Length == 2 && PlayerList != null)
        {
            stop = true;
            foundplayer = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
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
        Debug.Log("OnlineWaiting Stop");

        GameManager.singleton.machine.loop = false;
        GameManager.singleton.machine.running = true;


        if (foundplayer)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[3]);
            Player.enabled = true;

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
    public HUDRef HUD;

    public OnlineConnecting(HUDRef HUD)
    {
        StateName = "OnlineConnecting";
        foundhost = false;
        stop = false;
        this.HUD = HUD;
    }

    public override void Start()
    {
        Debug.Log("OfflineConnecting Start");

        GameManager.singleton.machine.running = true;


        HUD.HostButton.gameObject.SetActive(false);
        HUD.JoinButton.gameObject.SetActive(false);
        HUD.BackButton.gameObject.SetActive(false);
        HUD.ConnectButton.gameObject.SetActive(false);
        HUD.input.gameObject.SetActive(false);
        HUD.ReadyButton.gameObject.SetActive(true);
        HUD.infobox.gameObject.SetActive(true);


        GameManager.singleton.machine.loop = true;
        GameManager.singleton.machine.running = false;
    }

    public override void Loop()
    {
        Debug.Log("OfflineConnecting Loop");

        GameManager.singleton.machine.running = true;

        if (NetworkClient.isConnected && !ClientScene.ready)
        {
            if (HUD.Ready)
            {
                HUD.Ready = false;
                ClientScene.Ready(NetworkClient.connection);

                if (ClientScene.localPlayer == null)
                {
                    ClientScene.AddPlayer();
                    stop = true;
                }
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


        if (resumetogame)
        {
            GameManager.singleton.machine.ChangeCurrent(GameManager.singleton.states[3]);
        }
        else
        {
            NetworkManager.singleton.StopHost();
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
