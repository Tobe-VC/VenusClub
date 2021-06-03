using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmergencyExit : MonoBehaviour
{

    public GameObject endGamePopupPrefab;

    public void OpenMenu()
    {
        if (!StaticBooleans.exitMenuOn)
        {
            GameObject canvas = null;
            GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<Canvas>() != null)
                {
                    canvas = go;
                    break;
                }
            }

            if (canvas != null)
            {

                GameObject popup = Instantiate(endGamePopupPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                popup.transform.SetParent(canvas.transform, false);
            }
        }
        StaticBooleans.exitMenuOn = true;
        if (SceneManager.GetActiveScene().name == StaticStrings.BOOTHS_MANAGEMENT_SCENE)
        {
            BoothGameData.boothGameIsPaused = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey("m")){
            GMClubData.money += 1000;
        }
        
        if (Input.GetKeyDown("r"))
        {
            GMClubData.AddToReputation(20);
        }

        if (Input.GetKeyDown("c"))
        {
            GMClubData.AddToConnection(20);
        }

        if (Input.GetKeyDown("i"))
        {
            GMClubData.AddToInfluence(20);
        }

        if (Input.GetKeyDown("a"))
        {
            StaticAssistantData.data.assistantPoints += 20;
        }

        if (Input.GetKeyDown("t"))
        {
            RegisterDates.currentDate.videoLevel++;
        }

        if (Input.GetKeyDown("d"))
        {
            if(RegisterDates.currentDate.currentPortraitLevel < RegisterDates.currentDate.maxPortraitLevel - 1)
                RegisterDates.currentDate.currentPortraitLevel++;

        }

        if (Input.GetKeyDown("v"))
        {
            Debug.Log(GameMasterGlobalData.girlsList[1].name);
            GameMasterGlobalData.girlsList[1].AddToOpenness(100);
            Debug.Log(GMRecruitmentData.recruitedGirlsList[0].name);
            GMRecruitmentData.recruitedGirlsList[0].AddToOpenness(100);

        }
        */

        if (Input.GetButtonDown("ExitMenu"))
        {
            OpenMenu();
        }
        /*
        if (Input.GetButtonDown("Money"))
        {
            GMClubData.money += 1000;
        }
        if (Input.GetButtonDown("Reputation"))
        {
            GMClubData.AddToReputation(20);
        }
        if (Input.GetButtonDown("Connection"))
        {
            GMClubData.connection += 20;
        }
        if (Input.GetButtonDown("Influence"))
        {
            GMClubData.influence += 20;
        }
        if (Input.GetButtonDown("AllResources"))
        {
            GMClubData.money += 100000;
            GMClubData.AddToReputation(20);
            GMClubData.connection += 20;
            GMClubData.influence += 20;
        }
        */

    }

}
