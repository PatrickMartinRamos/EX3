using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapScript : MonoBehaviour
{
    //cameraScript _camScript;

    private void Awake()
    {
       // _camScript = FindAnyObjectByType<cameraScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //TODO: store player vector position --create savepoint.cs put collider as trigger -- when collided store player position same as savepoint
            //-- when collide here return to last save position

            playerScript playerScript = collision.gameObject.GetComponent<playerScript>();
            playerScript.resetToSavePos();

            //lipat to sa gate pag lilipat ng ibang level 
            // _camScript.changeLevel(cameraScript.Level.level_1);
        }
    }
}
