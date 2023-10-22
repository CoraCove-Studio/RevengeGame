using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Calculate the vector from the enemy to the main camera
        Vector3 enemyToCamera = mainCamera.transform.position - transform.position;

        // Calculate the angle between the enemyToCamera vector and the x-axis in the enemy's transform space
        float angle = Mathf.Atan2(enemyToCamera.y, enemyToCamera.x) * Mathf.Rad2Deg;

        // Apply the calculated rotation to the health bar
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
