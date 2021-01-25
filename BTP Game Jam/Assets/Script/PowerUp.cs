using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PowerUp", menuName = "PowerUp", order = 0)]
public class PowerUp : ScriptableObject
{
    public float HealthPoint;
    public float Speed;
    public float FireRate;
    public float Demage;
}