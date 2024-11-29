using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float damage;
    public float cooldown;
    public float activeTime;
    public enum InputType
    {
        repeat,
        single,
        extend
    };
    public InputType inputType;
    
    public virtual void Activate(GameObject parent)
    {

    }
    public virtual void Deactivate(GameObject parent) 
    { 
    
    }
}
