using UnityEngine;
using UnityEngine.Events;

public abstract class Shootable : MonoBehaviour {

    public UnityEvent onShot;

    public virtual void OnShot() {
        onShot.Invoke();
    }
}