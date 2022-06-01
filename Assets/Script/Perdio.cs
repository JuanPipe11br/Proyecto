using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Perdio : MonoBehaviour
{
    Button myBtnmenu;

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        myBtnmenu = root.Q<Button>("btnmenu");
        myBtnmenu.clicked += menux;
    }

    // Update is called once per frame

    void menux()
    {
        SceneManager.LoadScene("menu");

    }
}
