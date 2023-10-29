using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    private static readonly int Hidden = Animator.StringToHash("hidden");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Show()
    {
        animator.SetBool(Hidden, false);
        foreach (var obstacle in obstacles)
            obstacle.SetActive(false);
    }

    public void Hide()
    {
        animator.SetBool(Hidden, true);
        foreach (var obstacle in obstacles)
            obstacle.SetActive(true);
    }
}
