using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    [Header("Movement Input")]
    [SerializeField] private InputActionReference movementAction;

    [SerializeField] private Vector2ScriptableValue movementInputValue;

    private bool _immunityOnCooldown;

    private void Start() {
        // This is needed because if we loose while moving and restart, value starts as not zero. 
        movementInputValue.SetValue(Vector2.zero);
    }

    private void OnEnable() {
        // Register input callbacks
        movementAction.action.performed += OnMovementActionPerformed;
    }

    private void OnDisable() {
        // Unregister input callbacks
        movementAction.action.performed -= OnMovementActionPerformed;
    }

    /// <summary>
    /// Movement action only sets the value of the movement vector. Player script then uses it to move.
    /// </summary>
    /// <param name="obj"></param>
    private void OnMovementActionPerformed(InputAction.CallbackContext obj) {
        Vector2 axisMovementInput = obj.ReadValue<Vector2>();
        movementInputValue.SetValue(axisMovementInput);
    }
}
