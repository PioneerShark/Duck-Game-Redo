using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    void Start()
    {
        Manager.instance = this;   
    }
}
