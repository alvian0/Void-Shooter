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
    public float PowerUpChoosingTime = 3f;
    public Texture2D cursor;
    public float CountDown = 120;
    public TextMesh textmesh;
    public Text Ammo;
    public GameObject ammo;
    public Image CountDownPickPowerUp;
    public GameObject pauseScreen;
    public Text PoinText;
    public int PointEarn = 100;
    public int EnemyKill;
    public GameObject GameOverScreen;
    public Text Score;
    public GameObject Bossssss;
    public AudioSource BGM;
    public float AmmoSpawnTime;
    public bool IsThereIsABoos = false;

    int Point;
    public bool IsChoosingPower = false;
    float ChoosingTime;
    bool IsPaused;
    public float timer;

    void Start()
    {
        PlayerPrefs.SetInt("Play", PlayerPrefs.GetInt("Play") + 1);
        Time.timeScale = 1;
        ChoosingTime = PowerUpChoosingTime;
        HpBar.maxValue = Player.GetComponent<PlayerControl>().HP;
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
        InvokeRepeating("RandomAmmo", 1, AmmoSpawnTime);
        timer = CountDown;
    }

    void Update()
    {
        PoinText.text = Point.ToString();
        Score.text = "Score\n" + Point + "\nEnemy Kill\n" + EnemyKill;

        timer -= Time.deltaTime;

        if (timer >= 0)
        {
            string minute = ((int)timer / 60).ToString();
            string seconds = (timer % 60).ToString("f2");

            textmesh.text = minute + ":" + seconds;
        }

        else
        {
            if (!IsThereIsABoos)
            {
                Instantiate(Bossssss, new Vector2(0, 1.4f), Quaternion.identity);
                IsThereIsABoos = true;
                return;
            }

            textmesh.text = "Hello World";
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !IsChoosingPower)
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
                GameOverScreen.SetActive(true);

                if (Point >= PlayerPrefs.GetInt("HS"))
                {
                    PlayerPrefs.SetInt("HS", Point);
                }

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
        float YGetPos = SpawnPos.localScale.y / 2 + .6f;
        Instantiate(ammo, new Vector2(SpawnPos.position.x + randomx, SpawnPos.position.y + YGetPos), Quaternion.identity);
    }

    public void pause()
    {
        pauseScreen.SetActive(true);
        IsPaused = true;
        BGM.Pause();
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        IsPaused = false;
        BGM.Play();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetPoint(bool ExtraBoss = false)
    {
        if (ExtraBoss)
        {
            Point += PointEarn * 20;
        }

        else
        {
            Point += PointEarn;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}