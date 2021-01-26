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
    public Texture2D cursor;

    bool IsChoosingPower = false;
    float ChoosingTime;

    void Start()
    {
        ChoosingTime = PowerUpChoosingTime;
        HpBar.maxValue = Player.GetComponent<PlayerControl>().HP;
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
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
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                IsChoosingPower = false;
                return;
            }

            else
            {
                Time.timeScale = 0.2f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
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