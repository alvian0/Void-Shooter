using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 5, jumpHeight = 10, GroundCheckDistance = 0.1f, bulletSpeed = 5;
    public GameObject weapon, bullet;
    public GameObject GroundCheck;
    public LayerMask WhatIsGround;
    public Transform muzzle;
    public float FireRate = 15f;

    float NextTimeToShoot;
    Animator anim;
    Rigidbody2D rb;
    bool IsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheck.transform.position, GroundCheckDistance, WhatIsGround);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGround)
            {
                rb.velocity = Vector2.up * jumpHeight;
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(1,1,1);
        }

        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetMouseButton(0) && Time.time >= NextTimeToShoot)
        {
            NextTimeToShoot = Time.time + 1f / FireRate;
            Shoot();
        }

        WeapoRotation();
    }

    void WeapoRotation()
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
        GameObject bull = Instantiate(bullet, muzzle.position, Quaternion.identity);
        bull.GetComponent<bullets>().target = muzzle.up * 100;
    }

    private void FixedUpdate()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveInput * Speed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.transform.position, GroundCheckDistance);
    }
}