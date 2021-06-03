using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoothGameClientPortraitBehavior : MonoBehaviour, IPointerDownHandler
{

    private Client client;

    public int boothIndex;

    public Image clientPortrait;

    public GameObject traitsTexts;

    public Text favSexActValueText;
    public Text happinessValueText;
    public Text favFinisherValueText;
    public Text groupValueText;

    public GameObject girlTraitsTexts;
    public GameObject girlTextsSkills;

    //Ordered by happiness: 0 is angry, 1 is unhappy, 2 is neutral, 3 is happy, 4 is elated
    public Sprite[] clientSprites;

    public void OnPointerDown(PointerEventData eventData)
    {
        //If the click event is a left click
        if (BoothGameData.booths[boothIndex].client != null && eventData.button == PointerEventData.InputButton.Left)
        {
            girlTraitsTexts.SetActive(false);
            girlTextsSkills.SetActive(false);

            client = BoothGameData.booths[boothIndex].client;

            traitsTexts.SetActive(true);

            favSexActValueText.text = StaticFunctions.ToLowerCaseExceptFirst(client.favoriteSexAct.ToString());
            happinessValueText.text = StaticFunctions.ToLowerCaseExceptFirst((Mathf.Round(client.GetHappiness())).ToString());
            favFinisherValueText.text = StaticFunctions.ToLowerCaseExceptFirst(client.favoriteFinisher.ToString());
            groupValueText.text = StaticFunctions.ToLowerCaseExceptFirst(client.clientGroup.ToString());

            BoothGameBehavior.activityDisplayedGirl = null;
        }
    }

    private void Update()
    {
        if (BoothGameData.booths[boothIndex].client != null)
        {
            clientPortrait.sprite = ClientSprite(BoothGameData.booths[boothIndex].client.GetHappiness());
        }
    }

    //Returns the sprite corresponding to the current happiness of the client
    private Sprite ClientSprite(float happiness)
    {
        if (happiness < 20)
        {
            return clientSprites[0];
        }
        else if (happiness < 40)
        {
            return clientSprites[1];
        }
        else if (happiness < 60)
        {
            return clientSprites[2];
        }
        else if (happiness < 80)
        {
            return clientSprites[3];
        }
        else
        {
            return clientSprites[4];
        }
    }

}
