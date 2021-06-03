using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assumes that it has only one successor
/// </summary>
public class MessagePermanent : Message
{

    public MessagePermanent(string message, NPCCode npcCode) : base(message, npcCode)
    {

    }

    public override Message SelectNextMessage()
    {
        throw new MessageNoNextException();
    }

    public override Message SelectNextMessage(Func<Message> action)
    {
        throw new MessageNoNextException();
    }
}
