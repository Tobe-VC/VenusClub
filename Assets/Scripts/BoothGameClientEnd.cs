using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoothGameClientEnd : MonoBehaviour
{
    private float clientTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_BOOTH_TIME;

    private float finishTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_FINISH_TIME;

    private float fixedClientTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_BOOTH_TIME;

    private bool isTimerStarted = false;

    private Booth booth;

    private Client client;

    private bool clientHasFinished = false;

    private bool isFinishing = false;

    private float earnMoneyTimer = GMGlobalNumericVariables.gnv.EARN_MONEY_TIMER;

    private float earnOpennessTimer = GMGlobalNumericVariables.gnv.EARN_OPENNESS_TIMER;

    public GameObject endButtons;

    public Button facialButton;

    public Button bodyCumshotButton;

    public Button insideFinishButton;

    public Button swallowButton;

    public Button titsCumshotButton;

    public Button insideAnalFinishButton;

    public Button doubleFinishButton;
    public Button tripleFinishButton;

    public Button extendButton;

    public int boothIndex;

    public BoothGameReloadUIBooth UIReloader;

    public BoothGameBehavior behavior;

    public Text earningsText;

    public Text currentActText;

    public Button peekButton;
    public Button zoomInButton;


    private float ClientMultiplier()
    {
        switch (booth.client.clientType)
        {
            case ClientType.VERY_POOR: return GMGlobalNumericVariables.gnv.EARN_MONEY_VERY_POOR_CLIENT_MULTIPLIER;
            case ClientType.POOR: return GMGlobalNumericVariables.gnv.EARN_MONEY_POOR_CLIENT_MULTIPLIER;
            case ClientType.BELOW_AVERAGE: return GMGlobalNumericVariables.gnv.EARN_MONEY_BELOW_AVERAGE_CLIENT_MULTIPLIER;
            case ClientType.AVERAGE: return GMGlobalNumericVariables.gnv.EARN_MONEY_AVERAGE_CLIENT_MULTIPLIER;
            case ClientType.ABOVE_AVERAGE: return GMGlobalNumericVariables.gnv.EARN_MONEY_ABOVE_AVERAGE_CLIENT_MULTIPLIER;
            case ClientType.RICH: return GMGlobalNumericVariables.gnv.EARN_MONEY_RICH_CLIENT_MULTIPLIER;
            case ClientType.VERY_RICH: return GMGlobalNumericVariables.gnv.EARN_MONEY_VERY_RICH_CLIENT_MULTIPLIER;
            case ClientType.MEGA_RICH: return GMGlobalNumericVariables.gnv.EARN_MONEY_MEGA_RICH_CLIENT_MULTIPLIER;
            default: return GMGlobalNumericVariables.gnv.EARN_MONEY_AVERAGE_CLIENT_MULTIPLIER;
        }
    }

    private float BaseMoneyAmount()
    {
        switch (client.favoriteSexAct)
        {
            case Work.DANCE: return GMGlobalNumericVariables.gnv.EARN_MONEY_DANCE_AMOUNT;
            case Work.POSE: return GMGlobalNumericVariables.gnv.EARN_MONEY_POSE_AMOUNT;
            case Work.FOREPLAY: return GMGlobalNumericVariables.gnv.EARN_MONEY_FOREPLAY_AMOUNT;
            case Work.ORAL: return GMGlobalNumericVariables.gnv.EARN_MONEY_ORAL_AMOUNT;
            case Work.SEX: return GMGlobalNumericVariables.gnv.EARN_MONEY_SEX_AMOUNT;
            case Work.GROUP: return GMGlobalNumericVariables.gnv.EARN_MONEY_GROUP_AMOUNT;
            default: return 0;
        }
    } 

    private float GirlSkillValue(GirlClass girl, Work work)
    {
        switch (work)
        {
            case Work.DANCE: return girl.GetDancing();
            case Work.POSE: return girl.GetPosing();
            case Work.FOREPLAY: return girl.GetForeplay();
            case Work.ORAL: return girl.GetOral();
            case Work.SEX: return girl.GetSex();
            case Work.GROUP: return girl.GetGroup();
        }
        return 1;
    }

    //Computes the amount of money earned by a girl for each second passed with a client 
    //The formula is multiplier x (base_amount + (10% of girl popularity) + (10% of client happiness)) + a bonus depending on her skills
    //multiplier's value depends on the type of client (see the switch instruction, can be changed in StaticNumbers)
    //The base amount is 10 (can be changed in StaticNumbers)

    //Also increases the client's happiness based on the skill of the girl for the client's favorite sex act
    //It increases by CLIENT_HAPPINESS_INCREASE_MULTIPLIER * the girl's skill + CLIENT_BASE_HAPPINESS_GAIN (can be changed in StaticNumbers)
    //every second 

    //Also decreases the girl's energy by EARN_MONEY_GIRL_ENERGY_LOST (can be changed in StaticNumbers)

    //Also gives the girl a chance for XP for her currenct act
    //Calculation of chance is made through GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_MIN, 
    //GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_MAX and GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_VALUE_TO_REACH
    private void EarnMoney()
    {
        //If the timer has ended and the girl has energy
        if (earnMoneyTimer <= 0f && booth.girl.GetEnergy() > 0)
        {
            float multiplier = ClientMultiplier();

            int moneyEarned = Mathf.RoundToInt(multiplier * (
                BaseMoneyAmount() + ((booth.girl.StatsValue(client.favoriteSexAct) * GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_SKILL_MULTIPLIER)) + (booth.girl.GetPopularity() * GMGlobalNumericVariables.gnv.EARN_MONEY_POPULARITY_MULTIPLIER)
                + (booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.EARN_MONEY_HAPPINESS_MULTIPLIER)));

            if(GMWardrobeData.currentlyUsedCostume.name == StaticStrings.EARNINGS_INCREASE_COSTUME_NAME)
            {
                //If the currently used costume is the bunny, increase the earnings by 25%
                moneyEarned = Mathf.RoundToInt(moneyEarned * 
                    GMGlobalNumericVariables.gnv.EARNINGS_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]);
            }
            else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
            {
                //If the currently used costume is the naked, increase the earnings by 5%
                moneyEarned = Mathf.RoundToInt(moneyEarned * 
                    GMGlobalNumericVariables.gnv.NAKED_EARNINGS_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]);
            }

            //If the coach is activaed, increase the earnings
            CrimeService csCoach = GMCrimeServiceData.unlockedCrimeServices.Find(x => x.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_COACH_ID);
            CrimeService csRelaxer = GMCrimeServiceData.unlockedCrimeServices.Find(x => x.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_RELAXER_ID);
            if (csCoach != null && csCoach.isSubscribed)
            {
                moneyEarned = Mathf.RoundToInt(moneyEarned * GMGlobalNumericVariables.gnv.COACH_EARNINGS_MULTIPLIER);
            }
            if (csRelaxer != null && csRelaxer.isSubscribed)
            {
                moneyEarned = Mathf.RoundToInt(moneyEarned * GMGlobalNumericVariables.gnv.RELAXER_EARNINGS_MULTIPLIER);
            }

            booth.girl.moneyEarned += moneyEarned;
            earnMoneyTimer = GMGlobalNumericVariables.gnv.EARN_MONEY_TIMER;

            int randomRange = Random.Range(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_MIN, 
                GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_MAX);

            int randomRangeValueToReach = GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_RANDOM_RANGE_VALUE_TO_REACH;
            if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.SKILL_GAIN_INCREASE_COSTUME_NAME)
            {
                //If the currently used costume is the gym clothes, double the skill gain chance
                randomRangeValueToReach -= 
                    GMGlobalNumericVariables.gnv.SKILL_GAIN_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
            }
            else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
            {
                //If the currently used costume is naked, increase the skill gain chance by 20%
                randomRangeValueToReach -= 
                    GMGlobalNumericVariables.gnv.NAKED_SKILL_GAIN_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
            }

            switch (client.favoriteSexAct)
            {
                //There is a 10% chance for an XP gain during work, except with the gym clothes that add a 25% chance (rounding it at 35%)
                case Work.DANCE: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN +
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetDancing()) * GMGlobalNumericVariables.gnv.DANCE_POLICIES_MULTIPLIER); ;
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToDancing(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

                case Work.POSE: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN + 
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetPosing()) * GMGlobalNumericVariables.gnv.POSE_POLICIES_MULTIPLIER);
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToPosing(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

                case Work.FOREPLAY: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN + 
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetForeplay()) * GMGlobalNumericVariables.gnv.FOREPLAY_POLICIES_MULTIPLIER);
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToForeplay(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

                case Work.ORAL: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN + 
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetOral()) * GMGlobalNumericVariables.gnv.ORAL_POLICIES_MULTIPLIER);
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToOral(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

                case Work.SEX: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN + 
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetSex()) * GMGlobalNumericVariables.gnv.SEX_POLICIES_MULTIPLIER);
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToSex(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

                case Work.GROUP: client.AddToHappiness((GMGlobalNumericVariables.gnv.CLIENT_BASE_HAPPINESS_GAIN + 
                        GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_INCREASE_MULTIPLIER * booth.girl.GetGroup()) * GMGlobalNumericVariables.gnv.GROUP_POLICIES_MULTIPLIER);
                    if (randomRange >= randomRangeValueToReach)
                    {
                            booth.girl.AddToGroup(GMGlobalNumericVariables.gnv.BOOTH_GAME_WORK_XP_VALUE);
                    }
                    break;

            }

            float energyLost = 0f;
            
            if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ENERGY_LOST_DECREASE_COSTUME_NAME)
            {
                energyLost = GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST *
                    GMGlobalNumericVariables.gnv.ENERGY_LOST_DECREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
                //If the currently used costume is the nurse, decrease the girls energy lost by 50%
                /*booth.girl.AddToEnergy(-(GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST * 
                    GMGlobalNumericVariables.gnv.ENERGY_LOST_DECREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]));*/
            }
            else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
            {
                energyLost = GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST *
                    GMGlobalNumericVariables.gnv.NAKED_ENERGY_LOST_DECREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
                //If the currently used costume is naked, decrease the girls energy lost by 10%
                /*booth.girl.AddToEnergy(-(GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST *
                    GMGlobalNumericVariables.gnv.NAKED_ENERGY_LOST_DECREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]));*/
            }
            else
            {
                energyLost = GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST;
                //booth.girl.AddToEnergy(-GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST);
            }

            if(csCoach != null && csCoach.isSubscribed)
            {
                energyLost *= GMGlobalNumericVariables.gnv.COACH_ENERGY_LOST_MULTIPLIER;
            }

            if (csRelaxer != null && csRelaxer.isSubscribed)
            {
                energyLost *= GMGlobalNumericVariables.gnv.RELAXER_ENERGY_LOST_MULTIPLIER;
            }

            booth.girl.AddToEnergy(-energyLost);

            earningsText.text = "+" + moneyEarned;
            earningsText.CrossFadeAlpha(1, 0f, true);
            earningsText.CrossFadeAlpha(0, 0.5f,true);
        }
        else
        {
            earnMoneyTimer -= Time.deltaTime;
        }
    }

    private void EarnOpenness()
    {
        if(earnOpennessTimer <= 0f)
        {
            if (CanEarnOpenness())
            {
                booth.girl.AddToOpenness(GMGlobalNumericVariables.gnv.EARN_OPENNESS_AMOUNT);
            }
            earnOpennessTimer = GMGlobalNumericVariables.gnv.EARN_OPENNESS_TIMER;
        }
        else
        {
            earnOpennessTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Returns true if the giil can earn openness based on the performance she is giving
    /// </summary>
    /// <returns></returns>
    private bool CanEarnOpenness()
    {
        switch (booth.client.favoriteSexAct)
        {
            case Work.DANCE: return booth.girl.GetOpenness() < GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY;
            case Work.POSE:return booth.girl.GetOpenness() < GMGlobalNumericVariables.gnv.MIN_OPENNESS_ORAL;
            case Work.FOREPLAY:return booth.girl.GetOpenness() < GMGlobalNumericVariables.gnv.MIN_OPENNESS_SEX;
            case Work.ORAL:return booth.girl.GetOpenness() < GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP;
            case Work.SEX:return true;
            case Work.GROUP:return true;
            default: return true;
        }
    }

    //If you choose the right finisher
    //Gain a bonus amount of money 
    //Which is one instance of money gain multiplied by EARN_MONEY_CORRECT_FINISHER_MULTIPLIER (can be changed in StaticNumbers)
    private void GoodFinisherChosen()
    {
        float multiplier = ClientMultiplier();

        booth.girl.moneyEarned += Mathf.RoundToInt((multiplier * (
            BaseMoneyAmount() + (booth.girl.GetPopularity() * GMGlobalNumericVariables.gnv.EARN_MONEY_POPULARITY_MULTIPLIER)
            + booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.EARN_MONEY_HAPPINESS_MULTIPLIER)) * GMGlobalNumericVariables.gnv.EARN_MONEY_CORRECT_FINISHER_MULTIPLIER);
    }

    private void PressButtonCommon()
    {
        currentActText.gameObject.SetActive(true);
        extendButton.gameObject.SetActive(false);
        UIReloader.TurnOffEndButtons();
    }

    //If the client's timer end without a girl comming
    //The popularity of the club is decreased by CLIENT_END_WITHOUT_GIRL_POPULARITY_DROP * clientMultiplier
    //They both can be changed in StaticNumbers
    private void EndWithoutGirl()
    {

        //If the client belongs to one of the groups, lose influence for this group
        if (booth.client.clientGroup == ClientGroup.CRIMINAL)
        {
            GMClubData.SpendInfluence(GMGlobalNumericVariables.gnv.INFLUENCE_LOST);
        }
        else if (booth.client.clientGroup == ClientGroup.POLICE)
        {
            GMClubData.SpendConnection(GMGlobalNumericVariables.gnv.CONNECTION_LOST);
        }

        float multiplier = ClientMultiplier();
        GMClubData.SpendReputation(Mathf.RoundToInt(GMGlobalNumericVariables.gnv.CLIENT_END_WITHOUT_GIRL_POPULARITY_DROP * multiplier));

        VideoPlayer videoPlayer = BoothGameData.UIBooths[boothIndex].GetComponentInChildren<VideoPlayer>();
        clientHasFinished = false;
        //Put the girl back into the roster
        //Empty the booth
        booth.EmptyBooth();
        UIReloader.ReloadUIBooth();
        isFinishing = false;
        clientTimer = fixedClientTimer;
        isTimerStarted = false;
        videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);
        videoPlayer.Stop();
        videoPlayer = new VideoPlayer();
        currentActText.gameObject.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
        earningsText.CrossFadeAlpha(0, 0f, true);
        booth = BoothGameData.booths[boothIndex];

        BoothGameData.UIBooths[boothIndex].GetComponentInChildren<VideoPlayer>().gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);

        if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.INTERACTION_DURATION_INCREASE_COSTUME_NAME)
        {
            //If the currently used costume is the basic one, increase the duration of the prestations
            fixedClientTimer *= GMGlobalNumericVariables.gnv.INTERACTION_DURATION_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
            clientTimer = fixedClientTimer;
        }
        else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
        {
            //If the currently used costume is the naked one, increase the duration of the prestations
            fixedClientTimer *= GMGlobalNumericVariables.gnv.NAKED_INTERACTION_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
            clientTimer = fixedClientTimer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!BoothGameData.boothGameIsPaused)
        {

            if (!isTimerStarted && booth.client != null)
            {
                client = booth.client;
                StartClientTimer();
            }

            //If the client timer is started
            if (isTimerStarted)
            {
                //Decrease the timer
                clientTimer -= Time.deltaTime;

                //If the timer has reached zero
                if (clientTimer <= 0 && !isFinishing)
                {
                    if (booth.girl == null)
                    {
                        EndWithoutGirl();
                    }
                    else
                    {
                        //Check which finisher is available and offer buttons accordingly
                        if (GMPoliciesData.policies.creampie 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_INSIDE_FINISH
                            && booth.girl.doCreampie
                            && PeformanceUnlocksFinisher(Finisher.CREAMPIE))
                            insideFinishButton.gameObject.SetActive(true);

                        if (GMPoliciesData.policies.bodyFinish 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_BODY_FINISH
                            && booth.girl.doBodyCumshot
                            && PeformanceUnlocksFinisher(Finisher.BODY))
                            bodyCumshotButton.gameObject.SetActive(true);

                        if (GMPoliciesData.policies.facial 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_FACIAL_FINISH
                            && booth.girl.doFacial
                            && PeformanceUnlocksFinisher(Finisher.FACIAL))
                            facialButton.gameObject.SetActive(true);

                        if (GMPoliciesData.policies.titsFinish 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_TITS_FINISH
                            && booth.girl.doTitsCumshot
                            && PeformanceUnlocksFinisher(Finisher.TITS))
                            titsCumshotButton.gameObject.SetActive(true);

                        if (GMPoliciesData.policies.swallow 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_SWALLOW_FINISH
                            && booth.girl.doSwallow
                            && PeformanceUnlocksFinisher(Finisher.SWALLOW))
                            swallowButton.gameObject.SetActive(true);

                        if (GMPoliciesData.policies.analCreampie 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_INSIDE_ANAL_FINISH
                            && booth.girl.doAnalCreampie
                            && PeformanceUnlocksFinisher(Finisher.ANAL_CREAMPIE))
                        {
                            insideAnalFinishButton.gameObject.SetActive(true);
                        }
                        if (GMPoliciesData.policies.threesomeFinish 
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_DOUBLE_FINISH
                            && booth.girl.doThreesomeFinish
                            && PeformanceUnlocksFinisher(Finisher.THREESOME))
                        {
                            doubleFinishButton.gameObject.SetActive(true);
                        }
                        if (GMPoliciesData.policies.foursomeFinish
                            && booth.girl.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_TRIPLE_FINISH
                            && booth.girl.doGroupFinish
                            && PeformanceUnlocksFinisher(Finisher.FOURSOME))
                        {
                            tripleFinishButton.gameObject.SetActive(true);
                        }

                        //If no finisher are available, end the prestation now
                        if ((!insideFinishButton.gameObject.activeSelf &&
                            !bodyCumshotButton.gameObject.activeSelf &&
                            !facialButton.gameObject.activeSelf &&
                            !titsCumshotButton.gameObject.activeSelf &&
                            !swallowButton.gameObject.activeSelf &&
                            !insideAnalFinishButton.gameObject.activeSelf &&
                            !doubleFinishButton.gameObject.activeSelf &&
                            !tripleFinishButton.gameObject.activeSelf
                            )
                            || booth.girl.GetEnergy() <= 0)
                        {
                            PressButtonCommon(); //The common behavior for all buttons

                            //GMClubData.reputation += booth.client.happiness * GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_TRANSFER_RATIO;

                            //If the client belongs to one of the groups, gain the corresponding influence
                            //The gained influence is the happinness times the influence gain ratio (currentyl 0.02)
                            if (booth.client.clientGroup == ClientGroup.CRIMINAL)
                            {
                                GMClubData.EarnInfluence(booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.INFLUENCE_GAIN_RATIO);
                            }
                            else if (booth.client.clientGroup == ClientGroup.POLICE)
                            {
                                GMClubData.EarnConnection(GMGlobalNumericVariables.gnv.CONNECTION_GAIN);
                            }

                            booth.girl.AddToPopularity(GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_POPULARITY_GAIN);

                            isFinishing = true;
                            UIReloader.TurnOffPeekButton();
                            VideoPlayer videoPlayer = BoothGameData.UIBooths[boothIndex].GetComponentInChildren<VideoPlayer>();
                            videoPlayer.Stop();
                            videoPlayer.Prepare();
                            videoPlayer = new VideoPlayer();
                            clientHasFinished = true;
                        }

                        endButtons.SetActive(true);
                        if (client.GetHappiness() >= GMGlobalNumericVariables.gnv.BOOTH_GAME_MIN_HAPPINESS_FOR_EXTEND && !client.currentlyExtended)
                        {
                            client.currentlyExtended = true;
                            extendButton.gameObject.SetActive(true);
                        }
                    }

                }
                else
                {
                    if (booth.girl != null)
                    {
                        EarnMoney();
                        EarnOpenness();
                    }
                }
            }

            //If the client has finished
            if (clientHasFinished)
            {
                VideoPlayer videoPlayer = BoothGameData.UIBooths[boothIndex].GetComponentInChildren<VideoPlayer>();
                //If the finishing video is not playing anymore
                if (finishTimer <= 0f)
                {
                    float clientHappinessTransferRatio = GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_TRANSFER_RATIO;

                    if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.REPUTATION_INCREASE_COSTUME_NAME)
                    {
                        clientHappinessTransferRatio *= 
                            GMGlobalNumericVariables.gnv.REPUTATION_GAIN_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
                        //If the currently used costume is topless, increase the reputation gain by 50%
                        //GMClubData.AddToReputation((booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_TRANSFER_RATIO) * 
                            //GMGlobalNumericVariables.gnv.REPUTATION_GAIN_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]);
                    }
                    else if (GMWardrobeData.currentlyUsedCostume.name == StaticStrings.ALL_INCREASE_COSTUME_NAME)
                    {
                        clientHappinessTransferRatio *=
                            GMGlobalNumericVariables.gnv.NAKED_REPUTATION_GAIN_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
                        //If the currently used costume is naked, increase the reputation gain by 10%
                        //GMClubData.AddToReputation((booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.CLIENT_HAPPINESS_TRANSFER_RATIO) *
                            //GMGlobalNumericVariables.gnv.NAKED_REPUTATION_GAIN_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel]);
                    }

                    GMClubData.EarnReputation(booth.client.GetHappiness() * clientHappinessTransferRatio);

                    clientHasFinished = false;
                    //Put the girl back into the roster
                    GirlClass girl = booth.girl;
                    BoothGameData.girlsRoster.Add(girl);
                    //Empty the booth
                    booth.EmptyBooth();
                    UIReloader.ReloadUIBooth();
                    behavior.LoadGirlsList();
                    isFinishing = false;
                    videoPlayer.isLooping = true;
                    clientTimer = fixedClientTimer;
                    isTimerStarted = false;
                    videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);
                    videoPlayer.Stop();
                    videoPlayer.Prepare();
                    videoPlayer = new VideoPlayer();
                    peekButton.gameObject.SetActive(false);
                    zoomInButton.gameObject.SetActive(false);
                    client.isAskingForHelp = false;
                }
                else
                {
                    finishTimer -= Time.deltaTime;
                }
            }
        }
    }

    //Starts the client timer
    public void StartClientTimer()
    {
        isTimerStarted = true;
        finishTimer = 0f;
    }

    public void OnEndExtensionButtonPress()
    {
        clientTimer = fixedClientTimer/2;
        PressButtonCommon();//The common behavior for all buttons
    }

    //The common behavior for all six finisher buttons
    private void FinisherButtonsCommon(string videoPath, string girlName)
    {
        finishTimer = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_FINISH_TIME;
        PressButtonCommon(); //The common behavior for all buttons

        //If the client belongs to one of the groups, gain the correponding influence
        if(booth.client.clientGroup == ClientGroup.CRIMINAL)
        {
            GMClubData.EarnInfluence(booth.client.GetHappiness() * GMGlobalNumericVariables.gnv.INFLUENCE_GAIN_RATIO);
        }
        else if (booth.client.clientGroup == ClientGroup.POLICE)
        {
            GMClubData.EarnConnection(GMGlobalNumericVariables.gnv.CONNECTION_GAIN);
        }

        booth.girl.AddToPopularity(GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_POPULARITY_GAIN);

        isFinishing = true;
        VideoPlayer videoPlayer = BoothGameData.UIBooths[boothIndex].GetComponentInChildren<VideoPlayer>();
        videoPlayer.Stop();
        if (!booth.girl.external)
        {
            StaticFunctions.SelectRandomVideoClipFromFolder(videoPath, booth.girl.folderName, booth.girl, videoPlayer);
        }
        else
        {
            StaticFunctions.SelectRandomVideoClipFromExternalFolder(videoPath, booth.girl.folderName, videoPlayer, booth.girl);
        }
        videoPlayer.Prepare();
        StartCoroutine(WaitForPrepare(videoPlayer));
        
    }

    private IEnumerator WaitForPrepare(VideoPlayer videoPlayer)
    {
        videoPlayer.isLooping = true;
        yield return new WaitUntil(() => videoPlayer.isPrepared);
        videoPlayer.Play();
        //yield return new WaitUntil(() => !videoPlayer.);
        clientHasFinished = true;
    }

    private void GenericEndButton(Finisher finisher, string finishSubFolder)
    {
        insideAnalFinishButton.gameObject.SetActive(false);
        insideFinishButton.gameObject.SetActive(false);
        swallowButton.gameObject.SetActive(false);
        facialButton.gameObject.SetActive(false);
        bodyCumshotButton.gameObject.SetActive(false);
        titsCumshotButton.gameObject.SetActive(false);
        doubleFinishButton.gameObject.SetActive(false);
        tripleFinishButton.gameObject.SetActive(false);

        //If the chosen finisher is the client's favorite one
        if (client.favoriteFinisher == finisher)
        {
            //Get a bonus
            GoodFinisherChosen();
        }
        //Then load a video for the chosen act
        FinisherButtonsCommon(StaticStrings.WORK_FOLDER +
            StaticStrings.FINISH_FOLDER + finishSubFolder, booth.girl.folderName);
    }

    public void OnEndFacialButtonPress()
    {
        GenericEndButton(Finisher.FACIAL, StaticStrings.FACIAL_FINISH_FOLDER + StaticStrings.FACE_FINISH_SUBFOLDER);
    }

    public void OnEndSwallowButtonPress()
    {
        GenericEndButton(Finisher.SWALLOW, StaticStrings.FACIAL_FINISH_FOLDER + StaticStrings.SWALLOW_FINISH_SUBFOLDER);
    }

    public void OnEndBodyButtonPress()
    {
        GenericEndButton(Finisher.BODY, StaticStrings.BODY_FINISH_FOLDER + StaticStrings.BODY_FINISH_SUBFOLDER);
    }

    public void OnEndTitsButtonPress()
    {
        GenericEndButton(Finisher.TITS, StaticStrings.BODY_FINISH_FOLDER + StaticStrings.TITS_FINISH_SUBFOLDER);
    }

    public void OnEndInsideButtonPress()
    {
        GenericEndButton(Finisher.CREAMPIE, StaticStrings.INSIDE_FINISH_FOLDER + StaticStrings.CREAMPIE_FINISH_SUBFOLDER);
    }

    public void OnEndInsideAnalButtonPress()
    {
        GenericEndButton(Finisher.ANAL_CREAMPIE, StaticStrings.INSIDE_FINISH_FOLDER + StaticStrings.ANAL_CREAMPIE_FINISH_SUBFOLDER);
    }

    public void OnDoubleFinishButtonPress()
    {
        GenericEndButton(Finisher.THREESOME, StaticStrings.MULTIPLE_FINISH_FOLDER + StaticStrings.THREESOME_FINISH_SUBFOLDER);
    }

    public void OnTripleFinishButtonPress()
    {
        GenericEndButton(Finisher.FOURSOME, StaticStrings.MULTIPLE_FINISH_FOLDER + StaticStrings.FOURSOME_FINISH_SUBFOLDER);
    }

    /// <summary>
    /// Defines if a performance can unlock a finisher
    /// </summary>
    /// <param name="finisher">The finisher to check for</param>
    /// <param name="performance">The performance</param>
    /// <returns>True if the performance is compatible with the fnisher</returns>
    
    private bool PeformanceUnlocksFinisher(Finisher finisher)
    {
        if(finisher == Finisher.BODY)
        {
            if(booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.NONE)
            {
                return true;
            }
        }
        else if (finisher == Finisher.TITS)
        {
            //The tits finisher is only available if the current sex act is "at least" a titjob
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.NONE 
                && booth.nameOfFolderForPerformanceType != StaticStrings.HANDJOB_SUBFOLDER &&
                booth.nameOfFolderForPerformanceType != StaticStrings.FOOTJOB_SUBFOLDER)
            {
                return true;
            }
        }

        else if(finisher == Finisher.FACIAL)
        {
            //The facial finisher is only available if the current sex act is "at least" a blowjob
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.NONE)
            {
                return true;
            }
        }

        else if (finisher == Finisher.SWALLOW)
        {
            //The swallow finisher is only available if the current sex act is "at least" a facefuck
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.NONE && booth.nameOfFolderForPerformanceType != StaticStrings.BLOWJOB_SUBFOLDER &&
                booth.nameOfFolderForPerformanceType != StaticStrings.DEEPTHROAT_SUBFOLDER)
            {
                return true;
            }
        }

        else if (finisher == Finisher.CREAMPIE)
        {
            //The creampie finisher is only available if the current sex act is "at least" a facing vaginal sex
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.ORAL && booth.currentSexAct != Work.NONE)
            {
                return true;
            }
        }

        else if (finisher == Finisher.ANAL_CREAMPIE)
        {
            //The anal creampie finisher is only available if the current sex act is "at least" anal sex
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.NONE && booth.currentSexAct != Work.ORAL && 
                (booth.nameOfFolderForPerformanceType != StaticStrings.MISSIONARY_SUBFOLDER && booth.nameOfFolderForPerformanceType != StaticStrings.VAGINAL_FACING_SUBFOLDER) &&
                (booth.nameOfFolderForPerformanceType != StaticStrings.DOGGYSTYLE_SUBFOLDER && booth.nameOfFolderForPerformanceType != StaticStrings.VAGINAL_BACK_SUBFOLDER))
            {
                return true;
            }
        }

        else if (finisher == Finisher.THREESOME)
        {
            //The threesome finisher is only available if the current sex act is "at least" a threesome
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.ORAL && booth.currentSexAct != Work.SEX && booth.currentSexAct != Work.NONE)
            {
                return true;
            }
        }

        else if (finisher == Finisher.FOURSOME)
        {
            //The foursome finisher is only available if the current sex act is "at least" a foursome
            if (booth.currentSexAct != Work.DANCE && booth.currentSexAct != Work.POSE && booth.currentSexAct != Work.FOREPLAY &&
                booth.currentSexAct != Work.NONE && booth.currentSexAct != Work.ORAL && booth.currentSexAct != Work.SEX &&
                booth.nameOfFolderForPerformanceType != StaticStrings.THREESOME_SUBFOLDER)
            {
                return true;
            }
        }

        else
        {
            Debug.LogError("One or more finisher misses from the client end buttons choser.");
            return false;
        }
          
        return false;
    }
    
}
