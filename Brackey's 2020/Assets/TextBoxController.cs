using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public static TextBoxController Instance { get; set; }

    public GameObject TextBoxHolder;

    public bool Active = false;

    public float minActiveTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (minActiveTimer > 0)
            {
                minActiveTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.anyKey)
                {
                    ResetBox();
                }
            }
        }
    }

    public void ResetBox()
    {
        Active = false;
        minActiveTimer = 1;
        TextBoxHolder.SetActive(false);
    }

    public void NewTextBox(string text)
    {
        TextBoxHolder.GetComponentInChildren<Text>().text = text;
        Active = true;
        TextBoxHolder.SetActive(true);
    }

    public void NewTextBox(string text, float timer)
    {
        TextBoxHolder.GetComponentInChildren<Text>().text = text;
        minActiveTimer = timer;
        Active = true;
        TextBoxHolder.SetActive(true);
    }
}
