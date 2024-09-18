using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject wave;
    void Awake() { 
       wave.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null) {
            wave.SetActive(true);
        }
    }
}
