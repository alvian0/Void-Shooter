using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Slider HpBar;
    public Vector3 offset;

    float Hp;

    private void Start()
    {
        Hp = transform.parent.gameObject.GetComponent<Enemy>().HP;
        HpBar.maxValue = Hp;
    }
    void Update()
    {
        Hp = transform.parent.gameObject.GetComponent<Enemy>().HP;
        HpBar.transform.position = transform.parent.position + offset;
        HpBar.value = Hp;
    }
}