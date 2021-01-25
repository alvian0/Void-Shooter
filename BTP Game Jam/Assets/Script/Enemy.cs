using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;
    public float DemageDealt = 10f;
    public float HP = 10;

    Transform playerpos;
    float CurrentSpeed;

    private void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CurrentSpeed = Speed;
    }

    private void Update()
    {
        if (HP <= 5)
        {
            CurrentSpeed = Speed * 2;
        }

        if (playerpos != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerpos.transform.position, CurrentSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().HP -= DemageDealt;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().HP -= DemageDealt;
            if (HPCheck())
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().PowerUpsPick();

            Destroy(gameObject);
        }
    }

    bool HPCheck()
    {
        float[] i = { 90, 80, 70, 60, 50, 40, 30, 20, 10 };
        bool j;
        PlayerControl pcontrol = playerpos.transform.gameObject.GetComponent<PlayerControl>();

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