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
    private float AttackCoolDown;
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
        AttackCoolDown = 1.4f;
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
            nav.isStopped = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                nav.isStopped = false;
                nav.destination = hit.point;

            }
             
            anim.SetBool("running", true);
            Debug.Log(currPos);
        }

        if (Input.GetMouseButton(1))
        {
            anim.SetBool("running", false);
            nav.isStopped = true;
            IsAttacking = true;

            /*if (pos.x != 0 || pos.y != 0)
            {

                currPos = initialPos;

                Debug.Log(currPos);
            }*/
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                nav.isStopped = true;
                if (CanAttack)
                {
                    Debug.Log("canattack");
                    SwordAttack();
                    IsAttacking = false;

                }

                nav.isStopped = false;
                nav.destination = hit.point;

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

        if (IsAttacking == true)
        {
            anim.SetBool("running", false);
        }

        if (velocitySpeed != 0 && CanAttack == true)
        {
            anim.SetBool("running", true);

        }
        if (velocitySpeed == 0 )
        {
            anim.SetBool("running", false);
        }
    }

    public void SwordAttack()
    {
        Debug.Log(CanAttack);
        CanAttack = false;
        IsAttacking = true;
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
    }
}