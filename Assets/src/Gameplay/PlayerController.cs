using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed;

    [SerializeField] private Vector2ScriptableValue moveVector;
    
    private Rigidbody2D m_rb;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        m_rb.velocity = moveVector.Value * speed;
    }
}