using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

enum DialogType
{
    TYPE_0, TYPE_1, TYPE_2, TYPE_3, TYPE_4
}

[Serializable]
public class DialogTypeInfo
{
    public DialogObjectSO[] dialogs;
    public int played = 0;
}

public class DialogManager : MonoBehaviour, ICommandListener
{
    public TextMeshProUGUI text;
    public DialogTypeInfo[] dialogs;
    bool isPlaying = false;
    int currentType = -1;
    private int dialogsPlayed = 0;

    private AudioSource audioSource;

    public string ListenerName { get; set; } = "Dialog";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        CommandProcessor.RegisterListener(this);
    }

    [ContextMenu("Play Next")]
    void TestPlayNext() { PlayNext(); }
    [ContextMenu("Test Type 0")]
    void TestType0() { PlayType(DialogType.TYPE_0); }
    [ContextMenu("Test Type 1")]
    void TestType1() { PlayType(DialogType.TYPE_1); }
    [ContextMenu("Test Type 2")]
    void TestType2() { PlayType(DialogType.TYPE_2); }
    [ContextMenu("Test Type 3")]
    void TestType3() { PlayType(DialogType.TYPE_3); }
    [ContextMenu("Test Type 4")]
    void TestType4() { PlayType(DialogType.TYPE_4); }


    void PlayType(DialogType type, bool showDialog = true)
    {
        if (isPlaying)
        {
            Debug.Log("Other Dialog is already being played!");
            return;
        }

        if(dialogs.Length == 0) {
            Debug.Log("No Dialog Options set!");
            return;
        }

        int typeId = (int)type;
        if (typeId != (typeId % dialogs.Length))
            Debug.Log("Missing Dialog Type!");
        typeId = typeId % dialogs.Length;

        DialogTypeInfo info = dialogs[typeId];

        if (info.played != 0)
        {
            Debug.Log("Dialog has played already!");
            return;
        }

        if(info.dialogs.Length == 0)
        {
            Debug.Log("DialogSO reference is null!");
            return;
        }

        if (showDialog)
        {
            CommandProcessor.SendCommand("Canvas.ShowDialog");
        }
        
        SetDialog(info.dialogs[0]);
        currentType = typeId;
        info.played = 1;
        isPlaying = true;
    }

    public void PlayNext()
    {
        PlayType((DialogType)(dialogsPlayed++));
    }
    public void PlayNextNoDialog()
    {
        PlayType((DialogType)(dialogsPlayed++), false);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void SetDialog(DialogObjectSO dialog)
    {
        audioSource.clip = dialog.clip;
        audioSource.Play();

        // Change UI Text
        text.text = dialog.text;
    }

    void Update()
    {
        if (isPlaying && audioSource.isPlaying == false)
        {
            var info = dialogs[currentType];
            if(info.played == info.dialogs.Length)
            {
                // Finished
                isPlaying = false;
                Debug.Log("Stopped playing");
                currentType = -1;
                // Hide Canvas
                CommandProcessor.SendCommand("Canvas.HideDialog");
            }
            else
            {
                SetDialog(info.dialogs[info.played++]);
            }
        }
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "Next":
                PlayNext();
                break;
            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }
}
