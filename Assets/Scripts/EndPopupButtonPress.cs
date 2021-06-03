using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPopupButtonPress : MonoBehaviour
{

    public void OnToEndPopupButtonPress()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

}
