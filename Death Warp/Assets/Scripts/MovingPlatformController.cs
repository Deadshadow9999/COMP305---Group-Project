using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private Animator anim;
    private bool movingPlatformActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("platformSwitchActivated", movingPlatformActivated);
    }

    public void ToggleMovingPlatform()
    {
        movingPlatformActivated = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = this.transform;

            if (Input.GetKey(KeyCode.E))
            {
                ToggleMovingPlatform();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
