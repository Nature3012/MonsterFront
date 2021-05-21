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
    RectTransform rectTransform;

    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        rectTransform = gameObjects.GetComponent<RectTransform>();

        //var entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerEnter;

        //// リスナーは単純にLogを出力するだけの処理にする
        //entry.callback.AddListener((data) => { Sprite a = GetComponent<Image>().sprite; 
        //    GetComponent<Image>().sprite = image;
        //    image = a;
        //    Debug.Log("in");
        //});

        //var entry2 = new EventTrigger.Entry();
        //entry2.eventID = EventTriggerType.PointerExit;
        //entry2.callback.AddListener((date) => {
        //    Sprite a = GetComponent<Image>().sprite;
        //    GetComponent<Image>().sprite = image;
        //    image = a;
        //    Debug.Log("ex");
        //});

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
