using UnityEngine;

public class HidingPlace : MonoBehaviour, IInteractable
{
    public Transform hidePoint;

    public void Use(Player player)
    {
        Hide();
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        CommandProcessor.SendCommand($"Head.Hide", $"{hidePoint.position.ToString()};{hidePoint.rotation.eulerAngles.ToString()}");
        CommandProcessor.SendCommand("PlayerController.Block");
        // Play sound
    }

    [ContextMenu("Unhide")]
    public void Unhide()
    {
        CommandProcessor.SendCommand("Head.Follow");
        CommandProcessor.SendCommand("PlayerController.Unblock");
    }
}
