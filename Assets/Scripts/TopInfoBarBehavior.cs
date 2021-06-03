using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopInfoBarBehavior : MonoBehaviour
{

    public Text moneyText;
    public Text reputationText;
    public Text influenceText;
    public Text connectionText;
    public Text dayText;

    public Text assistantPointsText;
    public Image assistantPointsIcon;

    private int waitFrame = 0;

    private string ReputationStringConversion()
    {
        return GMClubData.GetReputation() < GMGlobalNumericVariables.gnv.REPUTATION_MAX ? StaticStrings.REPUTATION[Mathf.FloorToInt(GMClubData.GetReputation() / 10)] :
            StaticStrings.REPUTATION[10];
    }

    private void FillTexts()
    {
        moneyText.text = GMClubData.money.ToString();
        reputationText.text = ReputationStringConversion() + " (" + (Mathf.Floor(GMClubData.GetReputation() * 10)) / 10 + ")";
        influenceText.text = (Mathf.Floor(GMClubData.GetInfluence()* 10 ) / 10).ToString();
        connectionText.text = (Mathf.Floor(GMClubData.GetConnection() * 10) / 10).ToString();
        dayText.text = GMClubData.day.ToString();
        assistantPointsText.text = StaticAssistantData.data.assistantPoints.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        FillTexts();
        bool showAssistantResource = StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_SECOND_LATE_NIGHT_MEETING_DIALOG_ID).seen;
        assistantPointsText.gameObject.SetActive(showAssistantResource);
        assistantPointsIcon.gameObject.SetActive(showAssistantResource);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitFrame == 0)
        {
            FillTexts();
            waitFrame = 10;
        }
        else
        {
            waitFrame--;
        }
    }
}
