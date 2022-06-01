using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class UIControler : MonoBehaviour
{
    Button myBtn;
    

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root= GetComponent<UIDocument>().rootVisualElement;
        myBtn = root.Q<Button>("btnstart");
        myBtn.clicked += startgame;


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void startgame()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1f;

    }
}
