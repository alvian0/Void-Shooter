using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 5, jumpHeight = 10, GroundCheckDistance = 0.1f;
    public GameObject weapon, bullet;
    public GameObject GroundCheck;
    public LayerMask WhatIsGround;
    public Transform muzzle;
    public float FireRate = 15f;
    public float HP = 100;
    public float Demage = 2f;
    public float bulletSpeed = 30;
    public int JumpCount = 2;
    public GameObject JumpEffect;
    public GameObject TrailRender;
    public int TotalAmmo = 60, CurrentAmmo = 30, AmmoCap = 30;

    int Jump;
    float NextTimeToShoot;
    Animator anim;
    Rigidbody2D rb;
    bool IsGround;
    bool IsReloading = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheck.transform.position, GroundCheckDistance, WhatIsGround);

        if (IsGround) Jump = JumpCount;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
        }

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Jump >= 1)
            {
                rb.velocity = Vector2.up * jumpHeight;
                Instantiate(JumpEffect, GroundCheck.transform.position, Quaternion.identity);
                GameObject.Find("JumpSFX").GetComponent<AudioSource>().Play();
                anim.SetTrigger("Jump");
                Jump--;
            }
        }

        if (Input.GetMouseButton(0) && Time.time >= NextTimeToShoot && !IsReloading)
        {
            NextTimeToShoot = Time.time + 1f / FireRate;

            if (CurrentAmmo > 0)
            {
                Shoot();
                CurrentAmmo--;
            }

            else
            {
                IsReloading = true;

                if (TotalAmmo >= AmmoCap)
                {
                    TotalAmmo -= AmmoCap;
                    CurrentAmmo += AmmoCap;
                }

                else
                {
                    CurrentAmmo = TotalAmmo;
                    TotalAmmo = 0;
                }
                //reload
                //taking total ammo and add it into current ammo with capammo amount
            }
        }



        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, 0, 0);
            HP -= 10;
            TrailRender.SetActive(false);
            return;
        }

        if (transform.position.y > -10)
        {
            TrailRender.SetActive(true);
        }

        WeaponRotation();
    }
    private void FixedUpdate()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveInput * Speed, rb.velocity.y);
    }

    void WeaponRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -10f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(weapon.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        GameObject.Find("ShootSFX").GetComponent<AudioSource>().Play();
        GameObject bull = Instantiate(bullet, muzzle.position, muzzle.rotation);
        bull.GetComponent<Rigidbody2D>().AddForce(muzzle.up * bulletSpeed, ForceMode2D.Impulse);
        bull.GetComponent<PlayerBullets>().Demage = Demage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.transform.position, GroundCheckDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(JumpEffect, GroundCheck.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ammo")
        {
            TotalAmmo += AmmoCap;
            Destroy(collision.gameObject);
        }
    }
}