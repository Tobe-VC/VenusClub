using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneBehavior : MonoBehaviour
{

    public GameObject contentList;

    public GameObject loadSlotPrefab;

    private delegate void Del();

    public GameObject loadConfimationPopup;

    public GameObject eraseConfimationPopup;

    public GameObject warningSavePopup;

    public Button loadAutosave1;
    public Button loadAutosave2;

    //The number of the slot that has been clicked to save on
    private int slotNumberPress;

    private void CreateAutoSaveButton(Button autosaveButton, string path, int index)
    {
        if (File.Exists(path))
        {
            autosaveButton.gameObject.SetActive(true);
            System.DateTime dateAutosave = File.GetLastWriteTime(path);

            autosaveButton.GetComponentInChildren<Text>().text = "Autosave " + index + "\n" + dateAutosave.ToShortDateString() + "\n" +
                dateAutosave.ToLongTimeString();
            autosaveButton.onClick.RemoveAllListeners();
            autosaveButton.onClick.AddListener(delegate { LoadAutosave(index); });
        }
        else
        {
            autosaveButton.gameObject.SetActive(false);
        }
    }

    public void Awake()
    {
        eraseConfimationPopup.SetActive(false);

        if (loadConfimationPopup != null)
            loadConfimationPopup.SetActive(false);

        CreateAutoSaveButton(loadAutosave1, Application.persistentDataPath + "/autosave1.sav", 1);
        CreateAutoSaveButton(loadAutosave2, Application.persistentDataPath + "/autosave2.sav", 2);

        for (int i = 0; i < GMGlobalNumericVariables.gnv.MAX_SAVE_SLOTS; i++)
        {

            GameObject instance = Instantiate(loadSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(contentList.transform);
            Button b = instance.GetComponent<Button>();
            b.onClick.RemoveAllListeners();
            int k = i;//k is used because i is always its maximum when the delegate functions are created
            //This code is probably executed after the for loop has ended, so it is the same value for i in every listener
            //Said value being the maximum value of i in the loop (+1 because 1 is added)
            b.onClick.AddListener(delegate () { OnSlotClick((k + 1).ToString()); });
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

                t.text = "Slot " + (i + 1) +"\n" + date.ToShortDateString() + "\n" + date.ToLongTimeString();

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
        if (File.Exists(Application.persistentDataPath + "/" + StaticStrings.SAVE_FILE + "_" + slotNumber))
        {
            //If the loadConfirmationPopup is not null, then we are in the normal LoadScene
            //So the confirmation popup should appear
            if (loadConfimationPopup != null)
            {
                slotNumberPress = int.Parse(slotNumber);
                loadConfimationPopup.SetActive(true);
            }
            else
            {
                //If the loadConfirmationPopup is null, then we are in the main menu loading scene
                //So the file should be loaded directly
                Load(StaticStrings.SAVE_FILE + "_" + slotNumber);
            }

        }
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

        //Again, if the loadConfirmationPopup is not null, then we are in the normal loading scene, 
        //if it is, we are in the main menu loading scene
        if (loadConfimationPopup != null)
            SceneManager.LoadScene(StaticStrings.LOAD_SCENE);
        else
            SceneManager.LoadScene(StaticStrings.LOAD_SCENE_FROM_MAIN_MENU);
    }

    public void OnEraseNoButtonPress()
    {
        eraseConfimationPopup.SetActive(false);
    }

    public void OnWarningCancelButtonPress()
    {
        warningSavePopup.SetActive(false);
    }

    public void OnWarningProceedButtonPress()
    {
        StaticFunctions.ReinitializeVariables();
        StaticFunctions.Load(StaticStrings.SAVE_FILE + "_" + slotNumberPress);
        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
    }

    private void Load(string fileName)
    {
        if (StaticFunctions.TestLoad(fileName))
        {
            StaticFunctions.ReinitializeVariables();
            StaticFunctions.Load(fileName);
            SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
        }
        else
        {
            loadConfimationPopup.SetActive(false);
            warningSavePopup.SetActive(true);
        }
    }

    public void OnYesButtonPress()
    {
        Load(StaticStrings.SAVE_FILE + "_" + slotNumberPress);
    }

    public void OnNoButtonPress()
    {
        loadConfimationPopup.SetActive(false);
    }

    private void LoadAutosave(int index)
    {
        Load("autosave" + index + ".sav");
    }
}
