using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unpoweredWireBehavior : MonoBehaviour
{
    unpoweredWireStat unpoweredWireS;

    // Start is called before the first frame update
    void Start()
    {
        unpoweredWireS = gameObject.GetComponent<unpoweredWireStat>();
    }

    // Update is called once per frame
    void Update()
    {
        manageLight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<poweredWireStats>())
        {
            poweredWireStats poweredWireS = collision.GetComponent<poweredWireStats>();
            if (poweredWireS.objetColor == unpoweredWireS.objectColor)
            {
                poweredWireS.connected = true;
                unpoweredWireS.connected = true;
                poweredWireS.connectedPos = gameObject.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<poweredWireStats>())
        {
            poweredWireStats poweredWireS = collision.GetComponent<poweredWireStats>();

            poweredWireS.connected = false;
            unpoweredWireS.connected = false;
        }
    }

    void manageLight()
    {
        if (unpoweredWireS.connected)
        {
            unpoweredWireS.poweredLight.SetActive(true);
            unpoweredWireS.unPoweredLight.SetActive(false);
        }
        else
        {
            unpoweredWireS.poweredLight.SetActive(false);
            unpoweredWireS.unPoweredLight.SetActive(true);
        }
    }
}
