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


    //If the player is in interaction range of collecting nuts
    bool bInNutRange = false;
    //If the player is in interaction range of feeding birds
    bool bInFeedRange = false;

    CapsuleCollider targetTreeCollider;
    CapsuleCollider targetSmallBirdCollider;

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
        //Collect nuts
        if (bInNutRange && Input.GetKeyDown(KeyCode.E))
        {
            //Add nuts
            if(nutsInHand < maxNutsInHand)
            {
                nutsInHand += 1;
                UpdateNutBoard();

                //Reset variables
                targetTreeCollider.enabled = false;
                interactionCanvas.enabled = false;
                targetTreeCollider = null;
                bInNutRange = false;
            }
        }
        //Feed bird
        else if (bInFeedRange && Input.GetKeyDown(KeyCode.E))
        {
            if (nutsInHand >= 1)
            {
                nutsInHand -= 1;
                UpdateNutBoard();
                Animator birdAnimator = targetSmallBirdCollider.GetComponent<Animator>();
                birdAnimator.SetTrigger("Eat");

                //Reset variables
                targetTreeCollider.enabled = false;
                interactionCanvas.enabled = false;
                targetTreeCollider = null;
                bInNutRange = false;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Collect Nuts
        if(other.tag == "Collectable")
        {
            bInNutRange = true;
            interactionCanvas.enabled = true;

            targetTreeCollider = other.GetComponent<CapsuleCollider>();
        }
        //
        else if(other.tag == "SmallBird")
        {
            bInFeedRange = true;
            interactionCanvas.enabled = true;

            targetSmallBirdCollider = other.GetComponent<CapsuleCollider>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            bInNutRange = false;
            interactionCanvas.enabled = false;

            targetTreeCollider = null;
        }

        else if (other.tag == "SmallBird")
        {
            bInFeedRange = false;
            interactionCanvas.enabled = false;

            targetSmallBirdCollider = null;
        }
    }

    //Used to update the nut count board
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
