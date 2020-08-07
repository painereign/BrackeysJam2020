using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public float HorizontalMoveAdjustMultiple = 0.5f;
    bool collisionThisFrame = false;

    public float PlayerVertHightModPerFrame = 1f;

    public float PlayerJumpMod = 0.5f;

    public bool isWater = false;

    public bool InTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!InTrigger)
        {
            PlayerController.Instance.HoriMoveAjust(1f);
            PlayerController.Instance.HeighModPerFrameRESET(1f, isWater);
        }
        //Debug.Log(InTrigger);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InTrigger = true;
            if (!isWater && !GameTriggers.Instance.GameTriggersDict["LegsDowngraded"]) 
                TextBoxController.Instance.NewTextBox("Hold SHIFT to SPRINT to escape quicksand!", 5f);
            if (isWater)
            {
                if (!GameTriggers.Instance.GameTriggersDict["ChestDowngraded"])
                    PlayerController.Instance.HeighModPerFrame(1f, isWater, PlayerJumpMod);
                else
                    PlayerController.Instance.HeighModPerFrame(PlayerVertHightModPerFrame, isWater, PlayerJumpMod);
            }
            else
            {
                PlayerController.Instance.HeighModPerFrame(PlayerVertHightModPerFrame, isWater, PlayerJumpMod);
            }
            Debug.Log("colliding!!!!");
            collisionThisFrame = true;
            PlayerController.Instance.HoriMoveAjust(HorizontalMoveAdjustMultiple);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InTrigger = false;
            Debug.Log("NOT colliding!!!!");
            collisionThisFrame = false;
            PlayerController.Instance.HoriMoveAjust(1f);
            PlayerController.Instance.HeighModPerFrame(1f, isWater, 1f);
            PlayerController.Instance.rb.gravityScale = 1f;
            PlayerController.Instance.inMod = false;
            PlayerController.Instance.modIsWater = false;
            PlayerController.Instance.PlayerJumpMod = 1f;
        }
    }

}
