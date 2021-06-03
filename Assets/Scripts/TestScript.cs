using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class Test {
    public int testInt;
    public string testString;
    public float testFloat;
    public BustType bust;

    public static Test CreateFromJSON(string Json)
    {
        return JsonUtility.FromJson<Test>(Json);
    }
}

public class TestScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        Test t = Test.CreateFromJSON(StaticFunctions.ReadJSONString("Assets/Girls/Kendra Sunderland/texts/ExampleText.txt"));
        //Test t = Test.CreateFromJSON("{\"testInt\":5,\"testString\":\"test\",\"testFloat\":1.45}");
        Debug.Log("testInt : " + t.testInt+ "\n" + "testString : " + t.testString + "\n" + "testFloat : " + t.testFloat
            + "\n" + "bust : " + t.bust);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
