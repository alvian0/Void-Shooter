using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float Speed;
    public float KeepDistance = 5f;
    public Vector3 offset;
    Transform player;
    public float timeToShoot = 3f;
    public float Demage = 5;
    public GameObject bullets, weapon;
    public Transform muzzle;
    public float HP = 15;

    float NextTimeToShoot;
    Vector3 offsets;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offsets = offset;
        NextTimeToShoot = timeToShoot;
        transform.position = new Vector2(transform.position.x, 10);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position + offsets, Speed * Time.fixedDeltaTime);

            rotateTowardPlayer();

            if (transform.position.x >= player.position.x)
            {
                offsets.x = -offset.x;
            }

            else if (transform.position.x <= player.position.x)
            {
                offsets.x = offset.x;
            }

            if (NextTimeToShoot <= 0)
            {
                GameObject bull = Instantiate(bullets, muzzle.position, Quaternion.identity);
                bull.GetComponent<bullets>().Demage = 5;
                bull.GetComponent<bullets>().target = -muzzle.up * 100;
                NextTimeToShoot = timeToShoot;
            }

            else
            {
                NextTimeToShoot -= Time.deltaTime;
            }
        }
    }

    void rotateTowardPlayer()
    {
        Vector3 currentpos = weapon.transform.position;
        Vector3 playerpos = player.position;

        currentpos.x = currentpos.x - playerpos.x;
        currentpos.y = currentpos.y - playerpos.y;

        float angle = Mathf.Atan2(currentpos.y, currentpos.x) * Mathf.Rad2Deg - 90f;
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
