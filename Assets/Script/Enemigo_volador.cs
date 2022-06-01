using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemigo_volador : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] GameObject player;
    AIPath myPath;
    Animator myAnim2;

    /// cont enemigos
    private int contenemigos=5;
    int contadormuertos = 5;
    
    [SerializeField] private GameObject gano;
    Button myBtnmenu1;

    /// ///Gano
    public void Ganox()
    {
        
        Time.timeScale = 0f;
        gano.SetActive(true);

    }
    
    /// menu
    void menux()
    {
        SceneManager.LoadScene("menu");

    }
    



    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
        myAnim2 = GetComponent<Animator>();
        //menu
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        myBtnmenu1 = root.Q<Button>("btnmenu");
        myBtnmenu1.clicked += menux;

    }

    IEnumerator MiCorutina3()
    {
        contadormuertos--;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        //cont enemigos
        if (contenemigos == 0 || contadormuertos==0)
        {
            Ganox();
        }




    }

    // Update is called once per frame
    void Update()
    {
        
        



        Collider2D col = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));

        if (col != null)
        {
            myPath.isStopped = false;
        }
        else
            myPath.isStopped = true;



    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando : " + collision.gameObject.name);
        if (collision.gameObject.name == "Bullet(Clone)")
        {

            life = life - 1;

        }
        if (life == 0)
        {
            myAnim2.SetBool("isMuerte", true);
            StartCoroutine(MiCorutina3());
            Debug.Log(contenemigos+"hola");
            contenemigos--;

        }
    }





}
