using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothGameBehaviorClickHandler : MonoBehaviour
{
    public GameObject textsSkills;
    public GameObject physicalTraitsTexts;

    public GameObject clientTraitsTexts;

    // Update is called once per frame
    void Update()
    {
        //Deactivates the girl's info on a right click
        if (Input.GetMouseButtonUp(1))
        {
            textsSkills.SetActive(false);
            physicalTraitsTexts.SetActive(false);
            clientTraitsTexts.SetActive(false);
            BoothGameBehavior.activityDisplayedGirl = null;
        }
    }
}
