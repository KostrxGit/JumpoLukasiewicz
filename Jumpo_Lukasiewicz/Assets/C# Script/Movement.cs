using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;
    public float jumpValue = 0.0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1f), new Vector2(0.9f, 0.4f), 0f, groundMask);


        if (jumpValue > 0 || isGrounded == false)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        //Vector3 movment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movment * Time.deltaTime * moveSpeed;

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.03f;
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpValue >= 22f && isGrounded)
        {
            float tempx = moveInput * moveSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }


        if (isGrounded == true)
        {
            canJump = true;
        }
        else if(isGrounded == false)
        {
            canJump = false; 
        }

        if(isGrounded == false || canJump == false)
        {
            jumpValue = 0.0f;
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

    }


    void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1f), new Vector2(0.9f, 0.2f));
    }
}
