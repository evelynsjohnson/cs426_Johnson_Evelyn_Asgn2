using UnityEngine;
using Unity.Netcode; // Required for 'NetworkBehaviour'

public class LocalPlayerSetup : NetworkBehaviour
{
    // If your canvas has a specific name, change this string!
    private string canvasName = "WinGameCanvas";

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        // find canvas
        GameObject canvasObj = GameObject.Find(canvasName);

        if (canvasObj != null)
        {
            Camera myLocalCamera = GetComponentInChildren<Camera>();
            Canvas canvasComponent = canvasObj.GetComponent<Canvas>();
            if (canvasComponent != null && myLocalCamera != null)
            {
                canvasComponent.worldCamera = myLocalCamera;
            }

            // find the teleport script on the canvas
            WinGameTeleport teleportScript = canvasObj.GetComponent<WinGameTeleport>();

            // Assign local player
            if (teleportScript != null)
            {
                teleportScript.rb = GetComponent<Rigidbody>();
            }
        }
    }
}