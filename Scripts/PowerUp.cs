using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public float duration = 4f;
    public float multiplier = 2f;

    private GameController gameController;


    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
           StartCoroutine (Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        
        player.transform.localScale /= multiplier;

        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);
               
      
        player.transform.localScale *= multiplier;
        
        Destroy(gameObject);


    }
    

}
