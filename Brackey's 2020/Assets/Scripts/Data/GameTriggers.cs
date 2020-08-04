using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggers : MonoBehaviour
{
    public static GameTriggers Instance { get; set; }

    public Dictionary<string, bool> GameTriggersDict = new Dictionary<string, bool>();


    public bool debugEnabled = false;


    public bool BatsTriggered;

    public bool HelmetDowngraded;
    public bool ChestDowngraded;
    public bool ArmGunDowngraded;
    public bool MissleDowngraded;
    public bool LegsDowngraded;
    public bool AntiGravBootsDowngraded;

    public GameObject Flashlight;

    private void Awake()
    {
        Instance = this;
        GenerateAllTriggers();
    }

    void GenerateAllTriggers()
    {
        //world events
        GameTriggersDict.Add("BatsTriggered", false);



        //upgrades
        GameTriggersDict.Add("HelmetDowngraded", false);
        GameTriggersDict.Add("ChestDowngraded", false);
        GameTriggersDict.Add("ArmGunDowngraded", false);
        GameTriggersDict.Add("MissleDowngraded", false);
        GameTriggersDict.Add("LegsDowngraded", false);
        GameTriggersDict.Add("AntiGravBootsDowngraded", false);



        //collectable upgrades
    }

    void Update()
    {
        CheckTriggers();
        if (debugEnabled)
        {
            DebugCheck();
        }
    }

    public void CheckTriggers()
    {
        bool val = GameTriggersDict["HelmetDowngraded"];
        val = !val;
        Flashlight.SetActive(val);
    }

    public void DebugCheck()
    {
        GameTriggersDict["HelmetDowngraded"] = HelmetDowngraded;
    }

}
