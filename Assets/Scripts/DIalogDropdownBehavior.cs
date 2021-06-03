using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogDropdownBehavior : MonoBehaviour
{
    private void Awake()
    {
        InteractionSceneBehavior.allDialogsDropdownList = gameObject;
        gameObject.SetActive(false);
    }
}
