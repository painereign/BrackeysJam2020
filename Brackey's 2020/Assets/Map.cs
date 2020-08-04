using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public MapTeleporter MapTeleUp, MapTeleDown, MapTeleLeft, MapTeleRight;

    public TilemapCollider2D JumpRemoveColliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (JumpRemoveColliders != null)
        {
            if (!PlayerController.Instance.IsGrounded.OnGround && !PlayerController.Instance.Falling)
            {
                JumpRemoveColliders.enabled = false;
            }
            else
            {
                JumpRemoveColliders.enabled = true;
            }
        }
    }
}
