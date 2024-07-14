using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] protected float speed;
    public float Speed => speed;
    private float _originalSpeed;
    
    protected Rigidbody2D m_rb;
    
    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        _originalSpeed = speed;
    }

    public void SetVelocity(Vector2 velocityValue) {
        m_rb.velocity = velocityValue;
    }

    public void SetSpeedForTime(float speed, float seconds) {
        StartCoroutine(SetSpeedForTimeCoroutines(speed, seconds));
    }

    private IEnumerator SetSpeedForTimeCoroutines(float speed, float seconds) {
        this.speed = speed;
        yield return new WaitForSeconds(seconds);
        this.speed = _originalSpeed;    
    }
}
