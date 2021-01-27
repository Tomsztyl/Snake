using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyAI : MonoBehaviour
{
    private int myOrder;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private float rotHeadX, rotHeadY, rotHeadZ;
    // Start is called before the first frame update
    void Start()
    {
        //head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        for(int i=0;i<head.GetComponent<PlayerMove>().bodyParts.Count;i++)
        {
            if (gameObject==head.GetComponent<PlayerMove>().bodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
    }
    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float overTime = 0.5f;
    private void FixedUpdate()
    {
        if (myOrder == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
            transform.eulerAngles = new Vector3(rotHeadX, rotHeadY, rotHeadZ);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<PlayerMove>().bodyParts[myOrder - 1].position, ref movementVelocity, overTime);
            transform.eulerAngles = new Vector3(rotHeadX, rotHeadY, rotHeadZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotationVaraiblesHead();
    }
    private void RotationVaraiblesHead()
    {
        rotHeadX = head.eulerAngles.x;
        rotHeadY = head.eulerAngles.y;
        rotHeadZ = head.eulerAngles.z;
    }
}
