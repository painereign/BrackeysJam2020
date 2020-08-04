using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string CollectableTrigger;
    public string ObjectText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameTriggers.Instance.GameTriggersDict[CollectableTrigger] = true;

            TextBoxController.Instance.NewTextBox(ObjectText);

            this.gameObject.SetActive(false);
        }
    }
}
