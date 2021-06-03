using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameNavigation : MonoBehaviour
{

    public GameObject quitGameConfirmationPopupPrefab;

    private GameObject quitGameConfirmationPopup;

    public GameObject errorPopup;

    public GameObject settingPrefab;

    public void OnExitGameButtonPress()
    {
        //CommonButtonBehavior();

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

            quitGameConfirmationPopup = Instantiate(quitGameConfirmationPopupPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            quitGameConfirmationPopup.transform.SetParent(canvas.transform, false);
        }
    }

    public void OnYesQuitGameButtonPress()
    {
        Application.Quit();
    }

    public void OnNoQuitGameButtonPress()
    {
        Destroy(this.gameObject);
    }

    public void OnToMainMenuButtonPress()
    {
        CommonButtonBehavior();

        /*
        //Resets the values of the fired girls that have been changed in the "main" girls list
        foreach (GirlClass girl in GMRecruitmentData.firedGirlsList)
        {
            GirlClass g = null;
            if (!girl.external)
            {
                g = GirlClass.CreateFromJSON((Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                    StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE_NO_EXTENSION)).text);
            }
            else
            {
                StreamReader reader = new StreamReader(StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                    StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE);
                g = GirlClass.CreateFromJSON(reader.ReadToEnd());
                reader.Close();
            }
            GameMasterGlobalData.girlsList.Find(x => x.name.Equals(girl.name)).CopyFiredGirl(g);
        }*/
        SceneManager.LoadScene(StaticStrings.MAIN_MENU_SCENE);
    }

    public void OnCancelButtonPress()
    {
        Cancel();
    }

    public void OnSaveButtonPress()
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
        if (!DialogSystemBehavior.inDialog)
        {
            if (SceneManager.GetActiveScene().name != StaticStrings.BOOTHS_MANAGEMENT_SCENE)
            {
                if (!StaticBooleans.tutorialIsOn)
                {
                    CommonButtonBehavior();
                    //StaticFunctions.Save(StaticStrings.SAVE_FILE);
                    SceneManager.LoadScene(StaticStrings.SAVE_SCENE);
                }
                else
                {
                    GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                    errorPopupInstance.transform.SetParent(canvas.transform, false);
                    errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot save during the tutorial.";
                }
            }
            else
            {
                if (canvas != null)
                {
                    GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                    errorPopupInstance.transform.SetParent(canvas.transform, false);
                    errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot save during work.";
                }
            }
        }
        else
        {
            if (canvas != null)
            {

                GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                errorPopupInstance.transform.SetParent(canvas.transform, false);
                errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot save during a dialog.";
            }
            
        }
    }

    public void OnLoadButtonPress()
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

        if (!DialogSystemBehavior.inDialog)
        {
            if (SceneManager.GetActiveScene().name != StaticStrings.BOOTHS_MANAGEMENT_SCENE)
            {
                if (!StaticBooleans.tutorialIsOn)
                {
                    CommonButtonBehavior();
                    //StaticFunctions.Load();
                    SceneManager.LoadScene(StaticStrings.LOAD_SCENE);
                }
                else
                {
                    if (canvas != null)
                    {
                        GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                        errorPopupInstance.transform.SetParent(canvas.transform, false);
                        errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot load during the tutorial.";
                    }
                }
            }
            else
            {
                if (canvas != null)
                {
                    GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                    errorPopupInstance.transform.SetParent(canvas.transform, false);
                    errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot load during work.";
                }
            }
        }
        else
        {
            if (canvas != null)
            {
                GameObject errorPopupInstance = Instantiate(errorPopup, new Vector3(0, 0, 0), Quaternion.identity);
                errorPopupInstance.transform.SetParent(canvas.transform, false);
                errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot load during a dialog.";
            }

        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("ExitMenu"))
        {
            Cancel();
        }
    }

    private void Cancel()
    {
        CommonButtonBehavior();
        Destroy(this.gameObject);
        BoothGameData.boothGameIsPaused = false;
        
    }

    private void CommonButtonBehavior()
    {
        StaticBooleans.exitMenuOn = false;
    }

    public void OnToOptionsButtonPress()
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
            GameObject instance = Instantiate(settingPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(canvas.transform, false);
        }
    }

}
