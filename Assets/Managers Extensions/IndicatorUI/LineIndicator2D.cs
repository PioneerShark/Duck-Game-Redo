using UnityEngine;
using UnityEngine.UI;

public class LineIndicator2D : Indicator2D
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

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
