using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlaceAnim : MonoBehaviour
{
    [SerializeField] private HeadFollow headfollow;
    public void SetMode()
    {
        headfollow.SetMode();
    }
    public void TurnOff()
    {
        CommandProcessor.SendCommand("Fade in");
        gameObject.SetActive(false);
    }
}
