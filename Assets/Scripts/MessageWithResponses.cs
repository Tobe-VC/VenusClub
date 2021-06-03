using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Assumes that it has many successors that are accessed according to the action function
/// </summary>
[Serializable]
public class MessageWithResponses : Message
{
    /// <summary>
    /// The list of indexes to go to when giving a response
    /// </summary>
    public List<int> responsesIndexesToGo;

    /// <summary>
    /// The different possible responses
    /// </summary>
    public List<string> responses;

    /// <summary>
    /// The action to perform depending on the response, passed as an index
    /// </summary>
    public UnityAction<int> action;

    public MessageWithResponses(string message, NPCCode npcCode, List<int> responsesIndexesToGo, List<string> responses, UnityAction<int> action) : base(message, npcCode)
    {
        if (responses.Count != responsesIndexesToGo.Count)
            throw new MessageWithResponseListsNotSameSizeException();
        this.responsesIndexesToGo = responsesIndexesToGo;
        this.responses = responses;
        this.action = action;
    }

    public MessageWithResponses(string message, NPCCode npcCode, List<string> responses, UnityAction<int> action) : base(message, npcCode)
    {
        responsesIndexesToGo = new List<int>();
        for (int i = 0;i < responses.Count; i++)
        {
            responsesIndexesToGo.Add(0);
        }
        this.responses = responses;
        this.action = action;
    }

    public override Message SelectNextMessage()
    {
        throw new MessageNoNextException();
    }

    public override Message SelectNextMessage(Func<Message> action)
    {
        throw new MessageNoNextException();
    }

    /// <summary>
    /// Returns the message got after a response
    /// </summary>
    /// <param name="responseCode">The index of the int stored in responsesIndexesToGo where the answer will lead</param>
    /// <returns></returns>
    public virtual Message ResponseResult(int responseCode)
    {
        action(responseCode);
        return successors[responsesIndexesToGo[responseCode]];
    }
   
}
