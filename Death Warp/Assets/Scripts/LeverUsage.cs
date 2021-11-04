using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverUsage : MonoBehaviour
{
    public GameObject LeverDown;
    public GameObject Player;
    public GameObject Floor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LeverDown.SetActive(true);
            Floor.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
