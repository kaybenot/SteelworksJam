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
    public Animator badSightAnimator;

    public RectTransform playerHealthBarFill;
    public RectTransform enemyHealthBarFill;

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
            case "SetEnemyHealth": { enemyHealthBarFill.localScale = new Vector3(float.Parse(parameters[0]), 1.0f, 1.0f); break; }

            case "ShowPlayerHealth": playerHealthAnimator.SetTrigger("Show"); break;
            case "HidePlayerHealth": playerHealthAnimator.SetTrigger("Hide"); break;
            case "SetPlayerHealth": { playerHealthBarFill.localScale = new Vector3(float.Parse(parameters[0]), 1.0f, 1.0f); break; }

            case "ShowHint": hintAnimator.SetTrigger("Show"); break;
            case "HideHint": hintAnimator.SetTrigger("Hide"); break;

            case "ShowVignette": vignetteAnimator.SetTrigger("Show"); break;
            case "HideVignette": vignetteAnimator.SetTrigger("Hide"); break;

            case "ShowBlood": bloodAnimator.SetTrigger("Show"); break;
            case "HideBlood": bloodAnimator.SetTrigger("Hide"); break;

            case "ShowBadSight": badSightAnimator.SetTrigger("Show"); break;
            case "HideBadSight": badSightAnimator.SetTrigger("Hide"); break;

            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }
}
