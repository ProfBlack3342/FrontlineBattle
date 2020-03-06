using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{
    private float HP;
    private int Ammo;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case("Bullet"):
                {

                    break;
                }
            case ("Map"):
                {

                    break;
                }
            case ("Obstacle"):
                {

                    break;
                }
            case ("Trap"):
                {

                    break;
                }
        }
    }

    public void SetHP(float HP) { this.HP = HP; }
    public float GetHP() { return HP; }

    public void SetAmmo(int Ammo) { this.Ammo = Ammo; }
    public int GetAmmo() { return Ammo; }
}
