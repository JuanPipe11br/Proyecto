using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Enemigo_terrestre : MonoBehaviour

{
    [SerializeField] GameObject player;
    Animator myAnim;
    [SerializeField] int life;
 
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject sonBullet;
    [SerializeField] float firefate;
    private float nextfire = 0f;
    private float time = 0;
    Button myBtnmenu;
    [SerializeField] private GameObject gano;
    [SerializeField] AudioClip sfx_muerteenemigo;


    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        ////
        ///
        //menu
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        myBtnmenu = root.Q<Button>("btnmenu");
        myBtnmenu.clicked += menux;
    }


    //menu
    void menux()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1f;
    }
    public void Ganox()
    {

        Time.timeScale = 0f;
        gano.SetActive(true);

    }
    IEnumerator MiCorutina()
    {
        myAnim.SetBool("isMuerte", true);
        AudioSource.PlayClipAtPoint(sfx_muerteenemigo, Camera.main.transform.position);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        
        Ganox();

    }

    // Update is called once per frame
    void Update()
    {
        //Alternativa 1 vector2,distance
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        // Debug.Log("distancia del jugador: " + distancia);
        Debug.DrawLine(transform.position, player.transform.position, Color.red);

        if(distancia <= 9 && Time.time > nextfire)
        {
            myAnim.SetBool("isDisparar", true);
            Instantiate(sonBullet);
            nextfire = Time.time + firefate;
            myAnim.SetLayerWeight(1, 1);
           
            Instantiate(bullet, FirePoint.position, FirePoint.rotation);
            
        }
        if(distancia> 9)
        {
            myAnim.SetBool("isDisparar", false);
        }

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
            
            StartCoroutine(MiCorutina());

        }
    }


}
