using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mSpeed = 5f; // movement speed
    public int invert = -1; //invert goes negative, positive if you dont want it to
    private Animator playerAnimator;

    private float horizontalInput, verticalInput;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 finalDirection = new Vector3(horizontalInput, verticalInput, 1f);

        transform.position += direction * mSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 2.2f);

        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
        if (horizontalInput < 0)    // moving left
        {
            playerAnimator.SetBool("MovingLeft", true);
            playerAnimator.SetBool("MovingRight", false);
        }
   
        else if (horizontalInput > 0)
        {
            playerAnimator.SetBool("MovingLeft", false);
            playerAnimator.SetBool("MovingRight", true);
        }

        else
        {
            playerAnimator.SetBool("MovingLeft", false);
            playerAnimator.SetBool("MovingRight", false);
        }

    }
}
