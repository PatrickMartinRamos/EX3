using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapScript : MonoBehaviour
{
    //cameraScript _camScript;
    private storyTrigger[] storyTriggers;

    private void Start()
    {
        storyTriggers = FindObjectsOfType<storyTrigger>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //TODO: store player vector position --create savepoint.cs put collider as trigger -- when collided store player position same as savepoint
            //-- when collide here return to last save position--done

            playerScript playerScript = collision.gameObject.GetComponent<playerScript>();
            playerScript.resetToSavePos();

            foreach (storyTrigger trigger in storyTriggers)
            {
                trigger.activateTriggerColliderOnReset(true);
            }

        }
    }
}
