using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float AttackSpeed;
    public float moveSpeed;
    public float cooldown = 5f;
    public GameObject bullets;
    public float demage = 5;
    public float HP = 2000;

    GameObject player;
    float cooldowns;
    float ShootAttack;
    Vector3 target;
    bool IsIdle = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cooldowns = cooldown;
        ShootAttack = AttackSpeed;
    }

    void Update()
    {
        if (cooldowns <= 0)
        {
            //attack
        }

        else
        {
            target = player.transform.position;
            cooldowns -= Time.deltaTime;
        }

        if (ShootAttack <= 0 && IsIdle)
        {
            if (player != null)
            {
                GameObject bull = Instantiate(bullets, transform.position, Quaternion.identity);
                bull.GetComponent<bullets>().Demage = demage;
                bull.GetComponent<bullets>().target = player.transform.position.normalized * 100;
                bull.GetComponent<bullets>().IsShootedByBoss = true;
                ShootAttack = AttackSpeed;
            }
        }

        else
        {
            ShootAttack -= Time.deltaTime;
        }
    }

    void attack1()
    {
        if (Vector2.Distance(transform.position, target) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        else
        {

        }
    }
}