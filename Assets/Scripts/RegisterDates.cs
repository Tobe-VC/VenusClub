using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterDates : MonoBehaviour
{
    public static List<DateData> dates = new List<DateData>();

    public static DateData currentDate = null;

    public static void CreateDates()
    {
        if (dates.Count <= 0)
        {
            foreach (string s in StaticStrings.DATES_DIRECTORIES_NAMES)
            {
                DateData date = DateData.CreateFromJSON(Resources.Load<TextAsset>(StaticStrings.DATES_DIRECTORY + s +
                    StaticStrings.DATE_JSON_FILE_NO_EXTENSION).text);

                dates.Add(date);
            }
        }
            
    }
}
