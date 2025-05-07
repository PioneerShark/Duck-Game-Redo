using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SegmentedMeter : VisualElement
{
    public enum SegmentBehaviour {None, Fade, Slice};
    /* 
        None | Only show full segments
        Fade | Non full segments should have their opacity altered
        Slice | Non full segments should be sliced
    */

    // Properties
    private float _valueMin = 0;
    [UxmlAttribute] public float valueMin
    { 
        get => _valueMin;
        set => SetProperty(ref _valueMin, value, Redraw);
    }

    private float _valueMax = 100;
    [UxmlAttribute] public float valueMax
    { 
        get => _valueMax;
        set => SetProperty(ref _valueMax, value, Redraw);
    }

    private float _valueCurrent = 50;
    [UxmlAttribute] public float valueCurrent
    { 
        get => _valueCurrent; 
        set => SetProperty(ref _valueCurrent, value, Redraw);
    }

    private int _segmentAmount = 10;
    [UxmlAttribute] public int segmentAmount
    { 
        get => _segmentAmount;
        set => SetProperty(ref _segmentAmount, value, () => {TryGenerateSegments();}, Redraw);
    }

    private SegmentBehaviour _segmentBehaviour = SegmentBehaviour.None;
    [UxmlAttribute] public SegmentBehaviour segmentBehaviour
    { 
        get => _segmentBehaviour;
        set => SetProperty(ref _segmentBehaviour, value, Redraw);
    }

    private Color _segmentFullColor = Color.white;
    [UxmlAttribute] public Color segmentFullColor
    { 
        get => _segmentFullColor;
        set => SetProperty(ref _segmentFullColor, value, Redraw);
    }

    private Color _segmentEmptyColor = Color.clear;
    [UxmlAttribute] public Color segmentEmptyColor
    { 
        get => _segmentEmptyColor;
        set => SetProperty(ref _segmentEmptyColor, value, Redraw);
    }

    private int _segmentRadius = 4;
    [UxmlAttribute] public int segmentRadius
    { 
        get => _segmentRadius;
        set => SetProperty(ref _segmentRadius, value, Redraw);
    }

    private int _segmentBorder = 4;
    [UxmlAttribute] public int segmentBorder
    { 
        get => _segmentBorder;
        set => SetProperty(ref _segmentBorder, value, Redraw);
    }

    private int _segmentGap = 2;
    [UxmlAttribute] public int segmentGap
    { 
        get => _segmentGap;
        set => SetProperty(ref _segmentGap, value, Redraw);
    }

    private float _segmentRotation = 0;
    [UxmlAttribute] public float segmentRotation
    { 
        get => _segmentRotation;
        set => SetProperty(ref _segmentRotation, value, Redraw);
    }

    private VisualElement[] segments;
    private VisualElement[] segmentFills;

    public SegmentedMeter()
    {
        AddToClassList("segmented-meter");

        var styleSheet = Resources.Load<StyleSheet>("UIService/SegmentedMeter");
        if (styleSheet != null)
            styleSheets.Add(styleSheet);

        TryGenerateSegments();
        Redraw();
    }

    public bool TryGenerateSegments()
    {
        // Remove child segments
        Clear();

        // Generate new segments
        segments = new VisualElement[segmentAmount];
        segmentFills = new VisualElement[segmentAmount];

        for (int i = 0; i < segmentAmount; i++)
        {
            VisualElement segment = new VisualElement();
            segment.AddToClassList("meter-segment");

            VisualElement segmentFill = new VisualElement();
            segmentFill.AddToClassList("meter-segment-fill");

            segment.Add(segmentFill);
            Add(segment);
            segments[i] = segment;
            segmentFills[i] = segmentFill;
        }

        return true;
    }

    public void Redraw()
    {
        float valueClamped = Mathf.Clamp(valueCurrent, valueMin, valueMax);
        float range = valueMax - valueMin;
        float valueAsPercentage = (valueClamped - valueMin) / range;
        float segmentsToFill = valueAsPercentage * segmentAmount;

        for (int i = 0; i < segmentAmount; i++)
        {
            VisualElement segment = segments[i];
            VisualElement segmentFill = segmentFills[i];

            switch (segmentBehaviour)
            {
                case SegmentBehaviour.Fade:
                    if (i < (int) segmentsToFill)
                    {
                        segment.style.backgroundColor = segmentFullColor;
                        segmentFill.style.backgroundColor = segmentFullColor;
                    }
                    else if (i == (int) segmentsToFill)
                    {
                        float lerpValue = segmentsToFill - ((int) segmentsToFill);
                        Color lerpColor = Color.Lerp(segmentEmptyColor, segmentFullColor, lerpValue);
                        segmentFill.style.backgroundColor = lerpColor;
                        segment.style.backgroundColor = lerpColor;
                    }
                    else
                    {
                        segmentFill.style.backgroundColor = segmentEmptyColor;
                        segment.style.backgroundColor = segmentEmptyColor;
                    }
                    break;
                case SegmentBehaviour.Slice:
                    if (i < (int) segmentsToFill)
                    {
                        segment.style.backgroundColor = segmentFullColor;
                        segmentFill.style.backgroundColor = segmentFullColor;
                    }
                    else if (i == (int) segmentsToFill)
                    {
                        float lerpValue = segmentsToFill - ((int) segmentsToFill);
                        segmentFill.style.width = new StyleLength(new Length(lerpValue * 100, LengthUnit.Percent));
                        segmentFill.style.backgroundColor = segmentFullColor;
                        segment.style.backgroundColor = segmentEmptyColor;
                    }
                    else
                    {
                        segmentFill.style.backgroundColor = segmentEmptyColor;
                        segment.style.backgroundColor = segmentEmptyColor;
                    }
                    break;
                case SegmentBehaviour.None:
                default:
                    segmentFill.style.backgroundColor = 
                        i < (int) segmentsToFill ?
                        segmentFullColor : segmentEmptyColor;
                    break;
            }

            segment.style.borderTopLeftRadius = segmentRadius;
            segment.style.borderTopRightRadius = segmentRadius;
            segment.style.borderBottomLeftRadius = segmentRadius;
            segment.style.borderBottomRightRadius = segmentRadius;

            segmentFill.style.borderTopLeftRadius = segmentRadius - segmentBorder;
            segmentFill.style.borderTopRightRadius = segmentRadius - segmentBorder;
            segmentFill.style.borderBottomLeftRadius = segmentRadius - segmentBorder;
            segmentFill.style.borderBottomRightRadius = segmentRadius - segmentBorder;

            segment.style.borderTopWidth = segmentBorder;
            segment.style.borderBottomWidth = segmentBorder;
            segment.style.borderLeftWidth = segmentBorder;
            segment.style.borderRightWidth = segmentBorder;

            segment.style.marginLeft = segmentGap;
            segment.style.marginRight = segmentGap;

            SetElementRotation(segment, segmentRotation);
        }
    }

    private T SetProperty<T>(ref T field, T value, params Action[] onChanged)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;

            foreach (Action action in onChanged)
            {
                action?.Invoke();
            }
        }
        return field;
    }

    void SetElementRotation(VisualElement element, float degrees)
    {
        element.style.rotate = new StyleRotate(new Rotate(new Angle(degrees, AngleUnit.Degree)));
    }
}
