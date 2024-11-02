using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireGameTrigger : MonoBehaviour
{
    [SerializeField] private Transform CamPos;
    [SerializeField] private GameObject playerInput;

    public bool isWireShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(CamPos.transform.position.x, CamPos.transform.position.y - .5f, CamPos.transform.position.z + 5);
    }

    void showWireGame()
    {
        if(isWireShowing)
        {
            gameObject.SetActive(true);
        }
        else if(!isWireShowing)
        {
            gameObject.SetActive(false);
        }
    }
}
