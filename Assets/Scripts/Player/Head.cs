using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public enum MODE
{
    FOLLOW,
    HIDE
}

public class HeadFollow : MonoBehaviour, ICommandListener
{
    public Player Player;
    public MODE mode = MODE.FOLLOW;

    public float transitionSpeed = 1.0f;
    public float rotationSpeed = 1.0f;
    private Vector3 hidePosition;
    private Quaternion hideRotation;

    public string ListenerName { get; set; } = "Head";

    private void Awake()
    {
        CommandProcessor.RegisterListener(this);
    }

    private void Update()
    {
        if(mode == MODE.FOLLOW)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.HeadMountPoint.transform.position,
                Player.MovementSpeed * Time.deltaTime);
        }
        else if(mode == MODE.HIDE)
        {
            transform.position = Vector3.Lerp(transform.position, hidePosition, transitionSpeed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, hideRotation, rotationSpeed * Time.deltaTime);
        }
    }

    [ContextMenu("Hide")]
    void Hide()
    {
        mode = MODE.HIDE;
    }
    [ContextMenu("Follow")]
    void Follow()
    {
        mode = MODE.FOLLOW;
    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "Hide":
                mode = MODE.HIDE;
                hidePosition = StringUtils.Vec3FromString(parameters[0]);
                hideRotation = Quaternion.Euler(StringUtils.Vec3FromString(parameters[1]));
                break;
            case "Follow":
                mode = MODE.FOLLOW;
                break;
            default: 
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }
}
