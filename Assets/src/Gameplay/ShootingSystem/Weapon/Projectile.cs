using System;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float speed = 1f;

    /// <summary>
    /// How much does this bullet lasts active
    /// </summary>
    [SerializeField] private float lifeSpan = 2f;

    public EventHandler<Projectile> onLifeSpanFinished;

    public void OnShotBy(Gun gun) {
        // Rotate towards the gun's forward
        transform.up = gun.transform.up;
        Invoke(nameof(GhostImpact), lifeSpan);
    }

    /// <summary>
    /// Impacts nothing, triggering on life span finished callback. Useful to destroy bullets if they hit nothing.
    /// </summary>
    public void GhostImpact() {
        Impact(null);
    }

    private void FixedUpdate() {
        transform.Translate(Time.fixedDeltaTime * speed * Vector2.up);
    }

    public void Impact(Shootable shootable) {
        onLifeSpanFinished?.Invoke(this,this);
        CancelInvoke();
        if (!shootable) {
            return;
        }
    }
}
