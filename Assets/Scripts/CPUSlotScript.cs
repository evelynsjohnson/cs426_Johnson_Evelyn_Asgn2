using UnityEngine;

public class CPUSlotScript : MonoBehaviour
{
    public string requiredComponentTag; //tag of the component that needs to be place in this slot

    public bool isOccupied = false; //tracks if slot is occupied

    public Color correctColor = Color.green; //color to indicate correct component
    public Color incorrectColor = Color.red; //color to indicate incorrect component
    
    void OnTriggerEnter(Collider other)
    {
        //if slot is already occupied, return
        if (isOccupied) return; 

        //this checks if the object colliding does not have any of the valid tags
        if (!other.gameObject.CompareTag("RAM") && !other.gameObject.CompareTag("Register") && !other.gameObject.CompareTag("Cache") && !other.gameObject.CompareTag("MMU")){
            return;
        }

        //check if the component placed in the slot has the correct tag
        if (other.gameObject.CompareTag(requiredComponentTag)){
            Debug.Log("Correct Placement!");

            other.transform.position = transform.position + new Vector3(0,3f,0); //snap the component cube into place
            other.transform.rotation = transform.rotation; //align component cube rotation with the slot

            if (other.GetComponent<DragObject>() != null){
                Destroy(other.GetComponent<DragObject>()); //remove the drag script so the component cant be moved again
            }

            GameManagerCPU.instance.checkCompletion(); //call check completion to update game state

            isOccupied = true;
            GetComponent<Renderer>().material.color = correctColor; //change color to indicate correct component
        }
        else{
            isOccupied = false;
            GetComponent<Renderer>().material.color = incorrectColor; //change color to indicate incorrect component

            GameManagerCPU.instance.loseLife(); //call lose life to update game state
        }


    }
}
