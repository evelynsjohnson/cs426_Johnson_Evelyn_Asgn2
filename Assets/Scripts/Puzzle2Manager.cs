using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class Puzzle2Manager : NetworkBehaviour
{
    [SerializeField] private int[] correctSequence = { 1, 3, 0, 2 };
    [SerializeField] private Puzzle2Plate[] plates;
    [SerializeField] private GameObject solvedTextObject;
    public bool puzzleActive = true;
    private int currentStep = 0;
    private void Start()
    {
        solvedTextObject.SetActive(false);
    }
    [ServerRpc(RequireOwnership = false)]
    public void StepOnPlateServerRpc(int plateIndex)
    {
        if (!puzzleActive) return;
        if (plateIndex == correctSequence[currentStep])
        {
            currentStep++;
            Debug.Log("Correct!");
            CorrectPlateClientRpc(plateIndex);
            if (currentStep >= correctSequence.Length)
            {
                PuzzleSolved();
            }
        }
        else
        {
            Debug.Log("WRONG!");
            WrongPlateClientRpc();
            ResetPuzzle();
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("PUZZLE SOLVED");
        currentStep = 0;
        puzzleActive = false;
        AllPlatesGreenClientRpc();
        ShowSolvedTextClientRpc();
    }

    private void ResetPuzzle()
    {
        currentStep = 0;
    }
    [ClientRpc]
    private void ShowSolvedTextClientRpc()
    {
        solvedTextObject.SetActive(true);
    }

    [ClientRpc]
    private void CorrectPlateClientRpc(int plateIndex)
    {
        plates[plateIndex].SetGreen();
    }

    [ClientRpc]
    private void AllPlatesGreenClientRpc()
    {
        foreach (var plate in plates)
        {
            plate.SetGreen();
        }
    }

    [ClientRpc]
    private void WrongPlateClientRpc()
    {
        StartCoroutine(WrongFlash());
    }

    private IEnumerator WrongFlash()
    {
        foreach (var plate in plates)
        {
            plate.SetRed();
        }

        yield return new WaitForSeconds(1f);

        foreach (var plate in plates)
        {
            plate.ResetColor();
        }
    }
}
