using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public bool startMoving = false;
    public bool KeepMoving = false;
    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (startMoving == true)
        {
            if (transform.position.y < -6.41)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
        }
        if(KeepMoving == true)
        {
            if (transform.position.y < 0.77)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
        }
        if (transform.position.y > -6.6)
        {
            trigger.SetActive(true);
        }
    }
}
