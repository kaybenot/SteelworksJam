using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour, ICommandListener
{
    public string ListenerName { get; set; } = "Canvas";

    public Animator dialogAnimator;
    public Animator enemyHealthAnimator;
    public Animator playerHealthAnimator;
    public Animator hintAnimator;
    public Animator vignetteAnimator;
    public Animator bloodAnimator;

    void Awake()
    {
        CommandProcessor.RegisterListener(this);
        Debug.Log("RegisteringCanvas");
    }
    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "ShowDialog": dialogAnimator.SetTrigger("Show"); break;
            case "HideDialog": dialogAnimator.SetTrigger("Hide"); break;

            case "ShowEnemyHealth": enemyHealthAnimator.SetTrigger("Show"); break;
            case "HideEnemyHealth": enemyHealthAnimator.SetTrigger("Hide"); break;

            case "ShowPlayerHealth": playerHealthAnimator.SetTrigger("Show"); break;
            case "HidePlayerHealth": playerHealthAnimator.SetTrigger("Hide"); break;

            case "ShowHint": hintAnimator.SetTrigger("Show"); break;
            case "HideHint": hintAnimator.SetTrigger("Hide"); break;

            case "ShowVignette": vignetteAnimator.SetTrigger("Show"); break;
            case "HideVignette": vignetteAnimator.SetTrigger("Hide"); break;

            case "ShowBlood": bloodAnimator.SetTrigger("Show"); break;
            case "HideBlood": bloodAnimator.SetTrigger("Hide"); break;

            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }
}
