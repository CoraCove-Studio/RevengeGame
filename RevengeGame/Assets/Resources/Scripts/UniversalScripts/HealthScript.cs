using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int health = 100;
    private GameObject dmgPopUp;
    private Slider healthBar;
    private GameObject healthBarBlink;
    private bool invincible = false;

    public bool randomDmg = true;
    public bool randomHeal = true;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;

    public GameObject currentDmgPopUp;

    private void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarBlink = GameObject.Find("InvincibilityFill");
        if (!is_Player)
        {
            health = 50;
        }
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if (characterDied)
        {
            return;
        }
        else
        {
            if (randomDmg) { damage = Random.Range(damage * 0.75f, (damage * 1.25f) + 1); }
            if (is_Player && !invincible)
            {
                health -= (int)damage;
                healthBar.value = health;
                StartCoroutine(Invincible());
            }
            else if (!is_Player) { health -= (int)damage; }
        }

        //display health ui

        if (health <= 0)
        {
            animationScript.Death();
            characterDied = true;

            if (is_Player)
            {

            }
            else
            {
                Behaviour moveScript = gameObject.GetComponent<EnemyMovement>();
                moveScript.enabled = false; // Needs to be disabled, otherwise enemy refuses to die.
                CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
                Destroy(gameObject.GetComponent<Rigidbody>());
                Destroy(collider);
                //collider.excludeLayers = LayerMask.GetMask("Player"); // Stops the player from being able to collide with enemies on death.
            }

            return;
        }

        if (!is_Player)
        {
            currentDmgPopUp.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)damage).ToString();

            if (knockDown)
            {
                if (Random.Range(0, 2) > 0)
                {
                    animationScript.KnockDown();
                }
            }

            else
            {
                if (Random.Range(0, 3) > 1)
                {
                    animationScript.Hit();
                }
            }
        }
    }

    // Entity becomes temporarily invincible when hit.
    IEnumerator Invincible()
    {
        //invincible = true;
        yield return new WaitForSeconds(2.0f);
        // Play animation here?
        invincible = false;
    }
}
