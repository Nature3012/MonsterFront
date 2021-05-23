using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    EventTrigger eventTrigger = null;
    [SerializeField] Sprite image = null;
    [SerializeField] GameObject gameObjects = null;
    [SerializeField] RectTransform rectTransform;

    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        if (gameObjects != null)
        {
            rectTransform = gameObjects.GetComponent<RectTransform>();
        }
    }

    public void In()
    {
        Sprite a = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = image;
        image = a;
        rectTransform.anchoredPosition = new Vector3(0,0,0);
    }

    public void Out() 
    {
        Sprite a = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = image;
        image = a;
        rectTransform.anchoredPosition = new Vector3(5000, 0, 0);
    }
}
