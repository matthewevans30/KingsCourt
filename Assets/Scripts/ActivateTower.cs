using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//this script helps keep track of where the tower is in the game
//if it is hovering over a spot on the board, we do not want to move it back to its spawnlocation
public class ActivateTower : MonoBehaviour
{
    //keeps tower from entering node that is already taken
    bool nodeOccupied;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("DefenseTower"))
        {
            Tower towerScript = args.interactableObject.transform.GetComponent<Tower>();
            
            if (towerScript != null)
            {
                if (nodeOccupied)
                {
                    towerScript.SendHome();
                }
                else
                {
                    towerScript.isOnBoard = true;
                    nodeOccupied = true;
                }   
            }
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("DefenseTower"))
        {
            Tower towerScript = args.interactableObject.transform.GetComponent<Tower>();

            if (towerScript != null)
            {
                Debug.Log("Leaving board");
                towerScript.isOnBoard = false;
                nodeOccupied = false;
            }
        }
    }
}
