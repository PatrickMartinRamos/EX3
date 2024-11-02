using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poweredWireBehavior : MonoBehaviour
{
    private bool mouseDown = false;
    [SerializeField] private  poweredWireStats powerWireS;
    LineRenderer line;
    public GameObject wireHead;
    public GameObject baseWire;
    // Start is called before the first frame update
    void Start()
    {
        powerWireS = gameObject.GetComponent<poweredWireStats>();
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWire();
        //Vector3 startPosition = baseWire.transform.position - new Vector3(0f, 0f, -.1f);
        //line.SetPosition(0, startPosition);
        //line.SetPosition(1, startPosition);
        //line.SetPosition(2, startPosition);

        line.SetPosition(0, new Vector3(baseWire.transform.position.x - .4f, baseWire.transform.position.y - .05f, baseWire.transform.position.z));
        line.SetPosition(1, new Vector3(baseWire.transform.position.x - .1f, baseWire.transform.position.y - .05f, baseWire.transform.position.z));
        line.SetPosition(2, new Vector3(baseWire.transform.position.x - .1f, baseWire.transform.position.y - .05f, baseWire.transform.position.z));

        line.SetPosition(3,new Vector3(wireHead.transform.position.x - .4f, wireHead.transform.position.y - .05f, wireHead.transform.position.z));
        line.SetPosition(4, new Vector3(wireHead.transform.position.x - .1f, wireHead.transform.position.y - .05f, wireHead.transform.position.z));
    }

    private void OnMouseDown()
    {
        mouseDown = true;
    }
    private void OnMouseOver()
    {
        powerWireS.movable = true;
    }
    private void OnMouseExit()
    {
        if (!powerWireS.moving)
            powerWireS.movable = false;
    }
    private void OnMouseUp()
    {
        mouseDown = false;
        if(!powerWireS.connected)
            gameObject.transform.position = powerWireS.startPosition;
        if (powerWireS.connected)
            gameObject.transform.position = powerWireS.connectedPos;

    }

    void MoveWire()
    {
        if(mouseDown && powerWireS.movable)
        {
            powerWireS.moving = true;
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 1));
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.parent.position.z);
        }
        else
        {
            powerWireS.moving = false;
        }

    }
}
