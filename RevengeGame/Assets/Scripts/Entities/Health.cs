using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool isPlayer;

    public int maxHP = 100;
    public int HP;
    private bool invincible = false;

    public bool randomDmg = true;
    public bool randomHeal = true;

    void Start()
    {
        HP = maxHP;
        isPlayer = gameObject.name == "Player";
    }

    void Update()
    {
        
    }

    // Will be called by other scripts.
    void Heal(float restoredHP)
    {
        if (randomHeal) { restoredHP = Random.Range(restoredHP * 0.75f, (restoredHP * 1.25f) + 1); }
        HP = Mathf.Clamp(HP + (int)restoredHP, 0, maxHP); // Stops health from going over the max HP.
        // Later on, call animation here that shows a green pop-up on screen for healed damage.
    }

    // Will be called by other scripts.
    void TakeDamage(float lostHP)
    {
        if (!invincible)
        {
            StartCoroutine(Invincible());
            if (randomDmg) { lostHP = Random.Range(lostHP * 0.75f, (lostHP * 1.25f) + 1); }
            if ((HP - (int)lostHP) > 0) { HP -= (int)lostHP; }
            else { Die(); }
            Debug.Log(HP);
            // Same as Heal(), add animation later for damage pop-up.
            // Shake animation too?
        }
    }

    void Die()
    {
        if (isPlayer) { } // GAME OVER
        else
        {
            // Play death audio?
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && isPlayer)
        {
            TakeDamage(10);
        }
    }

    // Player becomes temporarily invincible when hit.
    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(2.0f);
        // Play animation here?
        invincible = false;
    }
}
