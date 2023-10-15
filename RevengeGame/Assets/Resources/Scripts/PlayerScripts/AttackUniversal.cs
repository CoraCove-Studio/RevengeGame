using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool is_Player, is_Enemy;

    public GameObject hit_FX_Prefab;
    public GameObject dmgPopUpPrefab;

    private HealthScript hpScript;

    private void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if (hit.Length > 0)
        {
            if (is_Player)
            {
                Vector3 hitFX_Pos = hit[0].transform.position;
                GameObject popUp = Instantiate(dmgPopUpPrefab, Vector3.zero, Quaternion.identity);
                popUp.transform.parent = GameObject.Find("InGameUI").transform;
                hpScript = hit[0].gameObject.GetComponent<HealthScript>();
                hpScript.currentDmgPopUp = popUp;
                Destroy(popUp, 1.0f);
                Vector3 relativePos = Camera.main.WorldToScreenPoint(hit[0].transform.position);
                popUp.transform.position = new Vector3(Random.Range(relativePos.x - 1.0f, relativePos.x + 1.0f),
                    Random.Range(1.75f - 1.0f, 1.75f + 1.0f), Random.Range(relativePos.z - 1.0f, relativePos.z + 1.0f));
                hitFX_Pos.y += 1.3f;

                if (hit[0].transform.forward.x > 0)
                {
                    hitFX_Pos.x += 0.3f;
                }

                else if (hit[0].transform.forward.x < 0)
                {
                    hitFX_Pos.x -= 0.3f;
                }

                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);

                if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) || gameObject.CompareTag(Tags.LEFT_LEG_TAG))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }
            }

            if (is_Enemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
            }

            gameObject.SetActive(false);
        }
    }
}
