using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using UnityEngine.UIElements;

public class Player1 : MonoBehaviour

{

    ///Enemigos
    [SerializeField] int life;
    [SerializeField] GameObject player;
    AIPath myPath;
    Animator myAnim2;
    public GameObject[] enemies;
    public float timeSpawn = 1;
    public float repetir = 3;

    public Transform salida;
    /// cont enemigos
    private int contenemigos;
    int contadormuertos;



    ///jugador
    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject sonBullet;

    [SerializeField] GameObject sonsalto;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_muerte;
    Rigidbody2D mybody;
    Animator myAnim;
    bool isGrounded = true;
    [SerializeField] float firefate;
    private float nextfire = 0f;
    private float time = 0;
    private bool p = false;
    private bool g = false;

    //menu
    [SerializeField] private GameObject perdio;
    Button myBtnmenu;
    [SerializeField] private GameObject gano;
    Button myBtnmenu1;




    /// ///perdio
    public void Perdiox()
    {

        Time.timeScale = 0f;
        perdio.SetActive(true);

    }
    /// ///Gano
    public void Ganox()
    {

        Time.timeScale = 0f;
        gano.SetActive(true);

    }
    public void Spawnenemies()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnPosition = new Vector3(salida.position.x, salida.position.y);
        GameObject enemigo = Instantiate(enemies[0], spawnPosition, gameObject.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        ///Enemigo
        myPath = GetComponent<AIPath>();
        myAnim2 = GetComponent<Animator>();
        InvokeRepeating("Spawnenemies", timeSpawn, repetir);

        ///Player
        mybody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //StartCoroutine(MiCorutina());

        //menu
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        myBtnmenu = root.Q<Button>("btnmenu");
        myBtnmenu.clicked += menux;

    }


    //menu
    void menux()
    {
        SceneManager.LoadScene("menu");

    }
    IEnumerator MiCorutina1()
    {
        AudioSource.PlayClipAtPoint(sfx_muerte, Camera.main.transform.position);

        myAnim.SetBool("isMuerte", true);
        yield return new WaitForSeconds(1);
        Perdiox();






    }


    IEnumerator MiCorutina()
    {

        yield return new WaitForSeconds(1);
        myAnim.SetLayerWeight(1, 0);



    }


    //enemigo
    IEnumerator MiCorutina3()
    {
        contadormuertos++;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        //cont enemigos
        if (contenemigos >= 6 || contadormuertos >= 6)
        {
            Ganox();
        }




    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));


        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        isGrounded = (ray.collider != null);

        Jump();
        Fire();


        ///enemigo

        Collider2D col = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));

        if (col != null)
        {
            myPath.isStopped = false;
        }
        else
            myPath.isStopped = true;

    }

    ///enemigo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    ///

    void TerminarCorrer()
    {
        Debug.Log("Esta terminando de correr");
    }
    IEnumerator Sonsalto()
    {
        Instantiate(sonsalto);
        yield return new WaitForSeconds(1);
        Destroy(sonsalto);

    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time > nextfire)
        {
            Instantiate(sonBullet);
            nextfire = Time.time + firefate;
            myAnim.SetLayerWeight(1, 1);
            Instantiate(bullet, FirePoint.position, FirePoint.rotation);
            StartCoroutine(MiCorutina());

        }

    }

    void Jump()
    {
        if (isGrounded && !myAnim.GetBool("isJumping"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mybody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnim.SetBool("isJumping", true);
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);


            }
        }
        if (mybody.velocity.y != 0 && !isGrounded)
            myAnim.SetBool("isJumping", true);
        else
            myAnim.SetBool("isJumping", false);


    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");

        if (dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
            if (dirH < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.localScale = new Vector2(-1, 1);


            }
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector2(1, 1);


        }
        else
            myAnim.SetBool("isRunning", false);

        mybody.velocity = new Vector2(dirH * speed, mybody.velocity.y);

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
            contenemigos = contenemigos + 1;
            StartCoroutine(MiCorutina3());
            Debug.Log(contenemigos + "hola");



        }


        /////////////////////////////
        Debug.Log("Colisionando : " + collision.gameObject.name);
        if (collision.gameObject.name == "enemy2_0" ||
            collision.gameObject.name == "enemy2_0 (1)"
            ||
            collision.gameObject.name == "enemy2_0 (2)"
            ||
            collision.gameObject.name == "enemy2_0 (3)" ||
            collision.gameObject.name == "enemy2_0 (4)" ||
            collision.gameObject.name == "enemy2_0 (5)" ||
            collision.gameObject.name == "enemy2_0 (6)" ||
            collision.gameObject.name == "enemy2_0 (7)" ||
            collision.gameObject.name == "enemy2_0 (8)" ||
            collision.gameObject.name == "enemy2_0(Clone)"
            || collision.gameObject.name == "bullet_enemigo(Clone)"
            || collision.gameObject.name == "bullet_enemigo(Clone)(1)"
            || collision.gameObject.name == "bullet_enemigo(Clone)(2)"
            || collision.gameObject.name == "bullet_enemigo(Clone)(3)")
        {
            StartCoroutine(MiCorutina1());




        }


    }



}
