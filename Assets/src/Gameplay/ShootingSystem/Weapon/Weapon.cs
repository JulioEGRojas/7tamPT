using UnityEngine;

public abstract class Weapon : Equippable {
    
    /// <summary>
    /// Times the gun attacks per second
    /// </summary>
    [SerializeField] private float attackRate = 1;

    public abstract void Attack();
}