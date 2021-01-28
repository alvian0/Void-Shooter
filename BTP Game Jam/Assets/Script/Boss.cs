using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float AttackSpeed;
    public float moveSpeed;
    public float cooldown = 5f;
    public GameObject bullets;
    public float demage = 5;
    public float HP = 2000, bulletSpeed = 40f;
    public bool IsShooting;
    public Transform[] Muzzle;
    public bool Imune = false;
    public float MeleeSpeed = 25f;
    public bool IsMeleeAttack = false;
    public Slider HpBar;
    public Text HpIndicatorText;
    public GameObject DeadParticle;

    GameObject player;
    float cooldowns;
    float ShootAttack;
    Vector3 target;
    bool IsIdle = true;
    Animator anim;
    float HalfHp;
    bool Phase2 = false;
    GameManager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        HalfHp = HP / 2;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cooldowns = cooldown;
        ShootAttack = AttackSpeed;
        HpBar.maxValue = HP;
    }

    void Update()
    {
        HpBar.value = HP;
        float CurrentHp = HP;
        float Clamped = Mathf.Clamp(CurrentHp, 0, HpBar.maxValue);
        HpIndicatorText.text = Clamped.ToString();

        if (HP <= HalfHp)
        {
            Phase2 = true;
            anim.SetBool("Phase2", true);
        }

        Physics2D.IgnoreLayerCollision(11, 8);
    }

    private void FixedUpdate()
    {
        if (player != null && IsMeleeAttack)
        {
            if (Vector2.Distance(transform.position, player.transform.position) >= 4f)
            {
                if (Phase2)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, MeleeSpeed * 1.5f * Time.fixedDeltaTime);
                }

                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, MeleeSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }

    public void Shoot()
    {
        if (ShootAttack <= 0 && IsIdle)
        {
            if (player != null)
            {
                for (int i = 0; i < Muzzle.Length; i++)
                {
                    GameObject bull = Instantiate(bullets, Muzzle[i].position, Quaternion.identity);
                    bull.GetComponent<BossBullets>().Demage = demage;
                    bull.GetComponent<Rigidbody2D>().AddForce(Muzzle[i].up * bulletSpeed, ForceMode2D.Impulse);
                }

                if (Phase2)
                {
                    ShootAttack = AttackSpeed / 2;
                }

                else
                {
                    ShootAttack = AttackSpeed;
                }
            }
        }

        else
        {
            ShootAttack -= Time.deltaTime;
        }
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
    }

    public void Particle()
    {
        Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
        GameObject.Find("EDie").GetComponent<AudioSource>().Play();
        Instantiate(DeadParticle, transform.position, Quaternion.identity);
    }

    public void SelfDestruck()
    {
        Camera.main.transform.parent.transform.GetComponent<Animator>().SetTrigger("Shake");
        GameObject.Find("EDie").GetComponent<AudioSource>().Play();
        Instantiate(DeadParticle, transform.position, Quaternion.identity);
        manager.timer = manager.CountDown;
        manager.IsThereIsABoos = false;
        manager.GetPoint(true);
        Destroy(gameObject);
    }

    public void ChooseAttack() { int index = Random.Range(0, 2); if (index == 0) anim.SetTrigger("Melee"); else anim.SetTrigger("Shooting"); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print(collision.gameObject);
        }
    }
}