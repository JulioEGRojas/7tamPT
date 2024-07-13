using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour {

    [Header("Movement Input")]
    [SerializeField] private bl_Joystick moveJoystick;
    
    [SerializeField] private InputActionReference movementAction;

    [SerializeField] private Vector2ScriptableValue moveVector;

    private bool _immunityOnCooldown;

    private void Start() {
        // This is needed because if we loose while moving and restart, value starts as not zero. 
        moveVector.SetValue(Vector2.zero);
    }

    private void OnEnable() {
        // Register input callbacks
        movementAction.action.performed += OnMovementActionPerformed;
    }

    private void OnDisable() {
        // Unregister input callbacks
        movementAction.action.performed -= OnMovementActionPerformed;
    }

    private void Update() {
        moveVector.SetValue(new Vector2(moveJoystick.Horizontal,moveJoystick.Vertical));
    }

    /// <summary>
    /// Movement action only sets the value of the movement vector. Player script then uses it to move.
    /// </summary>
    /// <param name="obj"></param>
    private void OnMovementActionPerformed(InputAction.CallbackContext obj) {
        Vector2 axisMovementInput = obj.ReadValue<Vector2>();
        moveVector.SetValue(axisMovementInput);
    }
}
