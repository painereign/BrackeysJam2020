using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{

    public Tilemap tm;


    public int Health = 5;

    public AIRoutine AI;

    public float Speed = 3f;

    public float Dir = 1f;

    Rigidbody2D rb;
    PolygonCollider2D pc;

    public CircleCollider2D FootL, FootR;

    public enum AIRoutine
    {
        Patrol,
        Stationary,
        DropDown
    }

    public EnemyViewingTrigger ViewingTrigger;

    public float DropDownTime = 0.33f;
    public bool dropping = false;
    public float dropSpeed = 1f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (AI)
        {
            case AIRoutine.Patrol:
                //walk forward
                rb.velocity = new Vector2(Dir * Speed, rb.velocity.y);
                //if edge of platform, turn around
                if (Dir > 0)
                {
                    CheckFoot(FootR);
                }
                else
                {
                    CheckFoot(FootL);
                }
                break;
            case AIRoutine.Stationary:
                break;
            case AIRoutine.DropDown:
                if (dropping)
                {
                    this.transform.position += Vector3.down * Time.deltaTime * dropSpeed;
                }
                else
                {
                    if (ViewingTrigger.CurrentlyTriggered)
                    {
                        DropDownTime -= Time.deltaTime;
                        if (DropDownTime <= 0)
                        {
                            DropDown();
                        }
                    }
                }
                break;
           }
    }

    public void CheckFoot(CircleCollider2D cc2)
    {
        ContactFilter2D cf = new ContactFilter2D();

        Collider2D[] colliders = new Collider2D[10];
        cc2.OverlapCollider(cf, colliders);
       if (colliders[1] == null)
        {
            Flip();
        }

    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Enemy collision");
        if (dropping)
        {
            if (collision.collider.tag == "Ground")
            {
                Vector3 hitPos = Vector3.zero;
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPos.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPos.y = hit.point.y - 0.01f * hit.normal.y;

                    //hitPos.x = (int)hitPos.x;
                    //hitPos.y = (int)hitPos.y;
                    //TileBase tb = MapManager.Instance.CurrentTileMap.GetTile(MapManager.Instance.CurrentTileMap.WorldToCell(hitPos));
                    //TileData td;
                    //tb.GetTileData(MapManager.Instance.CurrentTileMap.WorldToCell(hitPos), MapManager.Instance.CurrentTileMap.tilem
                    // MapManager.Instance.CurrentTileMap, out td);
                    Sprite s = MapManager.Instance.CurrentTileMap.GetSprite(MapManager.Instance.CurrentTileMap.WorldToCell(hitPos));
                    
                    if (s != null && s.name == "environment_Tiles_16")
                    {
                        MapManager.Instance.CurrentTileMap.SetTile(MapManager.Instance.CurrentTileMap.WorldToCell(hitPos), null);
                    }

                    GameObject.Destroy(this.gameObject);
                }
                //Debug.Log("Drop collision hit ground");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enemy trigger");
        if (collision.GetComponent<MapTeleporter>() != null)
        {
            Flip();
        }
    }

    public void Flip()
    {
        Dir = Dir * -1;
    }

    public void DropDown()
    {
        dropping = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Drop collision exit");
    }
}
