using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogMessage : Object
{
    //The message displayed in the text box
    public string message;

    //The list of possible responses. Null if there are no responses
    public List<string> responses;

    //True if the message should not be displayed
    //True by defaults and triggers should turn it to false
    public bool disabled = true;

    //Returns true if the message should be displayed
    public abstract bool Trigger(List<Object> parameters);    
    
    //Returns true if the message should be displayed
    public abstract bool Trigger();

    //The effect of the possible responses
    public abstract void ResponseEffect(List<Object> parameters);

    //The effect of clicking on the "Next" button
    public abstract void NextEffect(List<Object> parameters);

}
