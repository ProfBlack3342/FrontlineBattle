using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkBehaviour
{
    private NetworkRoomPlayer roomP;

    private void Update()
    {

        if(isLocalPlayer)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(!roomP.readyToBegin)
                {
                    roomP.readyToBegin = true;
                }
                else
                {
                    roomP.readyToBegin = false;
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //Sair do jogo
            }
        }
    }
}
