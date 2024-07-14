using System;
using UnityEngine.Events;

public abstract class Shootable : Damageable {

    public EventHandler<Projectile> onShot;
    public UnityEvent onShotEvent;

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