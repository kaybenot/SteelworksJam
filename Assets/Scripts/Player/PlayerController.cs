using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    
    private void Awake()
    {
        player = GetComponent<Player>();
        if (player == null)
            Debug.LogAssertion("PlayerController could not find Player script!");
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
            player.Move(Vector2.zero);
        
        if (!context.performed)
            return;

        var value = context.ReadValue<Vector2>();
        player.Move(value);
    }
    
    public void Turn(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        var value = context.ReadValue<Vector2>();
        player.Turn(value);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
            player.Jump();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
            player.Interact();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
            player.Shoot();
    }
}
