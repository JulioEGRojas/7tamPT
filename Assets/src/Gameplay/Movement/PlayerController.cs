using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : EntityController {

    [SerializeField] private Vector2ScriptableValue moveVector;

    private void FixedUpdate() {
        SetVelocity(speed * moveVector.Value);
    }
}