using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour, ICommandListener
{
    public string ListenerName { get; set; } = "Fade";

    private Animator animator;
    
    private void Awake()
    {
        CommandProcessor.RegisterListener(this);
        
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("Could not find Fade Animator!");
    }
    
    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "out":
                animator.SetBool("Enabled", true);
                break;
            case "in":
                animator.SetBool("Enabled", false);
                break;
            
            default:
                Debug.LogWarning($"Unimplemented command {command} in {ListenerName}!");
                break;
        }
    }
}
