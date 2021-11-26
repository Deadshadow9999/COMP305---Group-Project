using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private GameObject gemCollectionAnimation;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(gemCollectionAnimation, transform.position, transform.rotation);
            Destroy(this.gameObject);

            gameController.PickUpGem();
        }
    }
}
