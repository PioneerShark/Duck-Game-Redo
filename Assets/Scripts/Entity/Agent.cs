using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : BaseEntity
{
    [HideInInspector]
    public Vector2 _targetPosition, _targetLookAtPosition;
    public float sightRange = 20f;
    public float attackRange = 15f; // Temporary stand in value that is meant to be gotten from an attack object

    public void MoveTo()
    {
        TriggerMoveTo(_targetPosition);
    }

    public void SetTarget(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public void LookAt()
    {

    }
    private void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().RemoveEnemy();
        }
    }
}
