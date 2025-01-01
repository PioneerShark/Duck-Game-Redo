using UnityEngine;

public class AreaIndicator2D : Indicator2D
{
    protected override void OnEnable()
    {
        base.OnEnable();

        this.transform.position = this.positionStart;
        this.zone.sizeDelta = new Vector2(this.sizeStart, this.sizeStart);
    }

    protected override void Update()
    {
        base.Update();

        float timeRemaining = this.lifetime / this.duration;
        float sizeScaled = Mathf.Lerp(this.sizeStart, this.sizeEnd, timeRemaining);
        Vector3 positionScaled = Vector3.Lerp(this.positionStart, this.positionEnd, timeRemaining);

        this.zone.sizeDelta = new Vector2(sizeScaled, sizeScaled);
        this.transform.position = positionScaled;
    }

    protected override void ApplyColour(Color c)
    {
        if (zoneImage != null)
        {
            zoneImage.color = c;
        }
    }
}
