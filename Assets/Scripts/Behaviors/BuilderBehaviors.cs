using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderBehaviors : MonoBehaviour
{

    public Button barBuildingButton;

    public Button boothsBuildingButton;

    private void LoadNewButtons()
    {

    }

    private void GenericBuildingButtonPress()
    {
        LoadNewButtons();
    }


    // Start is called before the first frame update
    void Start()
    {
        LoadNewButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBarBuildingButtonPress()
    {
        GMBuilderData.builderData.bar = true;
        GenericBuildingButtonPress();
    }

    public void OnBoothsBuildingButtonPress()
    {
        GenericBuildingButtonPress();
    }
}
