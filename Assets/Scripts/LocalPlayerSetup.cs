using UnityEngine;
using Unity.Netcode;

public class LocalPlayerSetup : NetworkBehaviour
{
    private string canvasName = "WinGameCanvas";

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        GameObject canvasObj = GameObject.Find(canvasName);
        if (canvasObj == null) return;

        Camera myLocalCamera = GetComponentInChildren<Camera>();
        Canvas canvasComponent = canvasObj.GetComponent<Canvas>();

        if (canvasComponent != null && myLocalCamera != null)
        {
            canvasComponent.worldCamera = myLocalCamera;
        }

        WinGameTeleport teleportScript = canvasObj.GetComponent<WinGameTeleport>();
        if (teleportScript != null)
        {
            teleportScript.SetLocalPlayer(this);
        }
    }
}
