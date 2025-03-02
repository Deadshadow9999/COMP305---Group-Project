using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsHazard;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private LayerMask whatIsWallJumping;
    [SerializeField] private float respawnDelay;
    [SerializeField] private GameObject respawnEffectLocation;
    [SerializeField] private Text tutorialText;

    public GameObject checkPoint;
    public GameObject respawnAnimation;

    // Private Variables
    private GameController gameController;
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isWallJumping = false;
    private bool isFacingRight = true;
    private bool isCrouching = false;
    private bool isDying = false;
    private bool isDead = false;
    private bool isPushing = false;
    private bool isClimbingLadder = false;
    private float defaultSpeed;
    private float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        defaultSpeed = speed;
    }

    private void Update()
    {
        isDying = HazardCheck();
        // Teleport player to checkpoint on death
        if (isDying)
        {
            isDead = true;
            StartCoroutine(RespawnAtCheckPoint());
        }
    }

    // Fixed update is called once per frame
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        isGrounded = GroundCheck();
        isClimbingLadder = LadderCheck();

        if(isDying)
        {
            isDead = true;
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Crouch code
        if (!isDead)
        {
            if (!isClimbingLadder)
            {
                if (isGrounded && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    isCrouching = true;
                    speed = 0;
                }
                if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
                {
                    isCrouching = false;
                    speed = defaultSpeed;
                }
            }
        }
        // Jump code
        if (isGrounded && !isCrouching && !isDead && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        // Climbing ladder code
        if (isClimbingLadder)
        {
            rBody.velocity = new Vector2(horiz * speed, vert * speed);
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
        anim.SetBool("isDead", isDying);
        anim.SetBool("isPushing", isPushing);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }

    private bool HazardCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsHazard);
    }
    private bool LadderCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsLadder);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    IEnumerator RespawnAtCheckPoint()
    {
            gameController.IncrementDeathCounter();
            speed = 0;
            yield return new WaitForSeconds(respawnDelay);
            this.transform.position = checkPoint.transform.position;
            Instantiate(respawnAnimation, respawnEffectLocation.transform.position, respawnEffectLocation.transform.rotation);
            speed = defaultSpeed;
            isDying = false;
            isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GemTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(true);
            tutorialText.text = "Gems are valuable and can be collected";
        }

        if (other.CompareTag("PushableBoxTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(true);
            tutorialText.text = "Boxes can be pushed and used to avoid hazards";
        }

        if (other.CompareTag("MovingPlatformTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(true);
            tutorialText.text = "Moving platforms can be activated by interacting with the switch";
        }

        if (other.CompareTag("HazardsTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(true);
            tutorialText.text = "Death is not the end - you will respawn at the last checkpoint";
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            rBody.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            rBody.gravityScale = gravityScale;
        }

        if(other.CompareTag("GemTutorial"))
        {
                tutorialText.text = null;
                tutorialText.transform.parent.gameObject.SetActive(false);
        }

        if (other.CompareTag("PushableBoxTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(false);
            tutorialText.text = null;
        }

        if (other.CompareTag("MovingPlatformTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(false);
            tutorialText.text = null;
        }

        if (other.CompareTag("HazardsTutorial"))
        {
            tutorialText.transform.parent.gameObject.SetActive(false);
            tutorialText.text = null;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PushableObject") && speed > 0)
        {
            isPushing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PushableObject"))
        {
            isPushing = false;
        }
    }
}
