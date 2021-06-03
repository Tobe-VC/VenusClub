using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WardrobeEquipSceneBehavior : MonoBehaviour
{
    //The list of directories where the different costumes are
    //public List<string> directories;

    public GameObject contentList;

    public GameObject costumePrefab;

    public GameObject popup;

    private Image mainPopupImage;

    private int currentImageNumber;
    private Costume currentCostume;

    // Start is called before the first frame update
    void Start()
    {
        CreateList();
    }

    private void CreateList()
    {
        foreach (Costume c in GMWardrobeData.ownedCostumes)
        {
            CreateOneItemInList(c);
        }
    }

    private void CreateOneItemInList(Costume costume)
    {
        GameObject instance = Instantiate(costumePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(contentList.transform, false);
        instance.name = costume.name;

        //Look at all the images in the prefab
        foreach (Image img in instance.GetComponentsInChildren<Image>(true))
        {
            //The untagged one is the main image
            if (img.CompareTag("Untagged"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER +
                    costume.name + "/" + costume.imagesPaths[costume.currentLevel]);
            }

            if (img.CompareTag("BuyableImage"))
            {
                if (GMWardrobeData.currentlyUsedCostume.name != null && costume.name == GMWardrobeData.currentlyUsedCostume.name)
                {
                    img.gameObject.SetActive(true);
                }
                else
                {
                    img.gameObject.SetActive(false);
                }
            }

                Button b = instance.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { OnImageClick(costume.name, costume); });

            Text[] texts = instance.GetComponentsInChildren<Text>();
            foreach (Text t in texts)
            {
                if (t.CompareTag("ImprovementDescription"))
                {
                    t.text = costume.description;
                }
                if (t.CompareTag("ImprovementName"))
                {
                    t.text = costume.subNames[costume.currentLevel];

                    if (GMWardrobeData.currentlyUsedCostume.name != null && costume.name == GMWardrobeData.currentlyUsedCostume.name)
                    {
                        t.text += " (Equiped)";

                    }

                }
            }
        }
    }

    private void OnImageClick(string costumeName, Costume costume)
    {
        currentImageNumber = costume.currentLevel;
        currentCostume = costume;
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER + costume.name + "/" + costume.imagesPaths[costume.currentLevel]);
                mainPopupImage = img;
                break;
            }
        }
        Text[] texts = popup.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.CompareTag("WardrobeCurrentLevelText"))
            {
               t.text = costume.subDetailedDescriptions[costume.currentLevel];
            }
        }

        //Create the wear button in the popup
        foreach (Button b in popup.GetComponentsInChildren<Button>(true))
        {
            if (b.CompareTag("PopupConfirm"))
            {
                b.gameObject.SetActive(true);
                b.GetComponentInChildren<Text>().text = StaticStrings.COSTUME_WEAR_CONFIRM_BUTTON_TEXT;
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { OnAlreadyOwnedClick(costumeName, costume); });

            }

        }
    }

    private void SubButtonsCommonBehavior()
    {
        popup.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
        SceneManager.LoadScene(StaticStrings.WARDROBE_EQUIP_SCENE);
    }

    //The behavior when clicking on the confirmation of an already owned costume
    private void OnAlreadyOwnedClick(string costumeName, Costume costume)
    {
        GMWardrobeData.currentlyUsedCostume = costume;
        SubButtonsCommonBehavior();
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        SubButtonsCommonBehavior();
    }

    public void NextCostumeImageButtonPress()
    {
        if(currentImageNumber + 1 <= currentCostume.currentLevel)
        {
            mainPopupImage.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER + currentCostume.name + "/" + currentCostume.imagesPaths[currentImageNumber + 1]);
            currentImageNumber++;
        }
    }

    public void PreviousCostumeImageButtonPress()
    {
        if (currentImageNumber - 1 > -1)
        {
            mainPopupImage.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER + currentCostume.name + "/" + currentCostume.imagesPaths[currentImageNumber - 1]);
            currentImageNumber--;
        }
    }


}
