using UnityEngine;

public class ZoneIndicator2D : Indicator2D
{
    public Transform lookAt = null;
    public Transform lockTo = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        this.transform.position = this.positionStart;
        this.zone.sizeDelta = new Vector2(this.sizeStart, this.sizeStart);
    }

    protected override void Update()
    {
        base.Update();

        if (this.lockTo != null)
        {
            this.SetPosition(this.lockTo.position);
        }

        float timeRemaining = this.lifetime / this.duration;
        float sizeScaled = Mathf.Lerp(this.sizeStart, this.sizeEnd, timeRemaining);
        Vector3 positionScaled = Vector3.Lerp(this.positionStart, this.positionEnd, timeRemaining);

        this.zone.sizeDelta = new Vector2(sizeScaled, sizeScaled);
        this.transform.position = positionScaled;

        if (this.lookAt != null)
        {
            this.transform.right = (this.transform.position - this.lookAt.position).normalized;
        }
    }

    protected override void ApplyColour(Color c)
    {
        if (zoneImage != null)
        {
            zoneImage.color = c;
        }
    }
}
