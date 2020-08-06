using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    public Tilemap CurrentTileMap;

    public static MapManager Instance { get; set; }
    public List<GameObject> AllMaps;

    public List<GameObject> MapPrefabs;

    private List<List<GameObject>> SortedMaps = new List<List<GameObject>>();



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SortMaps();
        SetTeleports();

        GetActiveTileMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SortMaps()
    {
        for (int a = 0; a < 13; a++)
        {
            SortedMaps.Add(new List<GameObject>());
            for (int b = 0; b < 13; b++)
            {
                SortedMaps[a].Add(null);
            }
        }
        
        /*
        for (int x =0; x < AllMaps.Count; x++)
        {
            int val = LetterToVal(AllMaps[x].name.ToCharArray()[0]);
            //int num = Int.TruAllMaps[x].name.Substring(1,1);
            //int num = AllMaps[x].name.ToCharArray()[1];
            int num = -1;
            int.TryParse(AllMaps[x].name.Substring(1, 1), out num);
            //SortedMaps[val][num] = AllMaps[x];
            SortedMaps[num][val] = AllMaps[x];
        }*/

        for (int x = 0; x < MapPrefabs.Count; x++)
        {
            int val = LetterToVal(MapPrefabs[x].name.ToCharArray()[0]);
            //int num = Int.TruAllMaps[x].name.Substring(1,1);
            //int num = AllMaps[x].name.ToCharArray()[1];
            int num = -1;
            int.TryParse(MapPrefabs[x].name.Substring(1, 1), out num);
            //SortedMaps[val][num] = AllMaps[x];
            SortedMaps[num][val] = MapPrefabs[x];
        }
    }

    public int LetterToVal(char c)
    {
        return ((int)(char.ToUpper(c)) - 65);
    }

    public void SetTeleports()
    {
        for (int x = 0; x < SortedMaps.Count; x++)
        {
            for (int y = 0; y < SortedMaps[x].Count; y++)
            {
                if (SortedMaps[x][y] == null)
                    continue;
                if (y > 0)
                {
                    SortedMaps[x][y].GetComponent<Map>().MapTeleLeft.NewMap = SortedMaps[x][y - 1];
                }
                if (y < SortedMaps[x].Count -1)
                {
                    SortedMaps[x][y].GetComponent<Map>().MapTeleRight.NewMap = SortedMaps[x][y + 1];
                }
                if (x > 0)
                {
                    SortedMaps[x][y].GetComponent<Map>().MapTeleUp.NewMap = SortedMaps[x-1][y];
                }
                if (x < SortedMaps.Count - 1)
                {
                    SortedMaps[x][y].GetComponent<Map>().MapTeleDown.NewMap = SortedMaps[x + 1][y];
                }

                //Debug.Log(SortedMaps[x][y].name);
            }
        }
    }

    public void GetActiveTileMap()
    {
        /*
        for (int x = 0; x < AllMaps.Count; x++)
        {
            if (AllMaps[x].activeInHierarchy)
            {
                CurrentTileMap = AllMaps[x].GetComponentInChildren<MapTeleporter>().PlatformLayer;
            }
        }*/
        for (int x = 0; x < MapPrefabs.Count; x++)
        {
            if (MapPrefabs[x].activeInHierarchy)
            {
                CurrentTileMap = MapPrefabs[x].GetComponentInChildren<MapTeleporter>().PlatformLayer;
                CurrentTileMap.gameObject.GetComponent<TilemapRenderer>().sortingLayerID = 1;
            }
        }
    }

}
