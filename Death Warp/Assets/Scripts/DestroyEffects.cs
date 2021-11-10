using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffects : MonoBehaviour
{

    [Header("Animations")]
    [SerializeField] private GameObject teamLogo;
    [SerializeField] private GameObject explosionAnimation;

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void DisplayTeamLogo()
    {
        Instantiate(teamLogo, transform.position, transform.rotation);
    }

    public void DisplayExplosionAnimation()
    {
        Instantiate(explosionAnimation, transform.position, transform.rotation);
    }
}
