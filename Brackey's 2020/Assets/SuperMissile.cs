using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMissile : MonoBehaviour
{
    public float XDir = 1f;

    public bool FacingRight = true;

    public float speed = 8f;

    public float AliveTime = 5f;

    public int Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FacingRight)
            XDir = 1;
        else
            XDir = -1;
        transform.position += Vector3.right * speed * XDir * Time.deltaTime;

        AliveTime -= Time.deltaTime;

        if (AliveTime <= 0)
            GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") {
            //Debug.Log("bullet trigger enter");
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().Hit(Damage);
                GameObject.Destroy(this.gameObject);
            }

            else if (collision.tag == "Ground")
            {
                //check if it is one of the glass tiles
                //if yes, KILL THEM ALL
            }

            else if (collision.tag == "Explosion")
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.LogWarning("bullet collision enter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit trigger of " + collision.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("In trigger of " + collision.tag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exit collision of " + collision.collider.tag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("In collision of " + collision.collider.tag);
    }
}
