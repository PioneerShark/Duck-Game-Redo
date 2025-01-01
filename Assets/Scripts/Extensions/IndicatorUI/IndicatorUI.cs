using UnityEditor;
using UnityEngine;

public class IndicatorUI : MonoBehaviour
{
    // CREATE TRACKING INDICATOR
    [SerializeField] private AreaIndicator2D AreaIndicatorPrefab;
    [SerializeField] private LineIndicator2D LineIndicatorPrefab;

    public AreaIndicator2D CreateAreaIndicator()
    {
        AreaIndicator2D indicator2D = Instantiate(AreaIndicatorPrefab, Vector3.zero, Quaternion.identity);
        return indicator2D;
    }

    public AreaIndicator2D CreateAreaIndicator(Vector2 start, Vector2 end, float duration)
    {
        AreaIndicator2D indicator2D = CreateAreaIndicator();

        indicator2D.duration = duration;
        indicator2D.positionStart = start;
        indicator2D.positionEnd = end;

        return indicator2D;
    }
}
