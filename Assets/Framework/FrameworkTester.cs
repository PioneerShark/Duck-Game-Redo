using static Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FrameworkTester : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject> {};
    public AudioClip audioClip;

    public List<Texture> textures = new List<Texture>() {};

    void Start()
    {
        ZoneIndicator2D areaIndicator2D = Game.IndicatorService.CreateZone();
        areaIndicator2D.duration = 10;
        areaIndicator2D.positionStart = points[0].transform.position;
        areaIndicator2D.positionEnd = points[3].transform.position;
        areaIndicator2D.SetPosition(points[0].transform.position, points[3].transform.position);

        var (line, startZone, endZone) = Game.IndicatorService.CreateCompositeLine(points[1].transform.position, points[2].transform.position, -1, 1f);
        startZone.SetImage(textures[0]);
        endZone.SetImage(textures[1]);
        endZone.lookAt = null;
        endZone.SetSize(3);
    }

    void Update()
    {
        
    }
}
