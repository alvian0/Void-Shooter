using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPowerUp : MonoBehaviour
{
    public Text text;

    GameManager manager;
    PowerUp powerups;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        powerups = manager.powerups[Random.Range(0, manager.powerups.Length)];
    }

    private void Update()
    {
        if (powerups.HealthPoint <= 0)
        {
            text.text = 
                "HP " + powerups.HealthPoint.ToString().Replace("-", ("- ")) +
                "\nDemage + " + powerups.Demage.ToString() + 
                "\nSpeed + " + powerups.Speed.ToString() +
                "\nFire rate + " + powerups.FireRate.ToString();
        }
        else
        {
            text.text = 
                "HP + " + powerups.HealthPoint.ToString() +
                "\nDemage + " + powerups.Demage.ToString() + 
                "\nSpeed + " + powerups.Speed.ToString() +
                "\nFire rate + " + powerups.FireRate.ToString();
        }
    }

    public void AplyPowerUp()
    {
        PlayerControl player = manager.Player.GetComponent<PlayerControl>();

        player.HP += powerups.HealthPoint;
        player.Demage += powerups.Demage;
        player.Speed += powerups.Speed;
        player.FireRate += powerups.FireRate;

        manager.PowerUpPicked();

        for(int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<GetPowerUp>().Start();
        }

        Time.timeScale = 1f;
    }
}