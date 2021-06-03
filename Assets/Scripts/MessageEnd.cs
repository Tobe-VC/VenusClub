using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assumes that it has no successor
/// </summary>
[Serializable]
public class MessageEnd : Message
{

    public MessageEnd(string message, NPCCode npcCode) : base(message, npcCode) { }

    public override Message SelectNextMessage()
    {
        throw new MessageNoNextException();
    }

    public override Message SelectNextMessage(Func<Message> action)
    {
        throw new MessageNoNextException();
    }
}
