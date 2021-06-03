using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoliciesBehaviors : MonoBehaviour
{
    //public List<string> directories;

    public List<Policy> policies = new List<Policy>();

    public GameObject contentList;

    public GameObject policyPrefab;

    public GameObject popup;

    public GameObject secondaryImages;

    public GameObject errorPopup;

    private Image mainPopupImage;

    public GameObject toClubBlocker;

    //Check if all the predecessors of a policy are owned
    private bool PredecessorsOwned(Policy policy)
    {
        bool policyOwned = true;
        foreach (Policy pol in policy.predecessors)
        {
            if (policyOwned)
            {
                policyOwned = false;
                foreach (Policy p in GMPoliciesData.ownedPolicies)
                {
                    if (pol.name == p.name)
                    {
                        policyOwned = true;
                        break;
                    }
                }
            }

        }
        return policyOwned;
    }

    //Returns true if the policy in parameter is already owned
    private bool PolicyOwned(Policy policy)
    {
        foreach(Policy pol in GMPoliciesData.ownedPolicies)
        {
            if(policy.name == pol.name)
            {
                return true;
            }
        }
        return false;
    }

    //Creates one item in the list of policies
    private void CreateOneItemInList(Policy policy)
    {
        if (PredecessorsOwned(policy) && !PolicyOwned(policy))
        {
            //If the policy is the first in a new category (e.g. pose naked for all 3 pose type prestations), 
            //if the booth has not been built, the policy corrseponding is not shown in the list
            if (((policy.name == StaticStrings.DANCE_POLICY_NAME && GMImprovementsData.improvementsData.stage)
                || (policy.name == StaticStrings.POSE_NAKED_POLICY_NAME && GMImprovementsData.improvementsData.photostudio)
                || (policy.name == StaticStrings.HANDJOB_POLICY_NAME && GMImprovementsData.improvementsData.foreplayBooth)
                || (policy.name == StaticStrings.BLOWJOB_POLICY_NAME && GMImprovementsData.improvementsData.oralBooth)
                || (policy.name == StaticStrings.VAGINAL_FACING_POLICY_NAME && GMImprovementsData.improvementsData.sexBooth)
                || (policy.name == StaticStrings.THREESOME_POLICY_NAME && GMImprovementsData.improvementsData.groupRoom))
                || (policy.name != StaticStrings.DANCE_POLICY_NAME 
                    && policy.name != StaticStrings.POSE_NAKED_POLICY_NAME
                    && policy.name != StaticStrings.HANDJOB_POLICY_NAME
                    && policy.name != StaticStrings.BLOWJOB_POLICY_NAME
                    && policy.name != StaticStrings.VAGINAL_FACING_POLICY_NAME
                    && policy.name != StaticStrings.THREESOME_POLICY_NAME
                    )
                )
            {
                GameObject instance = Instantiate(policyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                instance.transform.SetParent(contentList.transform, false);
                instance.name = policy.name;

                //Look at all the images in the prefab
                foreach (Image img in instance.GetComponentsInChildren<Image>(true))
                {
                    //The untagged one is the main image
                    if (img.CompareTag("Untagged"))
                    {
                        if (policy.name == StaticStrings.FOOTJOB_POLICY_NAME && PlayerPrefs.GetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS) == 0)
                        {
                            img.sprite = Resources.Load<Sprite>(StaticStrings.PLACEHOLDER_IMAGE_NAME);
                        }
                        else
                        {
                            img.sprite = Resources.Load<Sprite>(StaticStrings.POLICIES_FOLDER +
                                policy.name + "/" + policy.imgPath);
                        }
                    }
                    //The one tagged with "BuyableImage" is the green overlay that indicates that this policy is buyable
                    else if (img.CompareTag("BuyableImage"))
                    {
                        if (GMClubData.money >= policy.priceMoney
                            && GMClubData.GetConnection() >= policy.priceConnection
                            && GMClubData.GetInfluence() >= policy.priceInfluence)
                        {
                            img.gameObject.SetActive(true);
                        }
                        else
                        {
                            img.gameObject.SetActive(false);
                        }
                    }
                }

                Button b = instance.GetComponentInChildren<Button>();
                b.onClick.AddListener(delegate () { OnButtonClick(policy.name, policy); });

                Text[] texts = instance.GetComponentsInChildren<Text>();
                foreach (Text t in texts)
                {
                    if (t.CompareTag("ImprovementDescription"))
                    {
                        t.text = policy.description;
                    }
                    if (t.CompareTag("ImprovementName"))
                    {
                        t.text = policy.name;
                    }
                    if (t.CompareTag("ImprovementPrice"))
                    {
                        t.text = policy.priceMoney + StaticStrings.MONEY_SIGN;
                        t.text += "\nOpenness above " + policy.minOpenness;
                    }
                }
            }
        }
    }

    private void Awake()
    {
        CreateList();
    }

    //Creates the list of policies
    private void CreateList()
    {
        if (GameMasterGlobalData.policiesList == null)
        {
            foreach (string d in StaticStrings.POLICIES_DIRECTORIES_NAMES)
            {
                Policy policy = JsonUtility.FromJson<Policy>(Resources.Load<TextAsset>(
                 StaticStrings.POLICIES_FOLDER + d + "/" + StaticStrings.POLICY_DATA_FILE).text);
                policies.Add(policy);
            }
            GameMasterGlobalData.policiesList = policies;

            foreach (Policy pol in policies)
            {
                pol.CreatePredecessors();
            }

            foreach (Policy p in policies)
            {
                CreateOneItemInList(p);
            }
        }
        else
        {
            foreach (Policy policy in GameMasterGlobalData.policiesList)
            {
                CreateOneItemInList(policy);
            }
        }

        if(GMPoliciesData.ownedPolicies.Count == StaticStrings.POLICIES_DIRECTORIES_NAMES.Length)
        {
            contentList.gameObject.SetActive(false);
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_POLICIES_BOUGHT_ERROR_POPUP_MESSAGE;
            Button b = errorPopup.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.CLUB_SCENE); });
        }

    }

    //The OnClick action for each image in the improvements menu
    private void OnButtonClick(string policyName, Policy policy)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.POLICIES_FOLDER + policy.name + "/" + policy.imgPath);
                mainPopupImage = img;
                break;
            }
        }
        Text detailedDesc = popup.GetComponentInChildren<Text>();

        detailedDesc.text = policy.detailedDescription;
        if (policy.priceMoney == 0)
        {
            if (policy.minOpenness == 0)
            {
                detailedDesc.text += StaticStrings.POLICY_PRICE_DISPLAY_FREE;
            }
            else
            {
                detailedDesc.text += StaticStrings.POLICY_PRICE_DISPLAY_FREE_BUT_REQUIRES_GIRL_OPENNESS +
                    policy.minOpenness + StaticStrings.POINT;
            }
        }
        else
        {
            detailedDesc.text += StaticStrings.POLICY_PRICE_DISPLAY_REQUIRES + policy.priceMoney + StaticStrings.MONEY_SIGN +
                StaticStrings.POLICY_PRICE_DISPLAY_GIRL_OPENNESS + policy.minOpenness + StaticStrings.POINT;
        }


        //Create the confirm button in the popup
        foreach (Button b in popup.GetComponentsInChildren<Button>())
        {
            if (b.CompareTag("PopupConfirm"))
            {
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { OnClick(policyName, policy); });
                break;
            }
        }
    }

    private void RefreshOneItem(Policy policy)
    {
        foreach (Transform t in contentList.transform)
        {
            if (t.name == policy.name)
            {
                Destroy(t.gameObject);
            }
        }
    }

    private void EmptyList()
    {
        foreach (Transform child in contentList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //The actions taken after each buying of a policy
    private void PolicyConfirmActions(string policyName, Policy policy)
    {
        StaticFunctions.BuildPoliciessModifiers(policyName,policy, toClubBlocker);
    }

    //Returns true if at least one girl can perform this action
    private bool OneGirlCanPerform(float minOpennessRequired)
    {
        if (GMRecruitmentData.recruitedGirlsList != null)
        {
            foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
            {
                if (girl.GetOpenness() >= minOpennessRequired)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //The OnClick action for the confirm button of the popup
    private void OnClick(string policyName, Policy policy)
    {
        if (GMClubData.money >= policy.priceMoney
            && GMClubData.GetConnection() >= policy.priceConnection
            && GMClubData.GetInfluence() >= policy.priceInfluence
            && OneGirlCanPerform(policy.minOpenness))
        {
            PolicyConfirmActions(policyName,policy);

            GMPoliciesData.ownedPolicies.Add(policy);

            GMClubData.SpendMoney(policy.priceMoney);
            GMClubData.SpendInfluence(policy.priceInfluence);
            GMClubData.SpendConnection(policy.priceConnection);

            //RefreshOneItem(policy);

            SceneManager.LoadScene(StaticStrings.POLICIES_SCENE);
        }
        else
        {
            //If the player doesn't have enough of a ressource, the corresponding error message is displayed
            if (GMClubData.money < policy.priceMoney)
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_MONEY_POLICY_ERROR_MESSAGE;
            }
            else if (GMClubData.GetConnection() < policy.priceConnection)
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_CONNECTION_POLICY_ERROR_MESSAGE;
            }
            else if (GMClubData.GetInfluence() < policy.priceInfluence)
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_INFLUENCE_POLICY_ERROR_MESSAGE;
            }
            else
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NO_GIRL_ABLE_POLICY_ERROR_MESSAGE;
            }
        }

        secondaryImages.SetActive(false);
        popup.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        popup.SetActive(false);
        secondaryImages.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

}
