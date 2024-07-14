using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : Weapon {

    [Header("Shooting")]
    /// <summary>
    /// Pool of projectiles. Contains a field for the sample projectile this gun will shoot.
    /// </summary>
    [SerializeField] private ProjectilePool _projectilePool;

    [SerializeField] private ShootableAutoDetector entityDetector;
    
    [SerializeField] private Transform muzzleLocation; // The place where bullets come out of
    
    /// <summary>
    /// Number of bullet the gun fires each time it shoots
    /// </summary>
    [SerializeField] private uint bulletsPerShot = 1;

    /// <summary>
    /// How much degrees the shot may move when shooting a target.
    /// </summary>
    [Range(0,45)]
    [SerializeField] private float dispersion = 0;

    [SerializeField] private Shootable target;

    private void Awake() {
        entityDetector.onObjectDetected += TryUpdateTarget;
        entityDetector.onObjectLost += TryUpdateTarget;
    }

    private void OnDestroy() {
        entityDetector.onObjectDetected -= TryUpdateTarget;
        entityDetector.onObjectLost -= TryUpdateTarget;
    }

    private void Update() {
        // Rotate towards target, if it exists
        if (!target) {
            return;
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
    }
    
    private void TryUpdateTarget(object sender, Shootable shootable) {
        target = entityDetector.GetClosestObject();
    }
    
    public override void Attack() {
        Shoot();
    }

    public void Shoot() {
        for (int i = 0; i < bulletsPerShot; i++) {
            Projectile projectile = _projectilePool.OccupyOne();
            projectile.transform.position = muzzleLocation.position;
            projectile.gameObject.SetActive(true);
            projectile.OnShotBy(this);
            projectile.transform.Rotate(transform.forward, Random.Range(-dispersion, dispersion));
            projectile.onLifeSpanFinished += ReturnProjectileToPool;
        }
    }
    
    private void ReturnProjectileToPool(object sender, Projectile projectile) {
        _projectilePool.ReturnToPool(projectile);
        projectile.gameObject.SetActive(false);
        projectile.onLifeSpanFinished -= ReturnProjectileToPool;
    }

    private void OnDrawGizmosSelected() {
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.color = transparentRed;
        Mesh angleMesh = new Mesh();
        Vector3 minDispersionAnglePosition = transform.position + Quaternion.AngleAxis(dispersion, Vector3.forward) * transform.up;
        Vector3 maxDispersionAnglePosition = transform.position + Quaternion.AngleAxis(-dispersion, Vector3.forward) * transform.up;
        angleMesh.vertices = new[] { transform.position, minDispersionAnglePosition, maxDispersionAnglePosition };
        angleMesh.triangles = new[] { 0, 1, 2 };
        angleMesh.RecalculateNormals();
        angleMesh.RecalculateTangents();
        Gizmos.DrawMesh(angleMesh);
    }
}
