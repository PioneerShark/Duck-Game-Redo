using static Game;
using UnityEngine;
using System.Collections.Generic;

public class Indicator2DTester : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject> {};

    void Start()
    {
        AreaIndicator2D areaIndicator2D = Manager.instance.IndicatorUI.CreateAreaIndicator();
        areaIndicator2D.duration = 10;
        areaIndicator2D.positionStart = points[0].transform.position;
        areaIndicator2D.positionEnd = points[3].transform.position;

        Main.Ping();
    }

    void Update()
    {
        
    }
}
