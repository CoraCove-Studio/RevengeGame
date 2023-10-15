using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
            health = 100;
        }
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        bool isCrit = false;
        if (characterDied)
        {
            return;
        }
        else
        {
            if (Random.Range(1, 6) == 1) { isCrit = true; }
            if (randomDmg) { damage = Random.Range(damage * 0.75f, (damage * 1.25f) + 1); }
            if (isCrit) { damage *= 2; }
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
                LevelTransitions lvlScript = GameObject.Find("InGameUI").GetComponent<LevelTransitions>();
                lvlScript.enemiesDefeated++;
                gameObject.tag = "Untagged";
                Behaviour moveScript = gameObject.GetComponent<EnemyMovement>();
                moveScript.enabled = false; // Needs to be disabled, otherwise enemy refuses to die.
                Component[] colliders = gameObject.GetComponentsInChildren<Collider>();
                foreach (var collider in colliders) { Destroy(collider); }
                Destroy(gameObject.GetComponent<Rigidbody>());
                //collider.excludeLayers = LayerMask.GetMask("Player"); // Stops the player from being able to collide with enemies on death.
            }

            return;
        }

        if (!is_Player)
        {
            if (isCrit) { currentDmgPopUp.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.yellow; }
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
        invincible = true;
        Image fill = healthBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Color originalColor = fill.color;
        for (var i = 0; i < 5; i++)
        {
            fill.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            fill.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
        // Play animation here?
        invincible = false;
    }
}
