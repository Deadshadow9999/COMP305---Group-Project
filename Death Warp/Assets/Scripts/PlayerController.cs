using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsHazard;

    // Private Variables
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isCrouching = false;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Fixed update is called once per frame
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        isGrounded = GroundCheck();
        isDead = HazardCheck();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
        }

        // Crouch code
        if (isGrounded && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isCrouching = true;
        }
        if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            isCrouching = false;
        }

        // Jump code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        rBody.velocity = new Vector2(horiz * speed, (rBody.velocity.y));

        // Check if the sprite needs to be flipped
        if((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        // Communicate with the animator
        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", (vert));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("isDead", isDead);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }

    private bool HazardCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsHazard);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

}
