using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Mission
{

    public readonly int id;

    public bool isTriggered;

    public bool isDone;

    public bool isActive;

    /// <summary>
    /// Used to see if this mission is currently being displayed on the office screen
    /// </summary>
    public bool isDisplayed = false;

    public readonly string title;

    public readonly string description;

    /// <summary>
    /// The event that makes the mission available to the player
    /// </summary>
    public System.Func<bool> trigger;

    /// <summary>
    /// Checks if the mission can be accomplished by the player
    /// </summary>
    public System.Func<bool> checkDoable;

    /// <summary>
    /// The effect when the mission is accomplished
    /// </summary>
    public UnityAction effect;

    /// <summary>
    /// The effect when the mission is triggered
    /// </summary>
    public UnityAction effectWhenTriggered;

    

    public Mission(int id, string title, string description, System.Func<bool> trigger, UnityAction effectWhenTriggered, System.Func<bool> checkDone, 
        UnityAction effect)
    {
        this.id = id;
        this.isDone = false;
        this.isActive = false;
        this.isTriggered = false;
        this.isDisplayed = false;
        this.title = title;
        this.description = description;
        this.trigger = trigger;
        this.checkDoable = checkDone;
        this.effect = effect;
        this.effectWhenTriggered = effectWhenTriggered;
    }

    public Mission(int id, bool isTriggered, bool isDone, bool isActive, string title, string description, System.Func<bool> trigger,
         UnityAction effectWhenTriggered, System.Func<bool> checkDone, UnityAction effect)
    {
        this.id = id;
        this.isTriggered = isTriggered;
        this.isDone = isDone;
        this.isActive = isActive;
        this.isDisplayed = false;
        this.title = title;
        this.description = description;
        this.trigger = trigger;
        this.checkDoable = checkDone;
        this.effect = effect;
        this.effectWhenTriggered = effectWhenTriggered;
    }

    public bool IsTriggered()
    {
        return isTriggered;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsDone()
    {
        return isDone;
    }

    public bool Trigger()
    {
        if (trigger())
        {
            effectWhenTriggered();
            isTriggered = true;
            return true;
        }
        return false;
    }

    public void Accept()
    {
        isActive = true;
    }

    public bool CheckDoable()
    {
        return checkDoable();
    }

    public void Effect()
    {
        isTriggered = true;
        isDone = true;
        isActive = false;
        effect();
    }

}
