using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    
    /// <summary>
    /// Times the gun attacks per second
    /// </summary>
    [SerializeField] private float attackRate = 1;

    public abstract void Attack();
}