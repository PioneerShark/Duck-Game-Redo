using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineIndicator2D : Indicator2D
{
    public Transform startTransform;
    public Transform endTransform;

    protected void Awake()
    {
        GameObject startObject = new GameObject("StartPoint");
        startTransform = startObject.transform;

        GameObject endObject = new GameObject("EndPoint");
        endTransform = endObject.transform;

        startTransform.SetParent(this.transform);
        endTransform.SetParent(this.transform);

        startTransform.position = positionStart;
        endTransform.position = positionEnd;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        if (startTransform != null)
            startTransform.position = positionStart;

        if (endTransform != null)
            endTransform.position = positionEnd;

        this.transform.position = Vector3.Lerp(this.positionStart, this.positionEnd, 0.5f);
        this.transform.right = (this.positionEnd - this.positionStart).normalized;

        float timeRemaining = this.lifetime / this.duration;
        float sizeScaled = Mathf.Lerp(this.sizeStart, this.sizeEnd, timeRemaining);

        this.zone.sizeDelta = new Vector2(Vector3.Distance(this.positionStart, this.positionEnd), sizeScaled);
        this.zoneImage.uvRect = new Rect(0, 0, (int) (this.zone.sizeDelta.x * (1 / this.zone.sizeDelta.y)) , 1);
    }

    protected override void ApplyColour(Color c)
    {
        if (zoneImage != null)
        {
            zoneImage.color = c;
        }
    }
}
