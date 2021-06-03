using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMessageBasic : DialogMessage
{

    public DialogMessageBasic(string message)
    {
        this.message = message;
    }

    public DialogMessageBasic(string message, bool disabled)
    {
        this.message = message;
        this.disabled = disabled;
    }

    /// <summary>
    /// Enables the next message in the dialog
    /// Assumes that the parameter at index 0 is the next message
    /// </summary>
    /// <param name="parameters"></param>
    public override void NextEffect(List<Object> parameters)
    {
        ((DialogMessage)parameters[0]).disabled = false;
        this.disabled = true;
    }

    /// <summary>
    /// Does nothing
    /// </summary>
    /// <param name="parameters"></param>
    public override void ResponseEffect(List<Object> parameters)
    {

    }

    /// <summary>
    /// The trigger is true if the message is not disabled
    /// Meaning that the message should be displayed if it is enabled
    /// It is technically useless in this case
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override bool Trigger(List<Object> parameters)
    {
        return Trigger();
    }    
    
    /// <summary>
    /// The trigger is true if the message is not disabled
    /// Meaning that the message should be displayed if it is enabled
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override bool Trigger()
    {
        return !disabled;
    }
}
