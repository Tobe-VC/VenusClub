using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterGirl : MonoBehaviour
{
    //[SerializeField]
    //private List<string> paths = new List<string>();

    [HideInInspector]
    private List<GirlClass> girls = new List<GirlClass>();

    public Image portraitLoader;
    public Image closeupPortraitLoader;

    public GameObject loadingPopup;

    private List<bool> registersOngoing = new List<bool>();

    private bool errorLoadContinue = false;

    List<string> errorMessages = new List<string>();

    public GameObject errorPopup;

    void Awake()
    {
        if (GameMasterGlobalData.girlsList == null)
        {
            
            foreach (string s in StaticStrings.GIRLS_DIRECTORIES_NAMES)
            {
                GirlClass g = GirlClass.CreateFromJSON((Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + s + "/" + 
                    StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE_NO_EXTENSION)).text);
                g.folderName = s;
                girls.Add(g);

                TextAsset[] textAssets = Resources.LoadAll<TextAsset>(StaticStrings.GIRLS_FOLDER + s + "/" +
                    StaticStrings.TEXTS_FOLDER + StaticStrings.DIALOGS_FOLDER);

                foreach (TextAsset ta in textAssets)
                {
                    GirlLesson gl = new GirlLesson(g,ta.text);
                    g.girlLessons.Add(gl);
                    gl.fileName = ta.name;
                }

                

                /*
                foreach(TextAsset ta in textAssets)
                {
                    GirlDialog gd = GirlDialog.CreateFromJSON(ta.text,g);
                    g.girlDialogs.Add(gd);
                    gd.girl = g;
                }

                g.girlDialogs.Add(new GirlDialog(int.MaxValue, g, 
                    new List<DialogLine> { new DialogLine("Narrator", "You had a wonderful talk with her.") },
                    Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                            g.folderName + "/" + StaticStrings.IMAGES_FOLDER +
                            StaticStrings.PORTRAIT_FILE_NO_EXTENSION)));
                */


                /*
                TextAsset jsonTextAsset = Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + s + "/" +
                    StaticStrings.TEXTS_FOLDER + StaticStrings.DIALOGS_FOLDER + StaticStrings.DIALOG_FILE_NO_EXTENSION);
                    */
            }

            DirectoryInfo d = new DirectoryInfo(StaticStrings.GIRLPACKS_DIRECTORY);
            DirectoryInfo[] toSearch = d.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo dir in toSearch)
            {
                errorLoadContinue = false;
                GirlClass g = null;
                try
                {
                StreamReader reader = new StreamReader(dir.FullName + "/" + StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE);
                    g = GirlClass.CreateFromJSON(reader.ReadToEnd());
                    reader.Close();

                    if (g.name == dir.Name)
                    {
                        //Check that the bio file is present
                        /*
                        StreamReader bioReader = new StreamReader(StaticStrings.EXTERNAL_ASSETS_DIRECTORY + g.name 
                            + "/" + StaticStrings.TEXTS_FOLDER + StaticStrings.BIO_FILE);
                        bioReader.Close();
                        */
                        g.folderName = dir.Name;
                        g.external = true;
                        girls.Add(g);

                        if (Directory.Exists(dir.FullName + "/" + StaticStrings.TEXTS_FOLDER + StaticStrings.DIALOGS_FOLDER))
                        {

                            DirectoryInfo dialogDir = new DirectoryInfo(dir.FullName + "/" + StaticStrings.TEXTS_FOLDER +
                                StaticStrings.DIALOGS_FOLDER);

                            foreach (FileInfo file in dialogDir.GetFiles())
                            {

                                reader = new StreamReader(file.FullName);
                                try
                                {
                                    GirlLesson gl = new GirlLesson(g, reader.ReadToEnd());
                                    g.girlLessons.Add(gl);
                                    gl.fileName = file.Name.Substring(0, (file.Name.Length - 5));
                                }
                                catch (Exception)
                                {
                                    errorMessages.Add("The lesson named " + file.Name + " of the girl " + dir.Name + " has an error." +
                                        "\nThe girl will still be available in the game but the lesson will not.");
                                }
                                /*
                                try {
                                    GirlDialog gd = GirlDialog.CreateFromJSON(reader.ReadToEnd(), g);
                                    g.girlDialogs.Add(gd);
                                }
                                catch (Exception)
                                {
                                    errorMessages.Add("The dialog named " + file.Name + " of the girl " + dir.Name +" has an error." +
                                        "\nThe girl will still be available in the game but the dialog will not.");
                                }
                                */
                                reader.Close();

                            }
                        }
                        StartCoroutine(LoadImage(g,new DirectoryInfo(dir.FullName + "/" + StaticStrings.IMAGES_FOLDER)));
                    }
                    else
                    {
                        errorMessages.Add("The girl " + dir.Name + " could not be properly loaded. " +
                            "The girlpack's folder name is different from the name in the JSON file.");
                    }

                }
                catch (DirectoryNotFoundException)
                {
                    errorMessages.Add("The girl " + dir.Name + " could not be properly loaded. " +
                        "It seems that the /" + StaticStrings.JSON_DATA_FOLDER + " folder is missing.");
                }
                catch (FileNotFoundException e)
                {
                    string[] splitted = e.FileName.Split('\\');
                    errorMessages.Add("The girl " + dir.Name + " could not be properly loaded. It seems that the \\"
                      + splitted[splitted.Length - 2] + "/" + splitted[splitted.Length - 1] + " file is missing.");
                }
                catch (ArgumentException)
                {
                    errorMessages.Add("The girl " + dir.Name + " could not be properly loaded. " +
                        "\nIt seems that the /" + StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE + " is incorrect.");
                }
                catch (Exception e)
                {
                    errorMessages.Add("The girl " + dir.Name + " could not be properly loaded for an unknown reason." +
                        "The original message for this error is the following: \n" + e.Message + 
                        "\nPlease report this bug (after an eventual anonymization if needed).");
                }
            }

            GameMasterGlobalData.girlsList = girls;

            if(SceneManager.GetActiveScene().name == StaticStrings.INIT_SCENE)
                SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
        }

        if (girls.Count <= 1 && GameMasterGlobalData.girlsList.Count <= 1)
        {
            Text text = errorPopup.GetComponentInChildren<Text>(true);
            text.text += "No girls in the Girlpacks folder! The game won't be playable if you don't add at least one girlpack!\n";
            errorPopup.gameObject.SetActive(true);
        }


        if (errorMessages.Count > 0)
        {
            Text text = errorPopup.GetComponentInChildren<Text>(true);
            foreach (string s in errorMessages)
            {
                text.text += s+"\n";
            }
            errorPopup.gameObject.SetActive(true);
        }
    }

    private IEnumerator WaitForGirlLoadedErrorContinue()
    {
        //Wait
        yield return new WaitUntil(() => errorLoadContinue);
    }

    /*
    private IEnumerator LoadSprite(GirlClass g)
    {
        yield return StaticFunctions.LoadSpriteFromURL("file:///" +
    StaticStrings.EXTERNAL_ASSETS_DIRECTORY + g.name + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE, sprite);
        g.portrait = sprite;
        portraitLoader.sprite = sprite;
    }
    */
    private void Update()
    {
        if(registersOngoing.Count > 0)
        {
            loadingPopup.gameObject.SetActive(true);
        }
        else
        {
            loadingPopup.gameObject.SetActive(false);
        }
    }

    private IEnumerator LoadImage(GirlClass g, DirectoryInfo dirInfo) 
    {
        bool regOngoing = true;
        registersOngoing.Add(regOngoing);
        /*
        FileInfo[] files = dirInfo.GetFiles();

        List<FileInfo> portraitsFiles = new List<FileInfo>();

        foreach(FileInfo f in files)
        {
            if (f.Name.Contains("portrait_small"))
            {

            }
            else if (f.Name.Contains("portrait"))
            {
                portraitsFiles.Add(f);
            }
        }
        
        foreach (FileInfo f in portraitsFiles)
        {*/
            //Debug.Log(f.Name);
            yield return StaticFunctions.LoadImageFromURL("file:///" +
                StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_JPG,
                "file:///" +
                StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_PNG,
                portraitLoader, true, true);

            g.SetPortrait(portraitLoader.sprite);

        yield return StaticFunctions.LoadImageFromURL("file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_25_TO_50_JPG,
    "file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_25_TO_50_PNG,
    portraitLoader, true,false);

        g.SetPortrait25(portraitLoader.sprite);


        yield return StaticFunctions.LoadImageFromURL("file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_50_TO_75_JPG,
    "file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_50_TO_75_PNG,
    portraitLoader, true, false);

        g.SetPortrait50(portraitLoader.sprite);


        yield return StaticFunctions.LoadImageFromURL("file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_75_TO_100_JPG,
    "file:///" +
    StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_75_TO_100_PNG,
    portraitLoader, true, false);

        g.SetPortrait75(portraitLoader.sprite);
        //}

        yield return StaticFunctions.LoadImageFromURL("file:///" +
            StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
            StaticStrings.IMAGES_FOLDER + StaticStrings.CLOSEUP_PORTRAIT_FILE_JPG,

            "file:///" +
            StaticStrings.GIRLPACKS_DIRECTORY + g.folderName + "/" +
            StaticStrings.IMAGES_FOLDER + StaticStrings.CLOSEUP_PORTRAIT_FILE_PNG,
            closeupPortraitLoader, false, true);

        g.closeupPortrait = closeupPortraitLoader.sprite;

        //The default dialog is added when the coroutine ends
        /*
        g.girlDialogs.Add(new GirlDialog(int.MaxValue, g,
            new List<DialogLine> { new DialogLine("Narrator", "You had a wonderful talk with her.") },
            g.portrait));
            */
        registersOngoing.Remove(regOngoing);
    }
}
