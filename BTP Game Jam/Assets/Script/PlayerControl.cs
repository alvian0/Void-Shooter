using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
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
    public float ReloadTime = 2f;
    public GameObject ReloadIndicator;
    public Image ReloadBar;
    public GameObject DashEffect;
    public int DashCount = 1;

    [SerializeField]
    bool IsCanDash;
    [SerializeField]
    int Jump;
    [SerializeField]
    int Dash;
    float NextTimeToShoot;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField]
    bool IsGround;
    bool IsReloading = false;
    GameManager manager;
    Vector2 temp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheck.transform.position, GroundCheckDistance, WhatIsGround);
        IsCanDash = Physics2D.OverlapCircle(GroundCheck.transform.position, GroundCheckDistance, WhatIsGround);

        if (IsCanDash) Dash = DashCount;

        if (IsGround) Jump = JumpCount;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
        }

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Dash >= 1)
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    StartCoroutine(dash(Vector2.right * 50));
                }

                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    StartCoroutine(dash(-Vector2.right * 50));
                }

                Dash--;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
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

        if (Input.GetKeyDown(KeyCode.R) && !IsReloading && CurrentAmmo < AmmoCap)
        {
            if (TotalAmmo > 0)
            {
                ReloadBar.fillAmount = 0;
                IsReloading = true;
            }
        }

        if (Input.GetMouseButton(0) && Time.time >= NextTimeToShoot && !IsReloading)
        {
            if (manager.IsChoosingPower)
            {
                return;
            }

            NextTimeToShoot = Time.time + 1f / FireRate;

            if (CurrentAmmo > 0)
            {
                Shoot();
                CurrentAmmo--;
            }

            else
            {
                ReloadBar.fillAmount = 0;
                IsReloading = true;
            }
        }

        if (IsReloading)
        {
            ReloadIndicator.SetActive(true);
            ReloadBar.fillAmount += Time.deltaTime / ReloadTime;

            if (ReloadBar.fillAmount >= 1)
            {
                if (CurrentAmmo >= 0)
                {
                    if (TotalAmmo >= AmmoCap - CurrentAmmo)
                    {
                        TotalAmmo -= AmmoCap - CurrentAmmo;
                        CurrentAmmo += AmmoCap - CurrentAmmo;
                    }

                    else
                    {
                        TotalAmmo -= AmmoCap - CurrentAmmo;
                        CurrentAmmo += TotalAmmo;
                    }
                }

                else
                {
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
                }

                ReloadIndicator.SetActive(false);
                IsReloading = false;
                return;
            }
        }

        WeaponRotation();
    }
    private void FixedUpdate()
    {
        float MoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(temp.x + MoveInput * Speed, rb.velocity.y);
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
        if (collision.tag == "Boss")
        {
            if (manager.IsChoosingPower)
            {
                return;
            }

            Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
            GameObject.Find("PHurt").GetComponent<AudioSource>().Play();
            HP -= 5;
            if (HPCheck())
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().PowerUpsPick();
        }

        if (collision.tag == "Ammo")
        {
            if (!IsReloading)
            {
                TotalAmmo += AmmoCap;
                Destroy(collision.gameObject);
            }
        }

        if (collision.tag == "Enemy3")
        {
            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().IsChoosingPower)
            {
                return;
            }

            Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
            GameObject.Find("PHurt").GetComponent<AudioSource>().Play();
            HP -= collision.gameObject.GetComponent<Enemy3>().Demage;
            if (HPCheck())
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().PowerUpsPick();
        }
    }
    bool HPCheck()
    {
        float[] i = { 90, 80, 70, 60, 50, 40, 30, 20, 10 };
        bool j;

        if (i.Contains(HP))
        {
            j = true;
        }

        else
        {
            j = false;
        }

        return j;
    }

    IEnumerator dash(Vector2 dir)
    {
        Instantiate(DashEffect, transform.position, Quaternion.identity);
        GameObject.Find("JumpSFX").GetComponent<AudioSource>().Play();
        temp = dir;
        yield return new WaitForSeconds(.1f);
        Instantiate(DashEffect, transform.position, Quaternion.identity);
        GameObject.Find("JumpSFX").GetComponent<AudioSource>().Play();
        temp = Vector2.zero;
    }
}