using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondtionDescriptionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
        InteractionSceneBehavior.conditionsDescription = gameObject;
        gameObject.SetActive(false);
    }

}
