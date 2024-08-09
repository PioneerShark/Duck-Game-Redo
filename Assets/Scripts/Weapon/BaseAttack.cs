using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    int damage;
    List<GameObject> colliderIgnoreList;
    GameObject owner;

    protected virtual void OnDeath() 
    {
        Destroy(gameObject);
    }
}
