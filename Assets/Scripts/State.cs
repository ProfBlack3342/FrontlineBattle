using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string StateName;

    public abstract void Play();
    public abstract void Loop();
    public abstract void Stop();

}

public class Pre : State
{
    public Pre()
    {
        StateName = "Pre";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OfflineMenu : State
{
    public OfflineMenu()
    {
        StateName = "OfflineMenu";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OfflineWaiting : State
{
    public OfflineWaiting()
    {
        StateName = "OfflineWaiting";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OfflineConnecting : State
{
    public OfflineConnecting()
    {
        StateName = "OfflineConnecting";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OnlinePlay : State
{
    public OnlinePlay()
    {
        StateName = "OnlinePlay";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OnlinePause : State
{
    public OnlinePause()
    {
        StateName = "OnlinePause";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}

public class OnlineEnd : State
{
    public OnlineEnd()
    {
        StateName = "OnlineEnd";
    }

    public override void Play()
    {

    }

    public override void Loop()
    {

    }

    public override void Stop()
    {

    }
}