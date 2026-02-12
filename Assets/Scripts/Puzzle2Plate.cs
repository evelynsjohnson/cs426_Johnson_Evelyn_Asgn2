using Unity.Netcode;
using UnityEngine;

public class Puzzle2Plate : MonoBehaviour
{
    [SerializeField] private int plateIndex;
    [SerializeField] private Puzzle2Manager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!puzzleManager.puzzleActive) return;

        puzzleManager.StepOnPlateServerRpc(plateIndex);
    }
}
