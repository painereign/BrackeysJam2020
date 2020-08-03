using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggers : MonoBehaviour
{
    public static GameTriggers Instance { get; set; }

    public Dictionary<string, bool> GameTriggersDict = new Dictionary<string, bool>();

    public bool BatsTriggered;

    public bool HelmetDowngraded;
    public bool ChestDowngraded;
    public bool ArmGunDowngraded;
    public bool MissleDowngraded;
    public bool LegsDowngraded;
    public bool AntiGravBootsDowngraded;

    private void Start()
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

}
