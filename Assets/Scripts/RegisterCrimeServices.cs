using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterCrimeServices : MonoBehaviour
{
    // Start is called before the first frame update
    public static void CreateCrimeServices()
    {
        if (GMCrimeServiceData.crimeServices == null || GMCrimeServiceData.crimeServices.Count <= 0)
        {
            //Create the list of services only if it has not been done yet
            foreach (string s in StaticStrings.CRIME_SERVICES_DIRECTORIES_NAMES)
            {
                CrimeService cs = new CrimeService(s);
                GMCrimeServiceData.crimeServices.Add(cs);
                if(cs.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_LOTTERY_ID)
                {
                    cs.effectOnUnlock = delegate () 
                    {
                        List<GirlClass> girlsInLottery = new List<GirlClass>();
                        foreach (GirlClass girl in GameMasterGlobalData.girlsList)
                        {
                            if (girl.inLottery)
                            {
                                bool isRecruited = false;
                                foreach (GirlClass g in GMRecruitmentData.recruitedGirlsList)
                                {
                                    if (girl.name.Equals(g.name))
                                    {
                                        isRecruited = true;
                                    }
                                }
                                if (!isRecruited)
                                {
                                    girlsInLottery.Add(girl);
                                }
                            }
                        }
                        if (girlsInLottery.Count > 0)
                        {
                            GirlClass tmpGirl = girlsInLottery[Random.Range(0, girlsInLottery.Count)];
                            GMRecruitmentData.recruitedGirlsList.Add(tmpGirl);
                            CrimeServiceBehavior.girlJustRecruited = tmpGirl;
                        }
                        cs.isUnlocked = false;
                    };
                }
                
            }
        }
    }

}
