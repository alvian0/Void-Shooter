using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy3 : MonoBehaviour
{
    public float HP = 20;
    public float Speed = 10;
    public float Demage = 5;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 8);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) >= 1.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().IsChoosingPower)
            {
                return;
            }

            Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
            GameObject.Find("PHurt").GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<PlayerControl>().HP -= Demage;
            if (HPCheck())
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().PowerUpsPick();
        }
    }

    bool HPCheck()
    {
        float[] i = { 90, 80, 70, 60, 50, 40, 30, 20, 10 };
        bool j;
        PlayerControl pcontrol = player.transform.gameObject.GetComponent<PlayerControl>();

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
