using UnityEngine;

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
        if (!puzzleManager.puzzleActive) return;
        if (!other.CompareTag("Player")) return;

        puzzleManager.StepOnPlateServerRpc(plateIndex);
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
