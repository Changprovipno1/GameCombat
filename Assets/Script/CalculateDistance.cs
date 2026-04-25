using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistance : MonoBehaviour
{
    public float GetSqrDistanceToPlayer(Enemy enemy)
    {
        Vector3 offset = enemy.transform.position - transform.position;
        float distance = offset.sqrMagnitude;
        return distance;
    }
}
