using GeneralNameSpace;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject wave;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            wave.SetActive(true);
        }
    }
}
