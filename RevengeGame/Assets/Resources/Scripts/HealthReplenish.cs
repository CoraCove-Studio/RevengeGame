using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthReplenish : MonoBehaviour
{
    public GameObject pickupEffect;
    private HealthScript healthPlayer;
    private void Awake()
    {
        healthPlayer = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<HealthScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        healthPlayer.health += 25;
        Destroy(this.gameObject);
    }
}
