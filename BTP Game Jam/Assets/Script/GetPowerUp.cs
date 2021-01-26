using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPowerUp : MonoBehaviour
{
    public float Demage;
    public float Hp;
    public float FireRate;
    public float Speed;
    public int Jumps;

    PlayerControl Player;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        Player = manager.Player.GetComponent<PlayerControl>();
    }

    public void DemagePlus(bool Double)
    {
        if (Double)
        {
            Player.Demage += Demage * 2;
            Player.HP -= Hp;
        }

        else
        {
            Player.Demage += Demage;
        }

        manager.PowerUpPicked();
    }

    public void SpeedPlus(bool Double)
    {
        if (Double)
        {
            Player.Speed += Speed * 2;
            Player.HP -= Hp;
        }
        
        else
        {
            Player.Speed += Speed;
        }

        manager.PowerUpPicked();
    }

    public void FireRatePlus(bool Double)
    {
        if (Double)
        {
            Player.FireRate += FireRate * 2;
            Player.HP -= Hp;
        }

        else
        {
            Player.FireRate += FireRate;
        }

        manager.PowerUpPicked();
    }

    public void JumpPlus(bool Double)
    {
        if (Double)
        {
            Player.JumpCount += Jumps * 2;
            Player.HP -= Hp;
        }

        else
        {
            Player.JumpCount += Jumps;
        }

        manager.PowerUpPicked();
    }
}