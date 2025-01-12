using UnityEngine;

public interface IIndicatorService : IFrameworkService
{
    AreaIndicator2D CreateAreaIndicator();
    AreaIndicator2D CreateAreaIndicator(Vector2 start, Vector2 end, float duration);
    AreaIndicator2D CreateAreaIndicator(Vector2 start, Vector2 end, float duration, float size);
    
    LineIndicator2D CreateLineIndicator();
    LineIndicator2D CreateLineIndicator(Vector2 start, Vector2 end, float duration);
    LineIndicator2D CreateLineIndicator(Vector2 start, Vector2 end, float duration, float width);
}
