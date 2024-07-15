using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    [SerializeField] private int damage = 1;
    public int Damage => damage;

    [SerializeField] private float speed = 1f;

    /// <summary>
    /// How much does this bullet lasts active
    /// </summary>
    [SerializeField] private float lifeSpan = 2f;

    public EventHandler<Projectile> onLifeSpanFinished;

    /// <summary>
    /// Called when a gun shoots this bullet.
    /// </summary>
    /// <param name="gun"></param>
    public void OnShotBy(Gun gun) {
        // Rotate towards the gun's forward
        transform.up = gun.transform.up;
        // Impact 'nothing' after life span finishes
        Invoke(nameof(GhostImpact), lifeSpan);
    }

    /// <summary>
    /// Impacts nothing, triggering on life span finished callback. Useful to destroy bullets if they hit nothing.
    /// </summary>
    public void GhostImpact() {
        Impact(null);
    }

    /// <summary>
    /// Moves in the local up, which is set by the weapon when shot.
    /// </summary>
    private void FixedUpdate() {
        transform.Translate(Time.fixedDeltaTime * speed * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out Shootable shootable)) {
            Impact(shootable);
        }
    }

    /// <summary>
    /// Called when a shootable is impacted.
    /// </summary>
    /// <param name="shootable"></param>
    public void Impact(Shootable shootable) {
        onLifeSpanFinished?.Invoke(this,this);
        // Cancels the ghost impact
        CancelInvoke();
        if (!shootable) {
            return;
        }
        shootable.ReceiveProjectile(this);
    }
}
