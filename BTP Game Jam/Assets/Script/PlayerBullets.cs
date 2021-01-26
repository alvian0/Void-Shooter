using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public Vector2 target;
    public float Speed = 5;
    public float Demage;
    public GameObject BulletsEffect;
    public LayerMask Ground;

    BoxCollider2D coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), coll);
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
                Destroy(collision.gameObject);
            }
            else GameObject.Find("EHurt").GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
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
                Destroy(collision.gameObject);
            }

            else GameObject.Find("EHurt").GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().HP -= Demage;
            Destroy(gameObject);
        }
    }
}