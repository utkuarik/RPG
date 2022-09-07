using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    private NavMeshAgent nav;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;
    private AnimatorStateInfo playerInfo;
    private float x;
    private float z;
    private float velocitySpeed;

    CinemachineTransposer ct;
    public CinemachineVirtualCamera playerCam;
    private Vector3 pos;
    private Vector3 currPos;
    public bool CanAttack;
    public bool IsAttacking;
    public float AttackCoolDown;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ct = playerCam.GetCinemachineComponent<CinemachineTransposer>();
        currPos = ct.m_FollowOffset;
        initialPos = ct.m_FollowOffset;
        CanAttack = true;
        AttackCoolDown = 4.0f;
        IsAttacking = false;
    } 


    // Start is called before the first frame update
    void Update()
    {
        playerInfo = anim.GetCurrentAnimatorStateInfo(0);

        x = nav.velocity.x;
        z = nav.velocity.z;
        velocitySpeed = x + z;

        Debug.Log(CanAttack);

        // Get mouse position
        pos = Input.mousePosition;
        ct.m_FollowOffset = currPos;

        if (Input.GetMouseButtonDown(0) && playerInfo.IsTag("NonAttack"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                nav.isStopped = false;
                nav.destination = hit.point;
                
            }
            anim.SetBool("attack", false);
        }

        if (Input.GetMouseButton(1))
        {
            if(pos.x !=0 || pos.y != 0 )
            {   

                currPos = initialPos;

                Debug.Log(currPos);
            }
        }



        if (Input.GetKeyDown("space"))
        {
            if (CanAttack)
            {
                Debug.Log(CanAttack);
                SwordAttack();
                
            }
            
            //anim.SetBool("sprinting", false);
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
    
    public void SwordAttack()
    {
        Debug.Log(CanAttack);
        CanAttack = false;
        anim.SetTrigger("attack");
        nav.isStopped = true;
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        IsAttacking = true;
        yield return new WaitForSeconds(AttackCoolDown);
        anim.SetBool("attack", false);
        CanAttack = true;
        IsAttacking = false;
        //gameObject.transform.position = v3_Original + new Vector3(3, 0, 0);
    }
}