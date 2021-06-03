using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GMClubData : MonoBehaviour
{
    public static int money = GMGlobalNumericVariables.gnv.STARTING_MONEY;
    private static float reputation = GMGlobalNumericVariables.gnv.STARTING_REPUTATION;
    private static float influence = GMGlobalNumericVariables.gnv.STARTING_INFLUENCE;
    private static float connection = GMGlobalNumericVariables.gnv.STARTING_CONNECTION;
    public static int day = GMGlobalNumericVariables.gnv.STARTING_DAY;

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GMClubData s_Instance = null;

    private static int moneySpent = 0;
    private static float reputationSpent = 0;
    private static float influenceSpent = 0;
    private static float connectionSpent = 0;

    private static int moneyEarned = 0;
    private static float reputationEarned = 0;
    private static float influenceEarned = 0;
    private static float connectionEarned = 0;

    // A static property that finds or creates an instance of the manager object and returns it.
    public static GMClubData instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(GMClubData)) as GMClubData;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GMClubData");
                s_Instance = obj.AddComponent<GMClubData>();
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    //Adds a certain amount to the reputation and makes sure that the reputation is always between REPUTATION_MIN and REPUTATION_MAX
    public static void AddToReputation(float toAdd)
    {
        reputation += toAdd;

        if (reputation > GMGlobalNumericVariables.gnv.REPUTATION_MAX)
            reputation = GMGlobalNumericVariables.gnv.REPUTATION_MAX;
        else if (reputation < GMGlobalNumericVariables.gnv.REPUTATION_MIN)
            reputation = GMGlobalNumericVariables.gnv.REPUTATION_MIN;
    }

    public static void AddToInfluence(float toAdd)
    {
        influence += toAdd;
        if (influence < 0)
            influence = 0;
    }

    public static void AddToConnection(float toAdd)
    {
        connection += toAdd;
        if (connection < 0)
            connection = 0;

    }

    public static void SetReputation(float repu)
    {
        reputation = repu;
    }

    public static void SetInfluence(float inf)
    {
        influence = inf;
    }

    public static void SetConnection(float con)
    {
        connection = con;
    }

    public static float GetConnection()
    {
        return connection;
    }

    public static float GetInfluence()
    {
        return influence;
    }

    public static float GetReputation()
    {
        return reputation;
    }

    public static void SpendMoney(int moneySpent)
    {
        money -= moneySpent;
        GMClubData.moneySpent += moneySpent;
    }

    public static void SpendReputation(float reputationSpent)
    {
        AddToReputation(-reputationSpent);
        GMClubData.reputationSpent += reputationSpent;
    }

    public static void SpendInfluence(float influenceSpent)
    {
        AddToInfluence(-influenceSpent);
        GMClubData.influenceSpent += influenceSpent;
    }

    public static void SpendConnection(float connectionSpent)
    {
        AddToConnection(-connectionSpent);
        GMClubData.connectionSpent += connectionSpent;
    }

    public static void SpendAssistantPoints(int pointsSpent)
    {
        StaticAssistantData.data.assistantPoints -= pointsSpent;
    }

    public static void SetSpentMoneyToZero()
    {
        moneySpent = 0;
    }

    public static void SetSpentReputationToZero()
    {
        reputationSpent = 0;
    }

    public static void SetSpentInfluenceToZero()
    {
        influenceSpent = 0;
    }

    public static void SetSpentConnectionToZero()
    {
        connectionSpent = 0;
    }

    public static int GetMoneySpent()
    {
        return moneySpent;
    }

    public static float GetReputationSpent()
    {
        return reputationSpent;
    }

    public static float GetInfluenceSpent()
    {
        return influenceSpent;
    }

    public static float GetConnectionSpent()
    {
        return connectionSpent;
    }

    public static void EarnMoney(int moneyEarned)
    {
        money += moneyEarned;
        GMClubData.moneyEarned += moneyEarned;
    }

    public static void EarnReputation(float reputationEarned)
    {
        AddToReputation(reputationEarned);
        GMClubData.reputationEarned += reputationEarned;
    }

    public static void EarnInfluence(float influenceEarned)
    {
        AddToInfluence(influenceEarned);
        GMClubData.influenceEarned += influenceEarned;
    }

    public static void EarnConnection(float connectionEarned)
    {
        AddToConnection(connectionEarned);
        GMClubData.connectionEarned += connectionEarned;
    }

    public static void SetEarnedMoneyToZero()
    {
        moneyEarned = 0;
    }

    public static void SetEarnedReputationToZero()
    {
        reputationEarned = 0;
    }

    public static void SetEarnedInfluenceToZero()
    {
        influenceEarned = 0;
    }

    public static void SetEarnedConnectionToZero()
    {
        connectionEarned = 0;
    }

    public static int GetMoneyEarned()
    {
        return moneyEarned;
    }

    public static float GetReputationEarned()
    {
        return reputationEarned;
    }

    public static float GetInfluenceEarned()
    {
        return influenceEarned;
    }

    public static float GetConnectionEarned()
    {
        return connectionEarned;
    }
}

