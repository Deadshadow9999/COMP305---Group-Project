using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCrouchAnimation : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }
    }
}
