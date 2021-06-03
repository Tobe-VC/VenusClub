using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogMessageFirst : DialogMessageBasic
{
    public Func<bool> trigger;

    public DialogMessageFirst(string message, Func<bool> trigger) : base(message)
    {
        this.trigger = trigger;
    }

    public DialogMessageFirst(string message, bool disabled, Func<bool> trigger) : base(message, disabled)
    {
        this.trigger = trigger;
    }


    /// <summary>
    /// In the first message of a dialog, there could be a special trigger to display it
    /// If there is, it is passed when creating the message
    /// </summary>
    /// <returns></returns>
    public override bool Trigger()
    {
        return base.Trigger() && trigger();
    }
}
