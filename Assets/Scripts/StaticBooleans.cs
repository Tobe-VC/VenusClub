using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticBooleans
{
    //DO NOT FORGET TO ADD BOOLEANS IMPORTANT TO THE SAVE SYSTEM TO THE INITIALIZER !!!!!

    //True if the game has been loaded from a save file
    public static bool gameIsLoaded = false;

    //True if the exit menu is activated 
    public static bool exitMenuOn = false;

    //True if the tutorial is currently being played
    public static bool tutorialIsOn = true;

    //True if the tutorial is currently being played in the policies
    //It is used to avoid the problem of displaying the tutorial again after buying the dance policy 
    //as the scene is reloaded after each passing of a policy
    public static bool tutorialIsOnPolicies = true;

    public static bool tutorialIsOnBoothGame = true;

    //True if the day recap should be displayed
    public static bool displayDayRecap = false;

    //True if the day recap is over
    public static bool dayRecapOver = false;

    //True if the player is coming from the work screen to the club screen
    public static bool comeFromWork = false;
    /// <summary>
    /// True if the wardrobe is accessible (i.e. if the Wardrobe improvement has been bought)
    /// </summary>
    public static bool wardrobeAvailable = false;

    /// <summary>
    /// True if the improved score increases (mouth) have been bought from the personal store
    /// </summary>
    public static bool dateGameBetterScoreIncreaseBought = false;

    /// <summary>
    /// True if the freeze time have been bought from the personal store
    /// </summary>
    public static bool dateGameFreezeTimeUnlocked = false;

    /// <summary>
    /// True if the tennis date is unlocked
    /// </summary>
    public static bool dateTennisUnlocked = false;



    //DO NOT FORGET TO ADD BOOLEANS IMPORTANT TO THE SAVE SYSTEM TO THE INITIALIZER !!!!!

    public static void InitializeBooleans()
    {
        wardrobeAvailable = false;
        dateGameBetterScoreIncreaseBought = false;
    }
    
}
