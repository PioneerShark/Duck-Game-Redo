using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent2D : Character2D
{
    public GameObject target;

    [Header("Entity UI")]
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private GameObject canvas;
    private void Awake()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        Vector3 canvasScale = canvas.transform.localScale;
        canvas.gameObject.transform.localScale = new Vector3(transform.localScale.x, canvasScale.y, canvasScale.z);

    }
    public override void ModifyHealth(float value)
    {
            
        base.ModifyHealth(value);
        float barValue = health / healthMax;
        Debug.Log(barValue);
        healthBar.value = barValue;
    }
    public void OnDisable()
    {
        for (int i = 0; i < abilityHolder.abilities.Count; i++)
        {
            abilityHolder.StopAbility(i);
        }
    }

}
