using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, ICommandListener
{
    private Player player;

    private bool blockPlayer = false;

    public string ListenerName { get; set; } = "PlayerController";

    private void Awake()
    {
        CommandProcessor.RegisterListener(this);

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

        if (blockPlayer) return;

        var value = context.ReadValue<Vector2>();
        player.Move(value);
    }
    
    public void Turn(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (blockPlayer) return;

        var value = context.ReadValue<Vector2>();
        player.Turn(value);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (blockPlayer) return;

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
        if (blockPlayer) return;

        if (context.started)
            player.Shoot();
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "Block":
                blockPlayer = true;
                break;
            case "Unblock":
                blockPlayer = false;
                break;
            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }
}
