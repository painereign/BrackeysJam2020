using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public int Damage;

    public float pushBackSpeed;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Trigger entered");
            PlayerController.Instance.TakeDamage(Damage);
            //PlayerController.Instance.rb.velocity = -PlayerController.Instance.rb.velocity.normalized * pushBackSpeed;

            PlayerController.Instance.rb.velocity *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("collision entered");
            PlayerController.Instance.TakeDamage(Damage);
            //PlayerController.Instance.rb.velocity = -PlayerController.Instance.rb.velocity.normalized * pushBackSpeed;
            /*
            PlayerController.Instance.rb.velocity = new Vector2(-PlayerController.Instance.rb.velocity.x * pushBackSpeed,
                PlayerController.Instance.rb.velocity.y);*/
            PlayerController.Instance.rb.AddForce(new Vector2(-PlayerController.Instance.rb.velocity.normalized.x * pushBackSpeed, PlayerController.Instance.rb.velocity.y));
        }
    }

}
