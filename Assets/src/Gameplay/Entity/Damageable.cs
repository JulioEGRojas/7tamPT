using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour {

    [SerializeField] protected int health;
    public int Health => health;
    
    [SerializeField] protected int maxHealth;
    public int MaxHealth => maxHealth;
    
    [SerializeField] protected int minHealth;
    public int MinHealth => minHealth;

    public void ReceiveDamage(int damage) {
        health = Mathf.RoundToInt(Mathf.Lerp(minHealth, maxHealth, health - damage));

    }

}
