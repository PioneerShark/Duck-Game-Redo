using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorService : MonoBehaviour, IIndicatorService
{
    // CREATE TRACKING INDICATOR
    [SerializeField] private ZoneIndicator2D AreaIndicatorPrefab;
    [SerializeField] private LineIndicator2D LineIndicatorPrefab;
    private Texture lineStart, lineMiddle, lineEnd, lineEndSimple, lineMiddleSimple, lineStartSimple, areaBase;

    public void Setup()
    {
        AreaIndicatorPrefab = Resources.Load<ZoneIndicator2D>("Prefabs/AreaIndicator2D");
        LineIndicatorPrefab = Resources.Load<LineIndicator2D>("Prefabs/LineIndicator2D");
        areaBase = Resources.Load<Texture>("Indicator Sprites/Mark2");
        lineStart = Resources.Load<Texture>("Indicator Sprites/LineStart");
        lineMiddle = Resources.Load<Texture>("Indicator Sprites/LineMiddle");
        lineEnd = Resources.Load<Texture>("Indicator Sprites/LineEnd");
        //lineStartSimple = Resources.Load<RawImage>("Indicator Sprites/");
        //lineEndSimple = Resources.Load<RawImage>("Indicator Sprites/");
        
    }

    public ZoneIndicator2D CreateZone()
    {
        ZoneIndicator2D indicator2D = Instantiate(AreaIndicatorPrefab, Vector3.zero, Quaternion.identity);
        return indicator2D;
    }

    public ZoneIndicator2D CreateZone(Vector2 start, Vector2 end, float duration)
    {
        ZoneIndicator2D indicator2D = CreateZone();

        indicator2D.duration = duration;
        indicator2D.SetPosition(start, end);

        return indicator2D;
    }

    public ZoneIndicator2D CreateZone(Vector2 start, Vector2 end, float duration, float size)
    {
        ZoneIndicator2D indicator2D = CreateZone();

        indicator2D.SetImage(areaBase);
        indicator2D.duration = duration;
        indicator2D.SetPosition(start, end);
        indicator2D.SetSize(size);

        return indicator2D;
    }

    public LineIndicator2D CreateLine()
    {
        LineIndicator2D indicator2D = Instantiate(LineIndicatorPrefab, Vector3.zero, Quaternion.identity);
        return indicator2D;
    }

    public LineIndicator2D CreateLine(Vector2 start, Vector2 end, float duration)
    {
        LineIndicator2D indicator2D = CreateLine();

        indicator2D.duration = duration;
        indicator2D.SetPosition(start, end);

        return indicator2D;
    }

    public LineIndicator2D CreateLine(Vector2 start, Vector2 end, float duration, float width)
    {
        LineIndicator2D indicator2D = CreateLine();

        indicator2D.duration = duration;
        indicator2D.SetPosition(start, end);
        indicator2D.SetSize(width);

        return indicator2D;
    }

    public (LineIndicator2D line, ZoneIndicator2D startZone, ZoneIndicator2D endZone) CreateCompositeLine(Vector2 start, Vector2 end, float duration = 3, float width = 1, bool lineSimple = false)
    {
        LineIndicator2D line = CreateLine(start, end, duration, width);
        ZoneIndicator2D startZone = CreateZone(start, start, duration, width);
        ZoneIndicator2D endZone = CreateZone(end, end, duration, width);

        line.SetImage(lineMiddle);
        startZone.SetImage(lineEnd);
        endZone.SetImage(lineEnd);

        startZone.lookAt = endZone.transform;
        startZone.lockTo = line.startTransform;

        endZone.lookAt = startZone.transform;
        endZone.lockTo = line.endTransform;

        return (line, startZone, endZone);
    }

}
