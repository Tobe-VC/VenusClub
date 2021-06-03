using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsBuiltScene : MonoBehaviour
{

    public GameObject contentList;

    public GameObject improvementPrefab;

    public GameObject popup;

    public Image mainPopupImage;

    //Creates one item in the list of improvements
    private void CreateOneItemInList(Improvement imp)
    {
        for (int i = 0; i < imp.currentLevel; i++)
        {
            GameObject instance = Instantiate(improvementPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(contentList.transform, false);
            instance.name = imp.subNames[i];
            instance.GetComponentInChildren<Image>().sprite =
                Resources.Load<Sprite>(StaticStrings.IMPROVEMENTS_FOLDER + imp.name + "/" + imp.imagesPaths[i]);


            Text[] texts = instance.GetComponentsInChildren<Text>();
            foreach (Text t in texts)
            {
                if (t.CompareTag("ImprovementName"))
                {
                    t.text = imp.subNames[i];
                }
            }

            int j = i;
            Button b = instance.GetComponentInChildren<Button>();
            b.onClick.AddListener(delegate () { OnButtonClick(imp.name, imp, j); });
        }
    }

    //Creates the list of improvements
    private void CreateList()
    {
            foreach (Improvement imp in GameMasterGlobalData.clubImprovementList)
            {
                CreateOneItemInList(imp);
            }
    }

    private void Awake()
    {
        CreateList();
    }

    //The OnClick action for each image in the improvements menu
    private void OnButtonClick(string improvementName, Improvement imp, int impLevel)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.IMPROVEMENTS_FOLDER + imp.name + "/" + imp.imagesPaths[impLevel]);
                mainPopupImage = img;
                break;
            }
        }
        popup.GetComponentInChildren<Text>().text = imp.detailedDescription[impLevel];

        Button b = popup.GetComponentInChildren<Button>(true);
        b.onClick.AddListener(delegate () { popup.SetActive(false); });
    }

}
