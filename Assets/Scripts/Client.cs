using UnityEngine;

public class Client 
{
    public class FavoritePhysicalTraits
    {
        EyeColor eyeColor;
        HairColor hairColor;
        BustType bustType;
        BodyType bodyType;
        SkinComplexion skinComplexion;

        public FavoritePhysicalTraits()
        {
            eyeColor = (EyeColor)Random.Range(0, System.Enum.GetNames(typeof(EyeColor)).Length);
            hairColor = (HairColor)Random.Range(0, System.Enum.GetNames(typeof(HairColor)).Length);
            bustType = (BustType)Random.Range(0, System.Enum.GetNames(typeof(BustType)).Length);
            bodyType = (BodyType)Random.Range(0, System.Enum.GetNames(typeof(BodyType)).Length);
            skinComplexion = (SkinComplexion)Random.Range(0, System.Enum.GetNames(typeof(SkinComplexion)).Length);
        }

        public bool GirlTraitCorresponds(EyeColor eyeColor, HairColor hairColor, 
            BustType bustType, BodyType bodyType, SkinComplexion skinComplexion)
        {
            return eyeColor == this.eyeColor || hairColor == this.hairColor || bustType == this.bustType ||
                bodyType == this.bodyType || skinComplexion == this.skinComplexion;
        }

    }

    public bool currentlyExtended = false;

    public ClientType clientType;

    public FavoritePhysicalTraits favoritePhysicalTraits;

    public Work favoriteSexAct ;

    private float happiness;

    public Finisher favoriteFinisher;

    public int boothNumber;

    public bool askedForHelp = false;

    public bool isAskingForHelp = false;

    public ClientGroup clientGroup;

    public float GetHappiness()
    {
        return happiness;
    }

    public void AddToHappiness(float toAdd)
    {
        happiness += toAdd;
        if (happiness > GMGlobalNumericVariables.gnv.MAX_CLIENT_HAPPINESS)
            happiness = GMGlobalNumericVariables.gnv.MAX_CLIENT_HAPPINESS;
        else if(happiness < GMGlobalNumericVariables.gnv.MIN_CLIENT_HAPPINESS)
            happiness = GMGlobalNumericVariables.gnv.MIN_CLIENT_HAPPINESS;
    }

    private ClientType SelectClientType()
    {
        //This number is always between 0 and 12 (included)
        float repMod = GMClubData.GetReputation() % 13;

        float randomValue = Random.Range(0,13);

        CrimeService cs = GMCrimeServiceData.unlockedCrimeServices.Find(x => x.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_SELECT_FEW_ID);
        if(cs != null && cs.isSubscribed)
        {
            randomValue *= GMGlobalNumericVariables.gnv.SELECT_FEW_QUALITY_MULTIPLIER;
        }

        //if()

        ClientType clientType;

        if(GMClubData.GetReputation() < 13)
            clientType = ClientType.VERY_POOR;
        else if (GMClubData.GetReputation() < 26)
            clientType = ClientType.POOR;
        else if (GMClubData.GetReputation() < 39)
            clientType = ClientType.BELOW_AVERAGE;
        else if (GMClubData.GetReputation() < 52)
            clientType = ClientType.AVERAGE;
        else if (GMClubData.GetReputation() < 65)
            clientType = ClientType.ABOVE_AVERAGE;
        else if (GMClubData.GetReputation() < 78)
            clientType = ClientType.RICH;
        else if (GMClubData.GetReputation() < 91)
            clientType = ClientType.VERY_RICH;
        else
            clientType = ClientType.MEGA_RICH;

        //We check if the client is a class above
        //The chance depends on the reputation, the higher it is (mod 13), the higher the chance
        if (randomValue <= repMod && clientType != ClientType.MEGA_RICH)
            clientType++;
        else
        {
            //If the client is not a rank above, test if he is a rank below, in the same way
            randomValue = Random.Range(0, 13);
            if (randomValue > repMod && clientType != ClientType.VERY_POOR)
                clientType--;
        }

        return clientType;
    }

    //Selects the group the client belongs to
    private ClientGroup SelectClientGroup()
    {
        CrimeService cs = GMCrimeServiceData.unlockedCrimeServices.Find(x => x.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_PATRONAGE_ID);
        int specialTypeTest;
        float random;
        if (cs != null && cs.isSubscribed)
        {
            //If the patronage service is subscribed to, the chances of being a criminal are higher 
            //and the chances of being a cop are lower

            //Chooses the type of special client he could be, 0 or 1  => criminal, 2 => police
            specialTypeTest = Random.Range(0, 3);
            random = Random.Range(0f, 1f);
            //If the client is in the criminal group, test if the random is correct
            if ((specialTypeTest == 0 || specialTypeTest == 1) && 
                (random * GMGlobalNumericVariables.gnv.PATRONAGE_CHANCE_MULTIPLIER) <= GMGlobalNumericVariables.gnv.CHANCE_CLIENT_IS_CRIMINAL)
                return ClientGroup.CRIMINAL;

            else if (specialTypeTest == 2 && random <= GMGlobalNumericVariables.gnv.CHANCE_CLIENT_IS_POLICE)
                return ClientGroup.POLICE;

            else return ClientGroup.CIVILIAN;
        }
        //Chooses the type of special client he could be, 0 => criminal, 1 => police
        specialTypeTest = Random.Range(0, 2);
        random = Random.Range(0f, 1f);
        //If the client is in the criminal group, test if the random is correct
        if (specialTypeTest == 0 && random <= GMGlobalNumericVariables.gnv.CHANCE_CLIENT_IS_CRIMINAL 
            && StaticFunctions.IsCrimeOfficeAvailable())
            return ClientGroup.CRIMINAL;

        else if (specialTypeTest == 1 && random <= GMGlobalNumericVariables.gnv.CHANCE_CLIENT_IS_POLICE && false)
            return ClientGroup.POLICE;

        else return ClientGroup.CIVILIAN;

    }

    //Chooes the client favorite sex act
    //It depends on wich prestation room is built
    //If the group room is built then the random is between all possibilities
    //If not, test for the sex booth then the oral booth... until the stage is reached
    //It supposes that the stage always exists
    //We make a random from 1 to size to avoid the NONE element of Work enum
    private Work FavoriteSexAct()
    {
        
        if (GMImprovementsData.improvementsData.groupRoom && GMPoliciesData.policies.threesome)
        {
            return (Work)Random.Range(1, System.Enum.GetNames(typeof(Work)).Length);
        }
        else if (GMImprovementsData.improvementsData.sexBooth && GMPoliciesData.policies.facingVaginal)
            {
                return (Work)Random.Range(1, System.Enum.GetNames(typeof(Work)).Length - 1);
            }
        else if (GMImprovementsData.improvementsData.oralBooth && GMPoliciesData.policies.blowjob)
        {
            return (Work)Random.Range(1, System.Enum.GetNames(typeof(Work)).Length - 2);
        }
        else if (GMImprovementsData.improvementsData.foreplayBooth && GMPoliciesData.policies.handjob)
        {
            return (Work)Random.Range(1, System.Enum.GetNames(typeof(Work)).Length - 3);
        }
        else if (GMImprovementsData.improvementsData.photostudio && GMPoliciesData.policies.poseNaked)
        {
            return (Work)Random.Range(1, System.Enum.GetNames(typeof(Work)).Length - 4);
        }
        else 
        {
            return Work.DANCE;
        }
        
    }

    //This constructor generates a random client with a given booth number
    public Client(int boothNumber)
    {
        clientType = SelectClientType();

        clientGroup = SelectClientGroup();

        favoritePhysicalTraits = new FavoritePhysicalTraits();


        if (NewPlanningBehaviors.boothsSpecialties[boothNumber] == Work.NONE)
        {
            favoriteSexAct = FavoriteSexAct();
        }
        else
        {
            favoriteSexAct = NewPlanningBehaviors.boothsSpecialties[boothNumber];
        }

        favoriteFinisher = (Finisher)Random.Range(0, System.Enum.GetNames(typeof(Finisher)).Length);
  
        this.boothNumber = boothNumber;

        this.happiness = Random.Range(GMGlobalNumericVariables.gnv.CLIENT_CREATION_HAPPINESS_MIN, GMGlobalNumericVariables.gnv.CLIENT_CREATION_HAPPINESS_MAX);

    }

    public string ClientTypeAsString()
    {
        return StaticFunctions.ToLowerCaseExceptFirst(clientType.ToString());
        /*
        switch (clientType)
        {
            case ClientType.VERY_POOR: return "$";
            case ClientType.POOR: return "$$";
            case ClientType.BELOW_AVERAGE: return "$$$";
            case ClientType.AVERAGE: return "$$$$";
            case ClientType.ABOVE_AVERAGE: return "$$$$$";
            case ClientType.RICH: return "$$$$$$";
            case ClientType.VERY_RICH: return "$$$$$$$";
            case ClientType.MEGA_RICH: return "$$$$$$$$";
            default: return "";
        }
        */
    }

}
