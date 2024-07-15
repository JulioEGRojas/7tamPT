using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Gun : Weapon {

    [Header("Shooting")]
    [Tooltip("Projectile's pool optimization for mobile devices. You can configure the gun damage per shot by editing the sample here.")]
    [SerializeField] private ProjectilePool _projectilePool;
    
    /// <summary>
    /// Number of bullet the gun fires each time it shoots
    /// </summary>
    [SerializeField] private uint bulletsPerShot = 1;

    /// <summary>
    /// How much degrees the shot may move when shooting a target. This angle is rendered on the gizmos.
    /// </summary>
    [Range(0,45)]
    [SerializeField] private float dispersion = 0;

    /// <summary>
    /// Callbacks when the gun shoots.
    /// </summary>
    public EventHandler onShoot;
    [SerializeField] public UnityEvent onShootEvent;
    
    [Header("Aiming")]
    [Tooltip("Entity Detector. Change the size of this object's collider to increase weapon range.")]
    [SerializeField] private ShootableAutoDetector entityDetector; 
    
    [SerializeField] private Transform muzzleLocation; // The place where bullets come out of.

    [SerializeField] private GameObject crossHair; // Shows where the target the gun is aiming at.

    [SerializeField] private Vector3 crossHairOffset; // How much it moves crosshair when aiming at a target.

    [SerializeField] private Shootable target; // Current target of this gun

    /// <summary>
    /// Time between each target update.
    /// </summary>
    private const float TARGET_UPDATE_TICK = 0.1f; 

    private void Awake() {
        onShoot += OnShoot;
        entityDetector.onObjectDetected += TryUpdateTarget;
        entityDetector.onObjectLost += TryUpdateTarget;
    }

    private void OnDestroy() {
        onShoot -= OnShoot;
        entityDetector.onObjectDetected -= TryUpdateTarget;
        entityDetector.onObjectLost -= TryUpdateTarget;
    }

    private void OnEnable() {
        StartCoroutine(UpdateTargetEach(TARGET_UPDATE_TICK));
    }

    private void OnDisable() {
        StopCoroutine(UpdateTargetEach(TARGET_UPDATE_TICK));
    }

    private void Update() {
        // Rotate towards target, if it exists
        if (!target) {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
            crossHair.gameObject.SetActive(false);
            return;
        }
        crossHair.gameObject.SetActive(true);
        crossHair.transform.position = target.transform.position + crossHairOffset;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
    }

    /// <summary>
    /// We try to update the target periodically. Can't do it every frame because older devices may lag. 
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public IEnumerator UpdateTargetEach(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);
            TryUpdateTarget(this,null);
        }
    }
    
    /// <summary>
    /// Tries to update the target, picking up the closest object. Used in callback form so that object detectors
    /// can use it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="shootable"></param>
    private void TryUpdateTarget(object sender, Shootable shootable) {
        target = entityDetector.GetClosestObject();
    }
    
    public override void Attack() {
        Shoot();
    }

    /// <summary>
    /// Fires, instantiating the amount of bullets specified in the configuration. 
    /// </summary>
    public void Shoot() {
        if (bulletsPerShot <= 0) {
            return;
        }
        onShoot?.Invoke(this,null);
        for (int i = 0; i < bulletsPerShot; i++) {
            // At instantiating, bullets are activated and rotated to face into the gun's rotation.
            Projectile projectile = _projectilePool.OccupyOne();
            projectile.transform.position = muzzleLocation.position;
            projectile.gameObject.SetActive(true);
            projectile.OnShotBy(this);
            projectile.transform.Rotate(transform.forward, Random.Range(-dispersion, dispersion));
            projectile.onLifeSpanFinished += ReturnProjectileToPool;
        }
    }
    
    /// <summary>
    /// Returns a projectile to this gun's pool, so it can be reused.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="projectile"></param>
    private void ReturnProjectileToPool(object sender, Projectile projectile) {
        _projectilePool.ReturnToPool(projectile);
        projectile.gameObject.SetActive(false);
        projectile.onLifeSpanFinished -= ReturnProjectileToPool;
    }
    
    private void OnShoot(object sender, EventArgs e) {
        onShootEvent.Invoke();
    }

    /// <summary>
    /// This draws a mesh showing the shot dispersion
    /// </summary>
    private void OnDrawGizmosSelected() {
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.color = transparentRed;
        Mesh angleMesh = new Mesh();
        Vector3 minDispersionAnglePosition = muzzleLocation.position + Quaternion.AngleAxis(dispersion, Vector3.forward) * transform.up;
        Vector3 maxDispersionAnglePosition = muzzleLocation.position + Quaternion.AngleAxis(-dispersion, Vector3.forward) * transform.up;
        angleMesh.vertices = new[] { muzzleLocation.position, minDispersionAnglePosition, maxDispersionAnglePosition };
        angleMesh.triangles = new[] { 0, 1, 2 };
        angleMesh.RecalculateNormals();
        angleMesh.RecalculateTangents();
        Gizmos.DrawMesh(angleMesh);
    }
}
