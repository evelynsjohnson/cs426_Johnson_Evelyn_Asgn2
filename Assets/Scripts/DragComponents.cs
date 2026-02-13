using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera; //retrieving main camera
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main; //retrieving the main camera in the scene
    }

    //calculating the offset between object and mouse position
    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    //positioning object where the mouse is dragging it
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    //retrieves the world position from the mouse position
    Vector3 GetMouseWorldPos()
    {
        Vector3 mouseCursorPosition = Input.mousePosition;
        mouseCursorPosition.z = 15f;

        return mainCamera.ScreenToWorldPoint(mouseCursorPosition); 
    }
}
