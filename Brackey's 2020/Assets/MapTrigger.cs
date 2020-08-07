using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    [Serializable]
    public struct RequiredTriggers
    {
        public string TriggerName;
        public bool RequiredValue;
    }

    [Serializable]
    public struct GameObjTriggers
    {
        public GameObject ReferenceObject;
        public bool EnabledValue;
    }

    public RequiredTriggers[] ReqTriggers;

    public RequiredTriggers TriggerChanged;

    public GameObjTriggers GOTriggers;

    public bool TextBox;
    public string Text;
    bool currentlyTriggered = false;

    float triggeredTime = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyTriggered)
        {
            triggeredTime -= Time.deltaTime;
            if (triggeredTime <= 0)
                currentlyTriggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");

        bool weGood = true;

        for (int x = 0; x < ReqTriggers.Length; x++) {
            if (GameTriggers.Instance.GameTriggersDict[ReqTriggers[x].TriggerName] != ReqTriggers[x].RequiredValue)
            {
                weGood = false;
            }
        }

        if (weGood)
        {
            ApplyChanges();
        }
        else
        {
            ApplyAntiChanges();
        }

        if (TextBox && !currentlyTriggered)
        {
            triggeredTime = 7.5f;
            currentlyTriggered = true;
            TextBoxController.Instance.NewTextBox(Text, 7.5f);
        }
    }

    void ApplyChanges()
    {
        if (TriggerChanged.TriggerName != "")
        {
            GameTriggers.Instance.GameTriggersDict[TriggerChanged.TriggerName] = TriggerChanged.RequiredValue;
        }
        if (GOTriggers.ReferenceObject != null)
        {
            GOTriggers.ReferenceObject.SetActive(GOTriggers.EnabledValue);
        }
        Debug.LogWarning("TRIGGERING EVENT");
    }
    void ApplyAntiChanges()
    {
        /*
        if (TriggerChanged.TriggerName != "")
        {
            GameTriggers.Instance.GameTriggersDict[TriggerChanged.TriggerName] = TriggerChanged.RequiredValue;
        }*/
        if (GOTriggers.ReferenceObject != null)
        {
            GOTriggers.ReferenceObject.SetActive(!GOTriggers.EnabledValue);
        }
    }
}
