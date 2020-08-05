using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public float HorizontalMoveAdjustMultiple = 0.5f;
    bool collisionThisFrame = false;

    public float PlayerVertHightModPerFrame = 0.01f;

    public bool isWater = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!collisionThisFrame)
        {
            PlayerController.Instance.HoriMoveAjust(1f);
            PlayerController.Instance.HeighModPerFrame(1f, isWater);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PlayerVertHightModPerFrame > 0f) 
                TextBoxController.Instance.NewTextBox("Hold SHIFT to SPRINT to escape quicksand!", 5f);
            if (isWater)
            {
                if (GameTriggers.Instance.GameTriggersDict["ChestDowngraded"])
                    PlayerController.Instance.HeighModPerFrame(1f, isWater);
                else
                    PlayerController.Instance.HeighModPerFrame(PlayerVertHightModPerFrame, isWater);
            }
            else
            {
                PlayerController.Instance.HeighModPerFrame(PlayerVertHightModPerFrame, isWater);
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
            Debug.Log("NOT colliding!!!!");
            collisionThisFrame = false;
            PlayerController.Instance.HoriMoveAjust(1f);
            PlayerController.Instance.HeighModPerFrame(1f, isWater);
        }
    }
}
