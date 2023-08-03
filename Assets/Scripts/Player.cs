using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public LayerMask canBeClicked;

    private NavMeshAgent character;

    private Animator animator;


    void Start()
    {
        character = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            /*var camera = GetComponentInChildren<Camera>();
            var mousePos2D = Input.mousePosition;
            var screenToCameraDistance = camera.nearClipPlane;

            var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);


            var worldPointPos = camera.ScreenToWorldPoint(mousePosNearClipPlane);

            Debug.Log("pressed");
            Debug.Log("moved to" + worldPointPos);

            character.SetDestination(worldPointPos);*/

            //animator.Play("walk");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,500))
            {
                character.SetDestination(hit.point);
                
                Debug.Log("moved to"+hit.point);
            }
        }

        //Debug.Log(character.velocity);

        if (character.velocity == Vector3.zero)
        {
            animator.SetInteger("Walk", 0);
            Debug.Log("idle");
        }
        else
        {
            animator.SetInteger("Walk", 1);
            Debug.Log("walk");
        }

        }
}
