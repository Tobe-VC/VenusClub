using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoothGameClientHelp : MonoBehaviour
{

    public int boothIndex;

    public Text askHelpText;
    public Text askHelpTimerText;
    public Image askHelpImage;

    public GameObject helpButtons;

    private Booth booth;

    private float askHelpTimer;

    private bool hasClient = false;

    private float clientTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP;

    private float timePassed = 0f;

    private float timerToGetHelp = GMGlobalNumericVariables.gnv.ASK_HELP_TIMER;

    private HelpType typeOfHelpAsked;

    public Sprite condomSprite;
    public Sprite cigaretteSprite;
    public Sprite drinkSprite;
    public Sprite pillSprite;

    public Button cigaretteHelpButton;
    public Button drinkHelpButton;
    public Button drugHelpButton;
    public Button condomHelpButton;

    public GameObject tutorialPopup;

    private bool cigServiceSubscribed;
    private bool condomServiceSubscribed;
    private bool drinkServiceSubscribed;
    private bool drugServiceSubscribed;

    private bool SetCrimeServices(int id)
    {
        CrimeService cs = GMCrimeServiceData.unlockedCrimeServices.Find(x => x.id == id);
        if (cs != null)
        {
            return cs.isSubscribed;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {

        cigServiceSubscribed = SetCrimeServices(GMGlobalNumericVariables.gnv.CRIME_SERVICE_GIFTS_CIGARETTES_ID);
        condomServiceSubscribed = SetCrimeServices(GMGlobalNumericVariables.gnv.CRIME_SERVICE_GIFTS_CONDOMS_ID);
        drinkServiceSubscribed = SetCrimeServices(GMGlobalNumericVariables.gnv.CRIME_SERVICE_GIFTS_DRINKS_ID);
        drugServiceSubscribed = SetCrimeServices(GMGlobalNumericVariables.gnv.CRIME_SERVICE_GIFTS_DRUGS_ID);

        askHelpTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP +
            Random.Range(-GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP_RANDOM_RANGE, GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP_RANDOM_RANGE);
        SetAskedHelp();
    }

    private void SetAskedHelp()
    {
        typeOfHelpAsked = (HelpType)Random.Range(0, System.Enum.GetNames(typeof(HelpType)).Length);
    }

    private void SetHelpImage()
    {
        switch (typeOfHelpAsked)
        {
            case HelpType.CIGARETTE: askHelpImage.sprite = cigaretteSprite; break;
            case HelpType.DRINK: askHelpImage.sprite = drinkSprite; break;
            case HelpType.CONDOM: askHelpImage.sprite = condomSprite; break;
            case HelpType.DRUG: askHelpImage.sprite = pillSprite; break;
        }
    }

    private void AskForHelp()
    {
        if (StaticBooleans.tutorialIsOn && StaticBooleans.tutorialIsOnBoothGame)
        {
            BoothGameData.boothGameIsPaused = true;
            tutorialPopup.SetActive(true);
            StaticBooleans.tutorialIsOnBoothGame = false;
        }

        booth.client.askedForHelp = true;

        askHelpText.text = StaticStrings.ASK_FOR_HELP;
        askHelpText.gameObject.SetActive(true);
        askHelpTimerText.text = timerToGetHelp.ToString() + "s";
        askHelpTimerText.gameObject.SetActive(true);
        SetHelpImage();
        askHelpImage.gameObject.SetActive(true);
        if (GMImprovementsData.improvementsData.cigarettesDispenser == true)
            cigaretteHelpButton.gameObject.SetActive(true);
        if (GMImprovementsData.improvementsData.bar == true)
            drinkHelpButton.gameObject.SetActive(true);
        if (GMImprovementsData.improvementsData.pharmacy == true)
            drugHelpButton.gameObject.SetActive(true);
        if (GMImprovementsData.improvementsData.condomDispenser == true)
            condomHelpButton.gameObject.SetActive(true);
        helpButtons.SetActive(true);


        AutoHelp(HelpType.CIGARETTE, GMImprovementsData.improvementsData.autoCigs || cigServiceSubscribed);
        AutoHelp(HelpType.CONDOM, GMImprovementsData.improvementsData.autoCondom || condomServiceSubscribed);
        AutoHelp(HelpType.DRINK, GMImprovementsData.improvementsData.autoDrink || drinkServiceSubscribed);
        AutoHelp(HelpType.DRUG, GMImprovementsData.improvementsData.autoDrug || drugServiceSubscribed);
    }

    private void AutoHelp(HelpType helpType, bool autoType)
    {
        if (typeOfHelpAsked == helpType && autoType)
        {
            OnHelpButtonPress((int)helpType);
        }
    }

    //What happens when the client stops asking for help either because of time limit or because it has been solved by the player
    private void StopAskingForHelp()
    {
        booth.client.isAskingForHelp = false;

        hasClient = false;

        clientTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP;
        timePassed = 0f;
        timerToGetHelp = GMGlobalNumericVariables.gnv.ASK_HELP_TIMER;
        askHelpTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP +
            Random.Range(-GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP_RANDOM_RANGE, GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP_RANDOM_RANGE);

        SetAskedHelp();
        //askHelpText.text = StaticStrings.ASK_FOR_HELP;
        askHelpText.gameObject.SetActive(false);
        //askHelpTimerText.text = timerToGetHelp.ToString() + "s";
        askHelpTimerText.gameObject.SetActive(false);
        //SetHelpImage();
        askHelpImage.gameObject.SetActive(false);
        helpButtons.SetActive(false);
        cigaretteHelpButton.gameObject.SetActive(false);
        drinkHelpButton.gameObject.SetActive(false);
        drugHelpButton.gameObject.SetActive(false);
        condomHelpButton.gameObject.SetActive(false);
    }

    //If the correct help is offered, the client gains EARN_MONEY_****_HAPPINESS_BOOST happiness (can be changed in StaticNumbers)
    private void CorrectHelp(HelpType code)
    {
        float happinessGain = 0f;
        switch (typeOfHelpAsked)
        {
            case HelpType.CIGARETTE: happinessGain = GMGlobalNumericVariables.gnv.EARN_MONEY_CIGARETTES_HAPPINESS_BOOST; break;
            case HelpType.DRINK: happinessGain = GMGlobalNumericVariables.gnv.EARN_MONEY_DRINK_HAPPINESS_BOOST; break;
            case HelpType.CONDOM: happinessGain = GMGlobalNumericVariables.gnv.EARN_MONEY_CONDOM_HAPPINESS_BOOST; break;
            case HelpType.DRUG: happinessGain = GMGlobalNumericVariables.gnv.EARN_MONEY_DRUG_HAPPINESS_BOOST; break;
        }
        if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.HELPS_INCREASE_COSTUME_NAME)
        {
            happinessGain *= GMGlobalNumericVariables.gnv.HELP_BONUS_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
        }
        else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
        {
            happinessGain *= GMGlobalNumericVariables.gnv.NAKED_HELP_BONUS_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
        }
        booth.client.AddToHappiness(happinessGain);
    }

    //If the incorrect help is offerd, the client loses CLIENT_INCORRECT_HELP_HAPPINESS_LOST happiness (can be changed in StaticNumbers)
    private void IncorrectHelp()
    {
        booth.client.AddToHappiness(-GMGlobalNumericVariables.gnv.CLIENT_INCORRECT_HELP_HAPPINESS_LOST);
    }

    // Start is called before the first frame update
    void Start()
    {
        booth = BoothGameData.booths[boothIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (booth.client == null)
        {
            //askHelpText.text = StaticStrings.ASK_FOR_HELP;
            askHelpText.gameObject.SetActive(false);
            //askHelpTimerText.text = timerToGetHelp.ToString() + "s";
            askHelpTimerText.gameObject.SetActive(false);
            //SetHelpImage();
            askHelpImage.gameObject.SetActive(false);
            helpButtons.SetActive(false);
            cigaretteHelpButton.gameObject.SetActive(false);
            drinkHelpButton.gameObject.SetActive(false);
            drugHelpButton.gameObject.SetActive(false);
            condomHelpButton.gameObject.SetActive(false);
            timePassed = 0f;
        }

        if (!BoothGameData.boothGameIsPaused && hasClient)
        {
            if (booth.girl != null)
            {
                timePassed += Time.fixedDeltaTime;
                if (timePassed >= askHelpTimer && !booth.client.askedForHelp)
                {
                    booth.client.isAskingForHelp = true;
                }

                if (booth.client.isAskingForHelp)
                {
                    if (!booth.client.askedForHelp)
                    {
                        //If the client is currently asking for help but it hasn't been acted upon by the program
                        AskForHelp();
                    }
                    timerToGetHelp -= Time.deltaTime;
                    askHelpTimerText.text = Mathf.RoundToInt(timerToGetHelp).ToString() + "s";
                }

                if (timerToGetHelp <= 0f && booth.client.isAskingForHelp)
                {
                    StopAskingForHelp();
                }
            }
        }
        else
        {
            if (!hasClient && booth.client != null)
            {
                hasClient = true;
            }
        }
    }

    public void OnHelpButtonPress(int code)
    {
        if (typeOfHelpAsked == (HelpType)code)
        {
            CorrectHelp((HelpType)code);
        }
        else
        {
            IncorrectHelp();
        }
        StopAskingForHelp();
    }

}
