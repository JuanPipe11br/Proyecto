using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limon_mov : MonoBehaviour
{

    public float speed;
    public float livingTime = 6f;
    public Vector3 direccion; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 movement= (direccion.normalized * speed * Time.deltaTime);
       transform.Translate(movement);
    }
}