using UnityEngine;
using UnityEngine.Events;

public abstract class Shootable : Damageable {

    public UnityEvent onShot;

    public virtual void OnShot() {
        onShot.Invoke();
    }
}