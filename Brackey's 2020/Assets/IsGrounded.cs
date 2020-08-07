using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool OnGround = false;

    public bool CollidingWithGround = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered Collision");
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Explosion")
        {
            OnGround = true;
            Debug.LogWarning("Entered Collision");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered Collision");
        if (collision.tag == "Ground" || collision.tag == "Explosion")
        {
            OnGround = true;

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Explosion")
        {
            CollidingWithGround = false;
            OnGround = false;

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Entered Collision");
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Explosion")
        {
            OnGround = false;
            Debug.Log("Exit Collision");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Explosion")
        {
            CollidingWithGround = true;
        }
    }
}
