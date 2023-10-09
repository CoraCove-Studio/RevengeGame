using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health = 100;
    private GameObject dmgPopUp;
    private bool invincible = false;

    public bool randomDmg = true;
    public bool randomHeal = true;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;



    private void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if (characterDied)
        {
            return;
        }
        if (randomDmg) { damage = Random.Range(damage * 0.75f, (damage * 1.25f) + 1); }
        if (!invincible) { health -= (int)damage; }

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
                collider.excludeLayers = LayerMask.GetMask("Player"); // Stops the player from being able to collide with enemies on death.
                //collider.direction = 2;
                //collider.center = new Vector3(0, 0.2f, 1);
            }

            return;
        }
        else if (!invincible) { StartCoroutine(Invincible()); }

        if (!is_Player)
        {
            //dmgPopUp = GameObject.Find("DMG-PopUp(Clone)");
            //dmgPopUp.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)damage).ToString();

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
