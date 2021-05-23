using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject toMenu;
    [SerializeField] GameObject setumeigamenn = null;
    GameObject game { get; set; }
    RectTransform mainTransform;
    RectTransform toMenuTransform;
    RectTransform setumeigamennTransform;
    void Start()
    {
        mainTransform = main.GetComponent<RectTransform>();
        toMenuTransform = toMenu.GetComponent<RectTransform>();
        GameObject menu = setumeigamenn;
        if (menu)
        {
            game = menu;
            setumeigamennTransform = menu.GetComponent<RectTransform>();
        }
    }

    public void MainMenu() 
    {
        mainTransform.anchoredPosition = new Vector2(405, 0);
        toMenuTransform.anchoredPosition = new Vector2(-405, 0);
        setumeigamennTransform.anchoredPosition = new Vector2(5000, 0);
        if (setumeigamenn)
        {
            Debug.Log("ma");
            
        }
    }

    public void ToMenu()
    {
        mainTransform.anchoredPosition = new Vector2(-405, 0);
        toMenuTransform.anchoredPosition = new Vector3(405, 0, 0);
        if (setumeigamenn)
        {
            setumeigamennTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}
