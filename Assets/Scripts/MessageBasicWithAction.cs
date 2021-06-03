using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Assumes that it has only one successor
/// </summary>
[Serializable]
public class MessageBasicWithAction : MessageBasic
{

    private UnityAction action;

    public MessageBasicWithAction(string message, NPCCode npcCode, UnityAction action) : base(message, npcCode)
    {
        this.action = action;
    }

    public override Message SelectNextMessage()
    {
        action();
        return successors[0];
    }

    public override Message SelectNextMessage(Func<Message> action)
    {
        throw new NotImplementedException();
    }
}
