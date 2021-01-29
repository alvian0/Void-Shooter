using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class MenuManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Material mat;
    public Color[] coll;
    [SerializeField]
    int index = 0;

    [Header("Menu")]
    public Text HighScore;
    public Text TimesPlayed;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        TimesPlayed.text = "Time Played\n" + PlayerPrefs.GetInt("Play").ToString();
        HighScore.text = "High Score\n" + PlayerPrefs.GetInt("HS").ToString();
    }

    public void ChangeColor(bool next)
    {
        if (next)
        {
            index++;
            if (index >= coll.Length)
            {
                index = 0;
                mat.color = coll[index];
            }
            else
            {
                mat.color = coll[index];
            }
        }

        else
        {
            index--;
            if (index < 0)
            {
                index = coll.Length - 1;
                mat.color = coll[index];
            }
            else
            {
                mat.color = coll[index];
            }
        }
    }

    public void Play(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenUrl(string link)
    {
        Application.OpenURL(link);
    }
}