using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int HP;

    public bool randomDmg = true;
    public bool randomHeal = true;

    public int randomness = 10; // Range of HP/DMG randomness.

    void Start()
    {
        HP = maxHP;
    }

    void Update()
    {
        
    }

    // Will be called by other scripts.
    void Heal(int restoredHP)
    {
        if (randomHeal) { restoredHP = Random.Range(restoredHP - randomness, restoredHP + randomness + 1); }
        HP = Mathf.Clamp(HP + restoredHP, 0, maxHP); // Stops health from going over the max HP.
        // Later on, call animation here that shows a green pop-up on screen for healed damage.
    }

    // Will be called by other scripts.
    void TakeDamage(int lostHP)
    {
        if (randomDmg) { lostHP = Random.Range(lostHP - randomness, lostHP + randomness + 1); }
        if ((HP -= lostHP) > 0) { HP -= lostHP; }
        else { Die(); }
        // Same as Heal(), add animation later for damage pop-up.
        // Shake animation too?
    }

    void Die()
    {
        if (gameObject.name == "Player") { } // GAME OVER
        else
        {
            // Play death audio?
            Destroy(gameObject);
        }
    }
}
