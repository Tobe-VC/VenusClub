using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CrimeService
{

    public int id;

    public int pricePerDayMoney;

    public float priceToAccess;

    public string name;
    public string description;
    public string detailedDescription;
    public string imagePath;
    public string folder;


    public bool isUnlocked;
    public bool isSubscribed;

    public UnityAction effectOnUnlock;
    public UnityAction effectOnSubscribe;
    public UnityAction effectOnUnsubscribe;

    public CrimeService(int id, int pricePerDayMoney, float priceToAccess, string name, string description, string detailedDescription,
        string imagePath, string folder, bool isUnlocked = false, bool isSubscribed = false, UnityAction effectOnUnlock = null, 
        UnityAction effectOnSubscribe = null, UnityAction effectOnUnsubscribe = null)
    {
        this.id = id;
        this.pricePerDayMoney = pricePerDayMoney;
        this.priceToAccess = priceToAccess;
        this.name = name;
        this.description = description;
        this.detailedDescription = detailedDescription;
        this.imagePath = imagePath;
        this.folder = folder;
        this.isUnlocked = isUnlocked;
        this.isSubscribed = isSubscribed;
        this.effectOnUnlock = effectOnUnlock;
        this.effectOnSubscribe = effectOnSubscribe;
        this.effectOnUnsubscribe = effectOnUnsubscribe;
    }

    public CrimeService(string subfolderName, bool isUnlocked = false, bool isSubscribed = false, UnityAction effectOnUnlock = null,
        UnityAction effectOnSubscribe = null, UnityAction effectOnUnsubscribe = null)
    {
        CrimeService cs = JsonUtility.FromJson<CrimeService>(Resources.Load<TextAsset>(
 StaticStrings.CRIME_SERVICES_FOLDER + subfolderName + "/" + StaticStrings.CRIME_SERVICES_DATA_FILE).text);
        this.id = cs.id;
        this.pricePerDayMoney = cs.pricePerDayMoney;
        this.priceToAccess = cs.priceToAccess;
        this.name = cs.name;
        this.description = cs.description;
        this.detailedDescription = cs.detailedDescription;
        this.imagePath = cs.imagePath;
        this.folder = cs.folder;
        this.isUnlocked = isUnlocked;
        this.isSubscribed = isSubscribed;
        this.effectOnUnlock = effectOnUnlock;
        this.effectOnSubscribe = effectOnSubscribe;
        this.effectOnUnsubscribe = effectOnUnsubscribe;

    }

    public void Unlock()
    {
        isUnlocked = true;
        effectOnUnlock?.Invoke();
    }

    public void Subscribe()
    {
        isSubscribed = true;
        effectOnSubscribe?.Invoke();
    }

    public void Unsubscribe()
    {
        isSubscribed = false;
        effectOnUnsubscribe?.Invoke();
    }

}
