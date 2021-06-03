using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booth {

    public Client client;
    public GirlClass girl;
    public int number;
    public Work currentSexAct;
    public bool helpAsked = false;
    public float timerHelp = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP;
    public bool finished = false;
    public bool extended = false;
    public float timeLeft = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_BOOTH_TIME;
    public Work specialty = Work.NONE;

    public string nameOfFolderForPerformanceType = "";


    public Booth(int number, Work specialty)
    {
        this.number = number;
        this.specialty = specialty;
    }

    public void EmptyBooth()
    {
        client = null;
        girl = null;
        currentSexAct = Work.NONE;
        helpAsked = false;
        timerHelp = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP;
        finished = false;
        extended = false;
        timeLeft = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_BOOTH_TIME;
    }

}
