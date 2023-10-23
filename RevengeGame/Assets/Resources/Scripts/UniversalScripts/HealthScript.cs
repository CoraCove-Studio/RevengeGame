using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int health = 100;
    private GameObject dmgPopUp;
    private Slider healthBar;
    private GameObject healthBarBlink;
    private RectTransform healthBarRectTransform;

    private bool invincible = false;

    public bool randomDmg = true;
    public bool randomHeal = true;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;

    [HideInInspector] public GameObject currentDmgPopUp;

    private GameObject enemyHP;

    private void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarBlink = GameObject.Find("InvincibilityFill");
        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (!is_Player)
        {
            GameObject enemyHP = Instantiate(Resources.Load<GameObject>("Prefabs/UI/EnemyHP"), Vector3.zero, Quaternion.identity);
            enemyHP.transform.parent = this.transform.GetChild(0).transform;
            enemyHP.transform.localPosition = new Vector3(0, 2.0f, 0);
            healthBar = enemyHP.transform.GetChild(0).GetComponent<Slider>();
        }
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        bool isCrit = false;
        if (characterDied)
        {
            LevelTransitions lvlScript = GameObject.Find("InGameUI").GetComponent<LevelTransitions>();
            StartCoroutine(lvlScript.FadeOut("LoseScreen"));
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
            else if (!is_Player)
            {
                health -= (int)damage;
                healthBar.value = health;
                enemyMovement.enabled = true;
            }
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
                Destroy(transform.GetChild(0).GetChild(transform.GetChild(0).childCount-1).gameObject);
                LevelTransitions lvlScript = GameObject.Find("InGameUI").GetComponent<LevelTransitions>();
                lvlScript.enemiesDefeated++;
                gameObject.tag = "Untagged";
                Behaviour moveScript = gameObject.GetComponent<EnemyMovement>();
                moveScript.enabled = false; // Needs to be disabled, otherwise enemy refuses to die.
                Component[] colliders = gameObject.GetComponentsInChildren<Collider>();
                foreach (var collider in colliders) { Destroy(collider); }
                Destroy(gameObject.GetComponent<Rigidbody>());
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
