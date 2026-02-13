using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    public Transform teleportDestination;
    private Rigidbody rb;
    
   private void OnCollisionEnter(Collision other)
    {

        //if player collides with teleport start platform, teleport them
        if (other.gameObject.CompareTag("Player"))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            Teleport();
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
