using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; set;}

    public GameObject TmpObjHolder;

    public string startingMap;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        for (int x = 0; x < MapManager.Instance.MapPrefabs.Count; x++)
        {
            if (MapManager.Instance.MapPrefabs[x].name == startingMap)
            {
                GameObject.Instantiate(MapManager.Instance.MapPrefabs[x]);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
