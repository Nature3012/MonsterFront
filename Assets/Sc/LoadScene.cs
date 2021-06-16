using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Image feadImage;
    [SerializeField] private float feadSpeed = 0.2f;
    private float red, green, blue, alfa;
    bool isFead = false;
    
    void Start()
    {
        alfa = feadImage.color.a;
        feadImage.enabled = false;
    }

    void Update()
    {
        if (isFead)
        {
            FeadOut();
        }
    }

    public void LoadScenes() 
    {
        SceneManager.LoadScene(sceneName);
    }

    void FeadOut() 
    {
        alfa += feadSpeed;
        SetAlpha();
        if (alfa >= 1.0f)
        {
            LoadScenes();
            isFead = false;
        }
    }

    void SetAlpha()
    {
        feadImage.color = new Color(red, green, blue, alfa);
    }

    public void Fead() 
    {
        feadImage.enabled = true;
        isFead = true;
    }
}
