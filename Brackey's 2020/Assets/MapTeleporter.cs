using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTeleporter : MonoBehaviour
{
    public BoxCollider2D Collider;

    public GameObject CurrentMap;

    public GameObject NewMap;

    public Tilemap PlatformLayer;
    

    private float NewUpY = -4.45f;
    private float NewDownY = 4.5f;
    private float NewLeftX = 7.5f;
    private float NewRightX = -7.2f;

    // Start is called before the first frame update
    void Start()
    {
        Collider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //also keep momentum - not sure if that is something I need to adjust here or not?
        if (collision.tag == "Player" && NewMap != null)
        {
            Debug.Log("TriggerEnter");
            switch (this.gameObject.name.Substring(this.gameObject.name.Length-1, 1))
            {
                case "U":
                    PlayerController.Instance.transform.position = new Vector2(PlayerController.Instance.transform.position.x, NewUpY);
                    break;
                case "D":
                    PlayerController.Instance.transform.position = new Vector2(PlayerController.Instance.transform.position.x, NewDownY);
                    break;
                case "L":
                    PlayerController.Instance.transform.position = new Vector2(NewLeftX, PlayerController.Instance.transform.position.y);
                    break;
                case "R":
                    PlayerController.Instance.transform.position = new Vector2(NewRightX, PlayerController.Instance.transform.position.y);
                    break;
            }
            NewMap.SetActive(true);
            MapManager.Instance.CurrentTileMap = PlatformLayer;
            CurrentMap.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider Enter");
    }
}
