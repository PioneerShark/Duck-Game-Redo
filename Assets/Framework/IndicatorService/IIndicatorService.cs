using UnityEngine;

public interface IIndicatorService : IFrameworkService
{
    ZoneIndicator2D CreateZone();
    ZoneIndicator2D CreateZone(Vector2 start, Vector2 end, float duration);
    ZoneIndicator2D CreateZone(Vector2 start, Vector2 end, float duration, float size);
    
    LineIndicator2D CreateLine();
    LineIndicator2D CreateLine(Vector2 start, Vector2 end, float duration);
    LineIndicator2D CreateLine(Vector2 start, Vector2 end, float duration, float width);

    (LineIndicator2D line, ZoneIndicator2D startZone, ZoneIndicator2D endZone) CreateCompositeLine(Vector2 start, Vector2 end, float duration, float width);
}
