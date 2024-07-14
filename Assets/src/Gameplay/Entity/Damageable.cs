using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour {

    [SerializeField] protected int health;
    public int Health => health;
    
    [SerializeField] protected int maxHealth;
    public int MaxHealth => maxHealth;
    
    [SerializeField] protected int minHealth;
    public int MinHealth => minHealth;
    
    public EventHandler onMaxHealthReached;
    public UnityEvent onMaxHealthReachedEvent;
    
    public EventHandler onMinHealthReached;
    public UnityEvent onMinHealthReachedEvent;

    protected virtual void Awake() {
        SubscribeToEventHandlers();
    }

    protected virtual void OnDestroy() {
        UnSubscribeToEventHandlers();
    }

    protected virtual void SubscribeToEventHandlers() {
        onMinHealthReached += OnMinHealthReached;
        onMaxHealthReached += OnMaxHealthReached;
    }

    protected virtual void UnSubscribeToEventHandlers() {
        onMinHealthReached -= OnMinHealthReached;
        onMaxHealthReached -= OnMaxHealthReached;
    }

    private void OnMinHealthReached(object sender, EventArgs e) {
        onMinHealthReachedEvent.Invoke();
    }
    
    private void OnMaxHealthReached(object sender, EventArgs e) {
        onMaxHealthReachedEvent.Invoke();
    }

    public void ReceiveDamage(int damage) {
        health = Mathf.RoundToInt(Mathf.Clamp(health - damage, minHealth, maxHealth));
        if (health == minHealth) {
            onMinHealthReached?.Invoke(this,null);
        }
        if (health == maxHealth) {
            onMaxHealthReached?.Invoke(this,null);
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

}
