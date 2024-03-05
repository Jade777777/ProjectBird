using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCollecting : MonoBehaviour
{
    [SerializeField]
    GameObject FollowerPrefab;
    /*
     * UI related
     */
    TextMeshProUGUI interactionText;
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

    OnboardingBehavior onboarding = null;
    private void Awake()
    {
        //Set up UI
        interactionText = GameObject.Find("InteractionInstruction").GetComponent<TextMeshProUGUI>();
        nut1 = GameObject.Find("Nut1");
        nut2 = GameObject.Find("Nut2");
        nut3 = GameObject.Find("Nut3");
        UpdateNutBoard();

        //Hide the instruction for interaction
        if (interactionText)
        {
            interactionText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("None");
        }

        onboarding = GameObject.Find("Canvas").GetComponent<OnboardingBehavior>();
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
                interactionText.gameObject.SetActive(false);
                
                bInNutRange = false;

                //Change the tree color
                if(targetTreeCollider.gameObject.GetComponent<TreeController>() != null)
                {
                    targetTreeCollider.gameObject.GetComponent<TreeController>().changeMaterial();
                }
                targetTreeCollider = null;

                onboarding.OnCollection();
            }
        }
        //Feed bird
        else if (bInFeedRange && Input.GetKeyDown(KeyCode.E))
        {
            if (nutsInHand >= 1)
            {
                nutsInHand -= 1;
                UpdateNutBoard();
                Animator birdAnimator = targetSmallBirdCollider.GetComponentInChildren<Animator>();
                //Debug.Log("Eat");
                birdAnimator.SetTrigger("Eat");

                interactionText.gameObject.SetActive(false);
                //Instantiate(FollowerPrefab, targetSmallBirdCollider.transform.position, Quaternion.identity);
                //Destroy(targetSmallBirdCollider.gameObject);

                onboarding.OnFeed();
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Collect Nuts
        if(other.tag == "Collectable")
        {
            bInNutRange = true;
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Press E to collect";

            targetTreeCollider = other.GetComponent<CapsuleCollider>();
        }
        //
        else if(other.tag == "SmallBird" && nutsInHand != 0)
        {
            bInFeedRange = true;
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Press E to feed";

            targetSmallBirdCollider = other.GetComponent<CapsuleCollider>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            bInNutRange = false;
            interactionText.gameObject.SetActive(false);

            targetTreeCollider = null;
        }

        else if (other.tag == "SmallBird")
        {
            bInFeedRange = false;
            interactionText.gameObject.SetActive(false);


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
