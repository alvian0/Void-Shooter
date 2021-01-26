using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Slider HpBar;
    public GameObject Player;
    public GameObject Spawner, PowerUpChoice;
    public PowerUp[] powerups;
    public float PowerUpChoosingTime = 3f;
    public Texture2D cursor;
    public float CountDown = 120;
    public TextMesh textmesh;
    public Text Ammo;
    public GameObject ammo;
    public Image CountDownPickPowerUp;
    public GameObject pauseScreen;

    bool IsChoosingPower = false;
    float ChoosingTime;
    bool IsPaused;
    float timess;

    void Start()
    {
        Time.timeScale = 1;
        timess = CountDown;
        ChoosingTime = PowerUpChoosingTime;
        HpBar.maxValue = Player.GetComponent<PlayerControl>().HP;
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
        InvokeRepeating("RandomAmmo", 1, 5);
    }

    void Update()
    {
        float timer = Time.time - CountDown;

        if (timer <= 0)
        {
            string minute = ((int)-timer / 60).ToString();
            string seconds = (-timer % 60).ToString("f2");

            textmesh.text = minute + ":" + seconds;
        }

        else
        {
            textmesh.text = "Hello World";
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                pause();
            }

            else
            {
                UnPause();
            }
        }

        if (Player != null)
        {
            Ammo.text = Player.GetComponent<PlayerControl>().CurrentAmmo.ToString() + "/" + Player.GetComponent<PlayerControl>().TotalAmmo.ToString();

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
                CountDownPickPowerUp.fillAmount = ChoosingTime;
            }
        }
    }

    public void PowerUpsPick()
    {
        PowerUpChoice.SetActive(true);
        ChoosingTime = PowerUpChoosingTime;
        CountDownPickPowerUp.fillAmount = PowerUpChoosingTime;
        IsChoosingPower = true;
    }

    public void PowerUpPicked()
    {
        PowerUpChoice.SetActive(false);
        ChoosingTime = PowerUpChoosingTime;
        IsChoosingPower = false;
        Time.timeScale = 1f;
    }

    void RandomAmmo()
    {
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
        Transform SpawnPos = grounds[Random.Range(0, grounds.Length)].transform;
        float randomx = Random.Range(-SpawnPos.localScale.x / 2.5f - 1, SpawnPos.localScale.x / 2.5f - 1);
        Instantiate(ammo, new Vector2(SpawnPos.position.x + randomx, SpawnPos.position.y + 1), Quaternion.identity);
    }

    public void pause()
    {
        pauseScreen.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}