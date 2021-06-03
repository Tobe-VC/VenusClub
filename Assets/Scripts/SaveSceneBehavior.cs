using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSceneBehavior : MonoBehaviour
{

    public GameObject contentList;

    public GameObject saveSlotPrefab;

    public GameObject saveConfimationPopup;

    public GameObject eraseConfimationPopup;

    private delegate void Del();

    //The number of the slot that has been clicked to save on
    private int slotNumberPress;

    public void Awake()
    {
        for(int i = 0; i < GMGlobalNumericVariables.gnv.MAX_SAVE_SLOTS; i++)
        {
            Del del = delegate () { OnSlotClick((i + 1).ToString()); }; 

            GameObject instance = Instantiate(saveSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(contentList.transform);

            Button b = instance.GetComponent<Button>();
            b.onClick.RemoveAllListeners();
            int k = i;//k is used because i is always its maximum when the delegate functions are created
            //This code is probably executed after the for loop has ended, so it is the same value for i in every listener
            //Said value being the maximum value of i in the loop (+1 because 1 is added)
            b.onClick.AddListener(delegate () { OnSlotClick( (k+1).ToString()); });
            Text t = b.GetComponentInChildren<Text>();

            //Get the eraseButton in the prefab
            Button[] buttons = b.GetComponentsInChildren<Button>(true);
            Button eraseButton = null;
            foreach (Button butt in buttons)
            {
                if (butt.CompareTag("EraseButton"))
                {
                    eraseButton = butt;
                }
            }

            if (File.Exists(Application.persistentDataPath + "/" + StaticStrings.SAVE_FILE + "_" + (i + 1).ToString()))
            {
                System.DateTime date = File.GetLastWriteTime(Application.persistentDataPath + "/" +
                    StaticStrings.SAVE_FILE + "_" + (i + 1));

                t.text = date.ToShortDateString() + "\n\n" + date.ToLongTimeString();

                eraseButton.onClick.AddListener(delegate () { OnEraseClick((k + 1).ToString()); });
                eraseButton.gameObject.SetActive(true);
            }
            else
            {
                t.text = "Slot " + (i + 1).ToString();
                eraseButton.gameObject.SetActive(false);
            }
        }
    }

    public void OnSlotClick(string slotNumber)
    {
        if (File.Exists(Application.persistentDataPath + "/" + StaticStrings.SAVE_FILE + "_" + slotNumber.ToString()))
        {
            slotNumberPress = int.Parse(slotNumber);
            saveConfimationPopup.SetActive(true);
        }
        else
        {
            StaticFunctions.Save(StaticStrings.SAVE_FILE + "_" + slotNumber);

            SceneManager.LoadScene(StaticStrings.SAVE_SCENE);
        }

    }

    public void OnYesButtonPress()
    {
        saveConfimationPopup.SetActive(false);
        StaticFunctions.Save(StaticStrings.SAVE_FILE + "_" + slotNumberPress);
        SceneManager.LoadScene(StaticStrings.SAVE_SCENE);
    }

    public void OnNoButtonPress()
    {
        saveConfimationPopup.SetActive(false);
    }

    public void OnEraseClick(string slotNumber)
    {
        if (File.Exists(Application.persistentDataPath + "/" + StaticStrings.SAVE_FILE + "_" + slotNumber))
        {
            eraseConfimationPopup.SetActive(true);
            slotNumberPress = int.Parse(slotNumber);
        }
    }

    public void OnEraseYesButtonPress()
    {
        File.Delete(Application.persistentDataPath + "/" + StaticStrings.SAVE_FILE + "_" + slotNumberPress.ToString());
        eraseConfimationPopup.SetActive(false);

        SceneManager.LoadScene(StaticStrings.SAVE_SCENE);
    }

    public void OnEraseNoButtonPress()
    {
        eraseConfimationPopup.SetActive(false);
    }

}
