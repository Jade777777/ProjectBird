using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollecting : MonoBehaviour
{
    /*
     * UI related
     */
    Canvas interactionCanvas;
    GameObject nut1;
    GameObject nut2;
    GameObject nut3;


    //If the player is in interaction range
    bool bInRange = false;

    CapsuleCollider targetTreeCollider;

    //The nuts the player has
    int nutsInHand = 0;
    //The max nuts the player can have
    int maxNutsInHand = 3;

    private void Awake()
    {
        //Set up UI
        interactionCanvas = GameObject.Find("Canvas_Interaction").GetComponent<Canvas>();
        nut1 = GameObject.Find("Nut1");
        nut2 = GameObject.Find("Nut2");
        nut3 = GameObject.Find("Nut3");
        UpdateNutBoard();

        //Hide the instruction for interaction
        if (interactionCanvas)
        {
            interactionCanvas.enabled = false;
        }
    }

    private void Update()
    {
        if (bInRange && Input.GetKeyDown(KeyCode.E))
        {
            //Add nuts
            if(nutsInHand < maxNutsInHand)
            {
                nutsInHand += 1;
                UpdateNutBoard();
            }

            Debug.Log(nutsInHand);

            //Reset variables
            targetTreeCollider.enabled = false;
            interactionCanvas.enabled = false;
            targetTreeCollider = null;
            bInRange = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectable")
        {
            bInRange = true;
            interactionCanvas.enabled = true;

            targetTreeCollider = other.GetComponent<CapsuleCollider>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            bInRange = false;
            interactionCanvas.enabled = false;

            targetTreeCollider = null;
        }
    }

    private void UpdateNutBoard()
    {
        switch(nutsInHand)
        {
            case 0:
                nut1.SetActive(false);
                nut2.SetActive(false);
                nut3.SetActive(false);
                break;
            case 1:
                nut1.SetActive(true);
                nut2.SetActive(false);
                nut3.SetActive(false);
                break;
            case 2:
                nut1.SetActive(true);
                nut2.SetActive(true);
                nut3.SetActive(false);
                break;
            case 3:
                nut1.SetActive(true);
                nut2.SetActive(true);
                nut3.SetActive(true);
                break;
        }
    }
}
