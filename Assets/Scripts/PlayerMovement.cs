using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;
// because we are using the NetworkBehaviour class
// NewtorkBehaviour class is a part of the Unity.Netcode namespace
// extension of MonoBehaviour that has functions related to multiplayer
public class PlayerMovement : NetworkBehaviour
{
    public float speed = 2f;
    // create a list of colors
    public List<Color> colors = new List<Color>();

    // getting the reference to the prefab
    [SerializeField]
    private GameObject spawnedPrefab;
    // save the instantiated prefab
    private GameObject instantiatedPrefab;

    public GameObject cannon;
    public GameObject bullet;

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;
    // mouse look settings
    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;
    public float maxLookAngle = 80f;
    public bool lockCursor = true;
    private float xRotation = 0f;
    private bool mouseDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        // locking mouse so it doesnt move around & isnt visible
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // setting initial camera rotation
        if (playerCamera != null)
        {
            xRotation = playerCamera.transform.localEulerAngles.x;
            if (xRotation > 180f) xRotation -= 360f;
        }

    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;

        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }
        transform.position += moveDirection * speed * Time.deltaTime;
        // Mouse disabling/enabling
        if (Input.GetKeyDown(KeyCode.E))
        {
            mouseDisabled = !mouseDisabled;
            if (mouseDisabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        // mouse look stuff
        if (playerCamera != null && !mouseDisabled)
        {
            // getting mouse movements
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
            // rotating camera
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX); // rotating player
        }


        // if I is pressed spawn the object 
        // if J is pressed destroy the object
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     //instantiate the object
        //     instantiatedPrefab = Instantiate(spawnedPrefab);
        //     // spawn it on the scene
        //     instantiatedPrefab.GetComponent<NetworkObject>().Spawn(true);
        // }

        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     //despawn the object
        //     instantiatedPrefab.GetComponent<NetworkObject>().Despawn(true);
        //     // destroy the object
        //     Destroy(instantiatedPrefab);
        // }

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     // call the BulletSpawningServerRpc method
        //     // as client can not spawn objects
        //     BulletSpawningServerRpc(cannon.transform.position, cannon.transform.rotation);
        // }
    }

    // this method is called when the object is spawned
    // we will change the color of the objects
    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object
        // enable the camera and the audio listener
        audioListener.enabled = true;
        playerCamera.enabled = true;
    }

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void BulletSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        // call the BulletSpawningClientRpc method to locally create the bullet on all clients
        BulletSpawningClientRpc(position, rotation);
    }

    [ClientRpc]
    private void BulletSpawningClientRpc(Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bullet, position, rotation);
        newBullet.GetComponent<Rigidbody>().linearVelocity += Vector3.up * 2;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        // newBullet.GetComponent<NetworkObject>().Spawn(true);
    }
}