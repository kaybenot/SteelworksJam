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
        CommandProcessor.SendCommand($"Head Hide {hidePoint.position.x} {hidePoint.position.y} {hidePoint.position.z} {hidePoint.rotation.eulerAngles.x} {hidePoint.rotation.eulerAngles.y} {hidePoint.rotation.eulerAngles.z}");
        CommandProcessor.SendCommand("PlayerController Block");
        CommandProcessor.SendCommand("Canvas ShowVignette");
        // Play sound
    }

    [ContextMenu("Unhide")]
    public void Unhide()
    {
        CommandProcessor.SendCommand("Head Follow");
        CommandProcessor.SendCommand("PlayerController Unblock");
        CommandProcessor.SendCommand("Canvas HideVignette");
    }
}
