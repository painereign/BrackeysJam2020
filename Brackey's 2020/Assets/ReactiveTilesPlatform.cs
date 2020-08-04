using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTilesPlatform : MonoBehaviour
{
    public string GameTrigger;
    public bool gameTriggerValue;

    public List<Animator> AllTileAnimators;

    public string AnimatorParam;

    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        for (int x = 0; x < AllTileAnimators.Count; x++)
        {
            AllTileAnimators[x].SetBool(AnimatorParam, !GameTriggers.Instance.GameTriggersDict[GameTrigger]);
        }
    }
}
