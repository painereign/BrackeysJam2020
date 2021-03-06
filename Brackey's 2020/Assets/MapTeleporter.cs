﻿using System.Collections;
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
    private float NewLeftX = 6.9f;
    private float NewRightX = -6.2f;

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
            GameObject go = GameObject.Instantiate(NewMap);
            go.SetActive(false);
            go.SetActive(true);
            TextBoxController.Instance.MapChange();
            PlayerController.Instance.MapChange();
            //NewMap.SetActive(true);
            MapManager.Instance.CurrentTileMap = NewMap.GetComponentInChildren<MapTeleporter>().PlatformLayer;
            GameObject.Destroy(CurrentMap);
            //CurrentMap.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider Enter");
    }
}
