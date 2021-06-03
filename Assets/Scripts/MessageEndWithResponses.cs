using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MessageEndWithResponses : MessageWithResponses
{
    public MessageEndWithResponses(string message, NPCCode npcCode, List<string> responses, UnityAction<int> action)
        : base(message,npcCode, responses, action) { }

    public override Message ResponseResult(int responseCode)
    {
        action(responseCode);
        return this;
    }

}
