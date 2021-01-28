using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class bullets : MonoBehaviour
{
    public Vector2 target;
    public float Speed = 5;
    public float Demage;
    public bool IsShootedByBoss = false;
    CircleCollider2D coll;

    float DestroyByTime = 3f;
    float DestroyTime;

    private void Start()
    {
        DestroyTime = DestroyByTime;
        coll = GetComponent<CircleCollider2D>();
    }
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);

        if (DestroyTime <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            DestroyTime -= Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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