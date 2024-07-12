using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : BaseEntity
{
    [HideInInspector]
    public Vector2 _targetPosition, _targetLookAtPosition;

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
}
