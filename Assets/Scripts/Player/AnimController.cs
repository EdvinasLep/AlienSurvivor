using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimController : MonoBehaviour
{
    Animator animator;
    MovementController movementController;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movementController = GetComponent<MovementController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movementController.moveDir.x != 0 || movementController.moveDir.y != 0) 
        {
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);

    }
}
