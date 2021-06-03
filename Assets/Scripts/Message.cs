using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class Message 
{
    /// <summary>
    /// The code of the NPC currently saying this message
    /// </summary>
    public NPCCode npcCode;

    /// <summary>
    /// The content of the message
    /// </summary>
    public string message;

    /// <summary>
    /// The list of all messages accessible from this message
    /// </summary>
    public List<Message> successors;

    public Message(string message, NPCCode npcCode)
    {
        this.message = message;
        this.successors = new List<Message>();
        this.npcCode = npcCode;
    }

    public void AddSuccessor(Message message)
    {
        successors.Add(message);
    }

    /// <summary>
    /// The function displaying the next message
    /// </summary>
    /// <param name="action"></param>
    public abstract Message SelectNextMessage();

    /// <summary>
    /// The function displaying the next message
    /// </summary>
    /// <param name="action"></param>
    public abstract Message SelectNextMessage(Func<Message> action);

}
