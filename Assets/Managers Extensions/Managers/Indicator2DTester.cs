using static Framework;
using UnityEngine;
using System.Collections.Generic;

public class Indicator2DTester : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject> {};

    void Start()
    {
        AreaIndicator2D areaIndicator2D = Game.IndicatorService.CreateAreaIndicator();
        areaIndicator2D.duration = 10;
        areaIndicator2D.positionStart = points[0].transform.position;
        areaIndicator2D.positionEnd = points[3].transform.position;
        areaIndicator2D.SetPosition(points[0].transform.position, points[3].transform.position);
    }

    void Update()
    {
        
    }
}
