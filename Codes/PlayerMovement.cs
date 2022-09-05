using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent nav;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;
    private float x;
    private float z;
    private float velocitySpeed;

    CinemachineTransposer ct;
    public CinemachineVirtualCamera playerCam;
    private Vector3 pos;
    private Vector3 currPos;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        ct = playerCam.GetCinemachineComponent<CinemachineTransposer>();
        currPos = ct.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {

        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = x + z;


        // Get mouse position
        pos = Input.mousePosition;
        ct.m_FollowOffset = currPos;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                nav.destination = hit.point;
            }
        }

        if (Input.GetMouseButton(1))
        {
            if(pos.x !=0 || pos.y != 0 || pos.z != 0)
            {
                currPos = pos / 200;
            }
        }



        if (Input.GetKeyDown("space"))
        {
            anim.SetBool("attack", true);
        }

        if (velocitySpeed != 0)
        {
            anim.SetBool("sprinting", true);

        }
        if (velocitySpeed == 0)
        {
             anim.SetBool("sprinting", false);
        }
     }
}