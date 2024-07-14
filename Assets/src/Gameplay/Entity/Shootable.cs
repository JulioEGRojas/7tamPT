using System;
using UnityEngine.Events;

public abstract class Shootable : Damageable {

    public EventHandler<Projectile> onShot;
    public UnityEvent onShotEvent;

    /// <summary>
    /// Flag that allows detectors to detect or not this entity
    /// </summary>
    public bool invisible;

    protected override void SubscribeToEventHandlers() {
        base.SubscribeToEventHandlers();
        onShot += OnShot;
    }

    protected override void UnSubscribeToEventHandlers() {
        base.UnSubscribeToEventHandlers();
        onShot -= OnShot;
    }
    
    private void OnShot(object sender, Projectile projectile) {
        onShotEvent?.Invoke();
        ReceiveDamage(projectile.Damage);
    }
    
    public virtual void ReceiveProjectile(Projectile projectile) {
        onShot?.Invoke(this,projectile);
    }
}