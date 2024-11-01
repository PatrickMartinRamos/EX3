using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawBladeScript : MonoBehaviour
{
    private storyTrigger[] storyTriggers;

    private void Start()
    {
        storyTriggers = FindObjectsOfType<storyTrigger>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerScript playerScript = collision.gameObject.GetComponent<playerScript>();
            playerScript.resetToSavePos();

            //when story is trigger it disable the collider of that trigger when the player die
            //the collider get enabled again
            foreach (storyTrigger trigger in storyTriggers)
            {
                trigger.activateTriggerColliderOnReset(true);
            }
        }
    }
}
