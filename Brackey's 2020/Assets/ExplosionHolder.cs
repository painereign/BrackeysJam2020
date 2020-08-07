using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHolder : MonoBehaviour
{
    public List<GameObject> DisableOnNormalMissile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalMissileHit()
    {
        for (int x =0; x < DisableOnNormalMissile.Count; x++)
        {
            DisableOnNormalMissile[x].SetActive(false);
        }
    }
}
