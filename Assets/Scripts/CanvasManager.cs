using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Animator weaponAnimator;
    public Animator weaponShootingAnimator;

    public Image enemyHealthBarFill;
    public GameObject playerHeartsParent;

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
            case "SetEnemyHealth": { enemyHealthBarFill.fillAmount = float.Parse(parameters[0]); break; }

            case "ShowPlayerHealth": playerHealthAnimator.SetTrigger("Show"); break;
            case "HidePlayerHealth": playerHealthAnimator.SetTrigger("Hide"); break;
            case "SetPlayerHearts": { SetPlayerHearts(int.Parse(parameters[0])); break; }

            case "ShowHint": hintAnimator.SetTrigger("Show"); break;
            case "HideHint": hintAnimator.SetTrigger("Hide"); break;

            case "ShowVignette": vignetteAnimator.SetTrigger("Show"); break;
            case "HideVignette": vignetteAnimator.SetTrigger("Hide"); break;

            case "ShowBlood": bloodAnimator.SetTrigger("Show"); break;
            case "HideBlood": bloodAnimator.SetTrigger("Hide"); break;

            case "ShowBadSight": badSightAnimator.SetTrigger("Show"); break;
            case "HideBadSight": badSightAnimator.SetTrigger("Hide"); break;

            case "ShowWeapons": weaponAnimator.SetTrigger("Show"); break;
            case "HideWeapons": weaponAnimator.SetTrigger("Hide"); break;

            case "ShootWeapons": weaponShootingAnimator.SetTrigger("Shoot"); break;

            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }

    private void SetPlayerHearts(int count)
    {
        const int maxCount = 5;
        if (count < 0 || count > maxCount) {
            Debug.LogWarning($"Illegal player hearts count: {count}");
        }

        for (int i = 0; i < playerHeartsParent.transform.childCount; ++i) {
            var child = playerHeartsParent.transform.GetChild(i).gameObject;
            if (child == null) break;

            if(i < count)
                child.SetActive(true);
            else
                child.SetActive(false);   
        }
    }


    [ContextMenu("Set Hearts 0")]
    void SetHearts0() { SetPlayerHearts(0); }
    [ContextMenu("Set Hearts 1")]
    void SetHearts1() { SetPlayerHearts(1); }
    [ContextMenu("Set Hearts 2")]
    void SetHearts2() { SetPlayerHearts(2); }
    [ContextMenu("Set Hearts 3")]
    void SetHearts3() { SetPlayerHearts(3); }
    [ContextMenu("Set Hearts 4")]
    void SetHearts4() { SetPlayerHearts(4); }
    [ContextMenu("Set Hearts 5")]
    void SetHearts5() { SetPlayerHearts(5); }
}
