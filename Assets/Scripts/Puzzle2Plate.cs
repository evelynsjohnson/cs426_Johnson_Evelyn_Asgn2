using UnityEngine;
using Unity.Netcode;

public class Puzzle2Plate : MonoBehaviour
{
    [SerializeField] private int plateIndex;
    [SerializeField] private Puzzle2Manager puzzleManager;
    [SerializeField] private Renderer plateRenderer;

    private Color defaultColor;

    private void Awake()
    {
        defaultColor = plateRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!NetworkManager.Singleton.IsServer) return;
        if (!puzzleManager.puzzleActive) return;
        if (!other.CompareTag("Player")) return;
        if (!other.attachedRigidbody) return;
        if (!other.TryGetComponent<NetworkObject>(out var netObj)) return;
        if (!netObj.IsPlayerObject) return;

        puzzleManager.StepOnPlate(plateIndex);
    }

    public void SetGreen()
    {
        plateRenderer.material.color = Color.green;
    }

    public void SetRed()
    {
        plateRenderer.material.color = Color.red;
    }

    public void ResetColor()
    {
        plateRenderer.material.color = defaultColor;
    }
}
