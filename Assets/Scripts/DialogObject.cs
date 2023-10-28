using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/CreateDialog", order = 1)]
public class DialogObjectSO : ScriptableObject
{
    public string text;
    public AudioClip clip;
}
