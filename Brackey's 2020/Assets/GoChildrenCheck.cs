using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoChildrenCheck : MonoBehaviour
{
    public string Trigger;
    public bool Value;

    public GameObject Holder;

    bool checkDone = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameTriggers.Instance.GameTriggersDict[Trigger] == Value)
        {
            Holder.SetActive(true);
        }
        else
        {
            Holder.SetActive(false);
        }
        checkDone = true;
    }

    private void Update()
    {
        if (!checkDone)
        { OnEnable(); }
    }
}
