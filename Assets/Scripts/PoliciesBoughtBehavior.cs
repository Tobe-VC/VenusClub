using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliciesBoughtBehavior : MonoBehaviour
{
    public GameObject contentList;

    public GameObject policyPrefab;

    public GameObject popup;
    public Image mainPopupImage;



    //Creates one item in the list of improvements
    private void CreateOneItemInList(Policy pol)
    {
        GameObject instance = Instantiate(policyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(contentList.transform, false);
        instance.name = pol.name;
        instance.GetComponentInChildren<Image>().sprite =
            Resources.Load<Sprite>(StaticStrings.POLICIES_FOLDER + pol.name + "/" + pol.imgPath);

        Text[] texts = instance.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.CompareTag("ImprovementName"))
            {
                t.text = pol.name;
            }
        }
        Button b = instance.GetComponentInChildren<Button>();
        b.onClick.AddListener(delegate () { OnButtonClick(pol.name, pol); });
    }

    //Creates the list of improvements
    private void CreateList()
    {
        foreach (Policy pol in GMPoliciesData.ownedPolicies)
        {
            CreateOneItemInList(pol);
        }
    }

    private void Awake()
    {
        CreateList();
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

        if (policy.minOpenness > 0)
        {
            detailedDesc.text += "\n\n" + StaticStrings.POLICY_BOUGHT_PRICE_DISPLAY_REQUIRES_GIRL_OPENNESS_1 + policy.minOpenness
                + StaticStrings.POLICY_BOUGHT_PRICE_DISPLAY_REQUIRES_GIRL_OPENNESS_2;
        }

        Button b = popup.GetComponentInChildren<Button>(true);
        b.onClick.AddListener(delegate () { popup.SetActive(false); });
    }

}
