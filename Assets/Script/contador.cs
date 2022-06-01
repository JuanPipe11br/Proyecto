using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class contador : MonoBehaviour
{

    //Ambos
    [SerializeField] GameObject player;
    Animator myAnim;
    [SerializeField] int life;
    //Enemigo terreste
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject sonBullet;
    [SerializeField] float firefate;
    private float nextfire = 0f;
    private float time = 0;

    //Enemigo Volador
    AIPath myPath;
 

    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
        myAnim = GetComponent<Animator>();

    }

    IEnumerator MiCorutina()
    {
        myAnim.SetBool("isMuerte", true);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        ///////////////////////////////////
        ///Terrestre
        

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
            myAnim.SetBool("isMuerte", true);
            StartCoroutine(MiCorutina());

        }
    }
    

}
