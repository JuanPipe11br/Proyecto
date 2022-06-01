using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UIElements;

public class player1 : MonoBehaviour

{

    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject sonBullet;
   
    [SerializeField] GameObject sonsalto;
    [SerializeField] AudioClip sfx_jump;
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
    ///cont enemigos
    public int contenemigos;
    int contadormuertos = 0;



    /// ///perdio
    public void Perdiox()
    {

        Time.timeScale = 0f;
        perdio.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
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

       
        myAnim.SetBool("isMuerte", true);
        yield return new WaitForSeconds(1);
        Perdiox();






    }


    IEnumerator MiCorutina()
    {

        yield return new WaitForSeconds(1);
        myAnim.SetLayerWeight(1, 0);



    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));


        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        isGrounded = (ray.collider != null);

        Jump();
        Fire();
    }

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
