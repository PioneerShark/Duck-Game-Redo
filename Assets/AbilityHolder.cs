using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldown;
    float activeTime;

    enum AbilityState { 
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public void TriggerAbility()
    {
        switch (state)
        {
            case AbilityState.ready:
                ability.Activate(gameObject);
                state = AbilityState.active;
                activeTime = ability.activeTime;
                break;
            case AbilityState.active:
                break;
            case AbilityState.cooldown:
                break;
        }
    }
    void Update()
    {
        switch (state) 
        { 
            case AbilityState.active:
                if (activeTime > 0) 
                {
                    activeTime -= Time.deltaTime;
                }
                else 
                {
                    state = AbilityState.cooldown;
                    cooldown = ability.cooldown;
                }
                
                break;
            case AbilityState.cooldown:
                if (cooldown <= 0)
                {
                    state = AbilityState.ready;
                }
                else 
                {
                    cooldown -= Time.deltaTime;
                }
                break;
        }
    }
}
