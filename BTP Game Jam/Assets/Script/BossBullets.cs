using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossBullets : MonoBehaviour
{
    public float Demage;
    public GameObject BulletsEffect;
    public LayerMask Ground;

    GameObject Player;
    PlayerControl playercontrol;
    Collider2D coll;
    GameManager manager;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playercontrol = Player.GetComponent<PlayerControl>();
        Physics2D.IgnoreLayerCollision(12, 11);
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        Physics2D.IgnoreLayerCollision(12, 11);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().IsChoosingPower)
            {
                return;
            }

            collision.gameObject.GetComponent<PlayerControl>().HP -= Demage;
            Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
            GameObject.Find("PHurt").GetComponent<AudioSource>().Play();

            if (HPCheck())
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().PowerUpsPick();
            }
        }

        Destroy(gameObject);
    }

    bool HPCheck()
    {
        float[] i = { 90, 80, 70, 60, 50, 40, 30, 20, 10 };
        bool j;
        PlayerControl pcontrol = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        if (i.Contains(pcontrol.HP))
        {
            j = true;
        }

        else
        {
            j = false;
        }

        return j;
    }
}