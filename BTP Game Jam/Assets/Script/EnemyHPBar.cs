using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Slider HpBar;
    public Vector3 offset;
    public int index = 1;

    float Hp;

    private void Start()
    {
        check();

        HpBar.maxValue = Hp;
    }
    void Update()
    {
        check();

        if (index == 3)
        {
            HpBar.transform.position = transform.parent.GetChild(0).transform.position + offset;
        }

        else
        {
            HpBar.transform.position = transform.parent.position + offset;
        }

        HpBar.value = Hp;
    }

    void check()
    {
        if (index == 1)
        {
            Hp = transform.parent.gameObject.GetComponent<Enemy>().HP;
        }

        else if (index == 2)
        {
            Hp = transform.parent.gameObject.GetComponent<Enemy2>().HP;
        }

        else if (index == 3)
        {
            Hp = transform.parent.GetChild(0).gameObject.GetComponent<Enemy3>().HP;
        }
    }
}