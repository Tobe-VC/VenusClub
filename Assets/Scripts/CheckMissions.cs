using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMissions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Check());
    }

    private void TriggerMissions()
    {
        foreach(Mission m in RegisterMissions.missions)
        {
            //The order of the conditional is important (see the Trigger function) !!!
            if (!m.IsTriggered() && m.Trigger() /*&& RegisterMissions.activeMissions.Count == 0*/)
            {
                //For the moment, only one mission at a time is allowed
                //RegisterMissions.availableMissions.Add(m);
                m.Accept();
                RegisterMissions.activeMissions.Add(m);
                //RegisterMissions.availableMissions.RemoveAt(0);
            }
        }
    }

    private IEnumerator Check()
    {
        TriggerMissions();
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Check());
    }
}
