using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator2D : MonoBehaviour
{
    [Header("Lifespan")]
    public float duration = 3;
    protected float lifetime = 0f;

    [Header("Colour Settings")]
    public bool enableFlashing = false;
    public bool enableLerping = false;
    public List<Color> flashColours = new List<Color> {Color.white, Color.red};
    public float colourChangeDelay = 0.25f;
    public float colourChangeTime = 0.5f;

    protected int currentColourIndex = 0;
    protected float colourTime = 0f;
    protected Color currentColour, nextColour;

    [Range(0f, 1f)]
    public float transparency = 0f;

    [Header("Other")]
    public RectTransform zone;
    public RawImage zoneImage;
    public float sizeStart = 4;
    public float sizeEnd = 4;
    public Vector3 positionStart;
    public Vector3 positionEnd;

    protected virtual void OnEnable()
    {
        this.lifetime = 0f;
        this.colourTime = 0f;
        this.currentColourIndex = 0;
        if (flashColours.Count > 0)
        {
            currentColour = flashColours[0];
            currentColour.a = 1f - this.transparency;
            nextColour = this.GetNextColour();
        }
    }

    protected virtual void Update()
    {
        this.lifetime += Time.deltaTime;
        if (this.lifetime >= this.duration && this.duration >= 0)
        {
            Destroy(this.gameObject);
        }

        if (enableFlashing && flashColours.Count > 0)
        {
            this.AnimateColour();
        }
    }

    protected virtual void AnimateColour()
    {
        if (this.enableLerping)
        {
            // Smoothed
            this.colourTime += Time.deltaTime;
            Color lerpedColour = Color.Lerp(this.currentColour, this.nextColour, this.colourTime / this.colourChangeTime);

            if (this.colourTime >= this.colourChangeTime)
            {
                this.colourTime = 0f;
                this.currentColour = this.nextColour;
                this.nextColour = GetNextColour();
            }


            this.ApplyColour(lerpedColour);
        }
        else
        {
            // Instant
            this.colourTime += Time.deltaTime;
            if (this.colourTime >= this.colourChangeDelay)
            {
                this.colourTime = 0f;
                this.currentColour = GetNextColour();
                this.ApplyColour(this.currentColour);
            }
        }
    }

    protected Color GetNextColour()
    {
        currentColourIndex++;
        if (currentColourIndex >= flashColours.Count)
        {
            currentColourIndex = 0;
        }

        Color modifiedColour = flashColours[currentColourIndex];
        modifiedColour.a = 1f - this.transparency;

        return modifiedColour;
    }

    protected virtual void ApplyColour(Color c)
    {

    }

    // Setters
    public virtual void SetSize(float sizeStart, float sizeEnd)
    {
        this.sizeStart = sizeStart;
        this.sizeEnd = sizeEnd;
    }

    public virtual void SetSize(float size)
    {
        this.sizeStart = size;
        this.sizeEnd = size;
    }

    public virtual void SetPosition(Vector3 positionStart, Vector3 positionEnd)
    {
        this.positionStart = positionStart;
        this.positionEnd = positionEnd;
    }

    public virtual void SetPosition(Vector3 position)
    {
        this.positionStart = position;
        this.positionEnd = position;
    }
}

