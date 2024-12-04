using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public List<Ability> abilities;
    private List<float> cooldowns;
    private List<float> activeTimes;
    private List<AbilityState> states;
    private List<bool> currentPerformer;
    private bool performing;
    int abilityCount;
    enum AbilityState { 
        ready,
        active,
        cooldown
    };
    public void Awake()
    {
        RefreshAbilities();
    }

    public void TriggerAbility(int current)
    {
        switch (abilities[current].inputType)
        {
            case Ability.InputType.repeat:
            case Ability.InputType.extend:
                switch (states[current])
                {
                    case AbilityState.ready:
                        if (performing) return;
                        currentPerformer[current] = true;
                        break;
                    case AbilityState.active:
                        break;
                    case AbilityState.cooldown:
                        break;
                }
                break;
            case Ability.InputType.single:
                break;
        }
        switch (states[current])
        {
            case AbilityState.ready:
                if (performing) return;
                abilities[current].Activate(gameObject);
                states[current] = AbilityState.active;
                activeTimes[current] = abilities[current].activeTime;
                performing = true;
                break;
            case AbilityState.active:
                break;
            case AbilityState.cooldown:
                break;
        }
    }
    public void CancelAbility(int current)
    {
        switch (abilities[current].inputType)
        {
            case Ability.InputType.repeat:
            case Ability.InputType.extend:
                currentPerformer[current] = false;
                break;
        }
    }
    public bool AbilityReady(int current)
    {
        if (states[current] == AbilityState.ready)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Update()
    {
        for (int i = 0; i < abilityCount; i++) 
        {
            AbilityUpdates(i);
        }
    }
    // ----------------------------------
    
    void AbilityUpdates(int i)
    {
        switch (abilities[i].inputType)
        {
            case Ability.InputType.single:
                SingleInputUpdate(i);
                break;
            case Ability.InputType.repeat:
                RepeatInputUpdate(i);
                break;
        }
    }



    private void SingleInputUpdate(int i)
    {
        switch (states[i])
        {
            case AbilityState.active:
                if (activeTimes[i] > 0)
                {
                    activeTimes[i] -= Time.deltaTime;
                }
                else
                {
                    states[i] = AbilityState.cooldown;
                    cooldowns[i] = abilities[i].cooldown;
                    performing = false;
                }

                break;
            case AbilityState.cooldown:
                if (cooldowns[i] <= 0)
                {
                    states[i] = AbilityState.ready;
                }
                else
                {
                    cooldowns[i] -= Time.deltaTime;
                }
                break;
        }
    }

    private void RepeatInputUpdate(int i)
    {
        switch (states[i])
        {
            case AbilityState.ready:
                if (!currentPerformer[i]) return;
                abilities[i].Activate(gameObject);
                states[i] = AbilityState.active;
                activeTimes[i] = abilities[i].activeTime;
                break;
            case AbilityState.active:
                if (activeTimes[i] > 0)
                {
                    activeTimes[i] -= Time.deltaTime;
                }
                else
                {
                    states[i] = AbilityState.cooldown;
                    cooldowns[i] = abilities[i].cooldown;
                    performing = false;
                }

                break;
            case AbilityState.cooldown:
                if (cooldowns[i] <= 0)
                {
                    states[i] = AbilityState.ready;
                }
                else
                {
                    cooldowns[i] -= Time.deltaTime;
                }
                break;
        }
    }
    public void RefreshAbilities()
    {
        states = new List<AbilityState> { };
        cooldowns = new List<float> { };
        activeTimes = new List<float> { };
        currentPerformer = new List<bool> { };
        performing = false;
        abilityCount = abilities.Count;
        for (int i = 0; i < abilityCount; i++)
        {
            cooldowns.Add(abilities[i].cooldown);
            activeTimes.Add(0);
            states.Add(AbilityState.ready);
            currentPerformer.Add(false);
        }

    }
}
