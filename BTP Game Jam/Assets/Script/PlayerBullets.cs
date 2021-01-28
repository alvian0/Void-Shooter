using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public float Speed = 5;
    public float Demage;
    public GameObject BulletsEffect;
    public LayerMask Ground;

    PlayerControl player;
    BoxCollider2D coll;
    GameManager manager;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), coll);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), coll);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(BulletsEffect, transform.position, Quaternion.identity);

        if (collision.gameObject.tag == "Enemy2")
        {
            collision.gameObject.transform.GetChild(0).GetComponent<EnemyHPBar>().HpBar.transform.gameObject.SetActive(true);
            collision.gameObject.GetComponent<Enemy2>().HP -= Demage;

            if (collision.gameObject.GetComponent<Enemy2>().HP <= 0)
            {
                Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
                GameObject.Find("EDie").GetComponent<AudioSource>().Play();
                Instantiate(collision.gameObject.GetComponent<Enemy2>().DeadEffect, collision.transform.position, Quaternion.identity);
                manager.GetPoint();
                manager.EnemyKill += 1;
                player.TotalAmmo += 10;
                Destroy(collision.gameObject);
            }

            else GameObject.Find("EHurt").GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(BulletsEffect, transform.position, Quaternion.identity);

        if (collision.tag == "Enemy")
        {
            collision.gameObject.transform.GetChild(0).GetComponent<EnemyHPBar>().HpBar.transform.gameObject.SetActive(true);
            collision.gameObject.GetComponent<Enemy>().HP -= Demage;

            if (collision.gameObject.GetComponent<Enemy>().HP <= 0)
            {
                Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
                GameObject.Find("EDie").GetComponent<AudioSource>().Play();
                Instantiate(collision.gameObject.GetComponent<Enemy>().DeadEffect, collision.transform.position, Quaternion.identity);
                manager.GetPoint();
                manager.EnemyKill += 1;
                player.TotalAmmo += 10;
                Destroy(collision.gameObject);
            }

            else GameObject.Find("EHurt").GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        if (collision.tag == "Enemy3")
        {
            collision.gameObject.transform.parent.GetChild(1).GetComponent<EnemyHPBar>().HpBar.transform.gameObject.SetActive(true);
            collision.gameObject.GetComponent<Enemy3>().HP -= Demage;

            if (collision.gameObject.GetComponent<Enemy3>().HP <= 0)
            {
                Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
                GameObject.Find("EDie").GetComponent<AudioSource>().Play();
                //Instantiate(collision.gameObject.GetComponent<Enemy2>().DeadEffect, collision.transform.position, Quaternion.identity);
                manager.GetPoint();
                manager.EnemyKill += 1;
                player.TotalAmmo += 10;
                GameObject parents = collision.gameObject.transform.parent.gameObject;
                Destroy(parents);
            }

            else GameObject.Find("EHurt").GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            try
            {
                if (!collision.gameObject.GetComponent<Boss>().Imune)
                {
                    collision.gameObject.GetComponent<Boss>().HP -= Demage;

                    if(collision.gameObject.GetComponent<Boss>().HP <= 0)
                    {
                        collision.gameObject.GetComponent<Boss>().Dead();
                    }
                }
            }

            catch
            {
                if (!collision.transform.parent.parent.GetComponent<Boss>().Imune)
                {
                    collision.transform.parent.parent.GetComponent<Boss>().HP -= Demage;

                    if(collision.transform.parent.parent.GetComponent<Boss>().HP <= 0)
                    {
                        collision.transform.parent.parent.GetComponent<Boss>().Dead();
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}