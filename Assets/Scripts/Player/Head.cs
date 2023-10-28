using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public enum MODE
{
    FOLLOW,
    HIDE
}

public class HeadFollow : MonoBehaviour
{
    public Player Player;
    public MODE mode = MODE.FOLLOW;

    public float transitionSpeed = 1.0f;
    public float rotationSpeed = 1.0f;
    public Transform hidePosition;
    public Transform hideLookAt;

    private void Update()
    {
        if(mode == MODE.FOLLOW)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.HeadMountPoint.transform.position,
                Player.MovementSpeed * Time.deltaTime);
        }
        else if(mode == MODE.HIDE)
        {
            transform.position = Vector3.Lerp(transform.position, hidePosition.position, transitionSpeed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, hideLookAt.rotation, rotationSpeed * Time.deltaTime);
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
}
