using System;
using UnityEngine;

public class Gun : Weapon {

    [Header("Shooting")]
    [SerializeField] private Transform muzzleLocation; // The place where bullets come out of

    /// <summary>
    /// Pool of projectiles. Contains a field for the sample projectile this gun will shoot.
    /// </summary>
    [SerializeField] private ProjectilePool _projectilePool;
    
    /// <summary>
    /// Number of bullet the gun fires each time it shoots
    /// </summary>
    [SerializeField] private uint bulletsPerShot = 1;

    /// <summary>
    /// How much degrees the shot may move when shooting a target.
    /// </summary>
    [SerializeField] private float dispersion = 0;

    [SerializeField] private Transform target;

    private void Update() {
        // Rotate towards target, if it exists
        if (!target) {
            return;
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
    }

    public void Shoot() {
        for (int i = 0; i < bulletsPerShot; i++) {
            Projectile projectile = _projectilePool.OccupyOne();
            projectile.transform.position = muzzleLocation.position;
            projectile.gameObject.SetActive(true);
            projectile.onLifeSpanFinished += ReturnProjectileToPool;
            projectile.OnShotBy(this);
        }
    }

    public override void Attack() {
        Shoot();
    }
    
    private void ReturnProjectileToPool(object sender, Projectile projectile) {
        _projectilePool.ReturnToPool(projectile);
        projectile.gameObject.SetActive(false);
        projectile.onLifeSpanFinished -= ReturnProjectileToPool;
    }
}
