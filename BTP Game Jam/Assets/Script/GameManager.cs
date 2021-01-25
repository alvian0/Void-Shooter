using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Slider HpBar;
    public GameObject Player;
    public GameObject Spawner, PowerUpChoice;
    public PowerUp[] powerups;
    public float PowerUpChoosingTime = 3f;

    bool IsChoosingPower = false;
    float ChoosingTime;

    void Start()
    {
        ChoosingTime = PowerUpChoosingTime;
        HpBar.maxValue = Player.GetComponent<PlayerControl>().HP;
    }

    void Update()
    {
        if (Player != null)
        {
            HpBar.value = Player.GetComponent<PlayerControl>().HP;

            if (Player.GetComponent<PlayerControl>().HP <= 0)
            {
                Spawner.SetActive(false);
                Destroy(Player);
            }
        }

        if (IsChoosingPower)
        {
            if (ChoosingTime <= 0)
            {
                PowerUpChoice.SetActive(false);
                IsChoosingPower = false;
                return;
            }

            else
            {
                ChoosingTime -= Time.deltaTime;
            }
        }
    }

    public void PowerUpsPick()
    {
        PowerUpChoice.SetActive(true);
        ChoosingTime = PowerUpChoosingTime;
        IsChoosingPower = true;
    }

    public void PowerUpPicked()
    {
        PowerUpChoice.SetActive(false);
        ChoosingTime = PowerUpChoosingTime;
        IsChoosingPower = false;
    }
}