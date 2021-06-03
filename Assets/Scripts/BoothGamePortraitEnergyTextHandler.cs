using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoothGamePortraitEnergyTextHandler : MonoBehaviour
{
    public Text energyText;

    public BoothGamePortraitData portraitData;

    // Update is called once per frame
    void Update()
    {
        energyText.text = Mathf.Floor(portraitData.girl.GetEnergy()).ToString();
    }
}
