using TMPro;
using UnityEngine;

public class WinGameTeleport : MonoBehaviour
{
    public TMP_InputField inputField;
    public Transform teleportDestination;
    public Rigidbody rb;

    private void Start()
    {
        inputField.onEndEdit.AddListener(CheckPassword);
    }
    private void CheckPassword(string userInput)
    {
        if (userInput.Equals("DELETE", System.StringComparison.OrdinalIgnoreCase))
        {
            Teleport();
            inputField.text = "";
        }
    }

    private void Teleport()
    {
        //move the player to the teleport destination
        rb.position = teleportDestination.position + Vector3.up; //Moving the player slightly above the plane

        //reset the player's velocity to prevent unintended movement after teleportation
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
