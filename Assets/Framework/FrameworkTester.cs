using static Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FrameworkTester : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject> {};
    public AudioClip audioClip;

    void Start()
    {
        AreaIndicator2D areaIndicator2D = Game.IndicatorService.CreateAreaIndicator();
        areaIndicator2D.duration = 10;
        areaIndicator2D.positionStart = points[0].transform.position;
        areaIndicator2D.positionEnd = points[3].transform.position;
        areaIndicator2D.SetPosition(points[0].transform.position, points[3].transform.position);

        LineIndicator2D lineIndicator2D = Game.IndicatorService.CreateLineIndicator(points[1].transform.position, points[2].transform.position, -1);

        Game.AudioService.PlaySFX(audioClip, this.transform);
    }

    void Update()
    {
        
    }
}
