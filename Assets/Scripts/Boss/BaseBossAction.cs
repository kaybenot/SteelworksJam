using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossAction : MonoBehaviour
{
    public abstract SpecialActionType SpecialActionType { get; }
    public abstract void Init(Boss boss);
    public abstract void PlayAction(Boss boss);
}

public enum SpecialActionType
{
    OnAttack,
    
}