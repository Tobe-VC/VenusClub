using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assumes that it has only one successor
/// </summary>
[Serializable]
public class MessageBasic : Message
{

    public MessageBasic(string message, NPCCode npcCode) : base(message, npcCode) { }

    public override Message SelectNextMessage()
    {
        return successors[0];
    }

    public override Message SelectNextMessage(Func<Message> action)
    {
        throw new NotImplementedException();
    }
}
