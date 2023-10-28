using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        RenderSettings.fogDensity = 0.08f;
        // Temporarily
        CursorManager.HideCursor();
    }
}
