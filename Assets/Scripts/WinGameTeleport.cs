using TMPro;
using UnityEngine;
using Unity.Netcode;

public class WinGameTeleport : NetworkBehaviour
{
    public TMP_InputField inputField;
    public Transform teleportDestination;
    private NetworkBehaviour localPlayer;

    public void SetLocalPlayer(NetworkBehaviour player)
    {
        localPlayer = player;
    }

    private void Start()
    {
        inputField.onEndEdit.AddListener(CheckPassword);
    }

    private void CheckPassword(string userInput)
    {
        if (localPlayer == null || !localPlayer.IsOwner) return;

        if (userInput.Equals("DELETE", System.StringComparison.OrdinalIgnoreCase))
        {
            TeleportAllPlayersServerRpc();
            inputField.text = "";
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void TeleportAllPlayersServerRpc()
    {
        Vector3 targetPos = teleportDestination.position + Vector3.up;
        TeleportAllPlayersClientRpc(targetPos);
    }

    [ClientRpc]
    private void TeleportAllPlayersClientRpc(Vector3 targetPos)
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject != null)
            {
                if (client.ClientId == NetworkManager.Singleton.LocalClientId)
                {
                    Rigidbody rb = client.PlayerObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.position = targetPos;
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }
                }
            }
        }
    }


}
