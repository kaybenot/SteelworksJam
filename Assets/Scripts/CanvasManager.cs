using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CanvasManager : MonoBehaviour, ICommandListener
{
    public string ListenerName { get; set; } = "Canvas";

    private Animator m_Animator;

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "Show":
                m_Animator.SetTrigger("Show");
                break;
            case "Hide":
                m_Animator.SetTrigger("Hide");
                break;
            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }

    void Awake()
    {
        CommandProcessor.RegisterListener(this);

        m_Animator = GetComponent<Animator>();
    }
}
