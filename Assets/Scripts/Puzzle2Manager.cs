using Unity.Netcode;
using UnityEngine;

public class Puzzle2Manager : NetworkBehaviour
{
    [SerializeField] private int[] correctSequence = { 1, 3, 0, 2 };
    public bool puzzleActive = true;

    private int currentStep = 0;

    [ServerRpc(RequireOwnership = false)]
    public void StepOnPlateServerRpc(int plateIndex)
    {
        if (!puzzleActive) return;
        if (plateIndex == correctSequence[currentStep])
        {
            currentStep++;
            Debug.Log("Correct!");
            if (currentStep >= correctSequence.Length)
            {
                PuzzleSolved();
            }
        }
        else
        {
            Debug.Log("WRONG!");
            ResetPuzzle();
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("PUZZLE SOLVED");
        currentStep = 0;
        puzzleActive = false;

        // idk do something
    }

    private void ResetPuzzle()
    {
        currentStep = 0;
        Debug.Log("Puzzle reset");
    }
}
