using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderData
{
    //True if the bar has been built
    public bool bar = false;

    //True if the kitchen has been built
    public bool kitchen = false;

    //True if the pharmacy has been built
    public bool pharmacy = false;

    //True if the condom dispensers have been built
    public bool condomDispensers = false;

    //True if the dance stage has been built
    public bool danceStage = false;

    //True if the training zone has been built and the same for all of its additions
    public bool trainingZone = false;
    public bool trainingZoneDance = false;
    public bool trainingZonePose = false;
    public bool trainingZoneForeplay = false;
    public bool trainingZoneOral = false;
    public bool trainingZoneSex = false;
    public bool trainingZoneGroup = false;

    //True if the dressing has been built and the same for all of its additions
    public bool dressingRoom = false;
    public bool dressingRoomPillows = false;
    public bool dressingRoomLoungeChair = false;
    public bool dressingRoomCouch = false;
    public bool dressingRoomBed = false;
    public bool dressingRoomKingSizeBed = false;

    //True if the sign has been built
    public bool sign = false;
    //True if the on line ads has been bought
    public bool onLineAds = false;
    //True if the flyer ad campaign has been bought
    public bool flyerAdCampaign = false;
    //True if the live ad campaign has been bought
    public bool liveAdCampaign = false;

    //Each of the following is true if the corresponding costume type has been bought
    public bool sexyCostumes = false;
    public bool bunnyCostumes = false;
    public bool maidCostumes = false;
    public bool nurseCostumes = false;
}

public class GMBuilderData : MonoBehaviour
{

    public static BuilderData builderData = new BuilderData();

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GMBuilderData s_Instance = null;


    // A static property that finds or creates an instance of the manager object and returns it.
    public static GMBuilderData instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(GMBuilderData)) as GMBuilderData;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GMClubData");
                s_Instance = obj.AddComponent<GMBuilderData>();
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }
}
