using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public Transform salida;
    public GameObject[] enemigos;
    int contenemigos = 2;
    public float timeSpawn = 1;
    public float repetir = 3;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies",timeSpawn,repetir);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            spawnPosition = new Vector3(salida.position.x, salida.position.y);
            GameObject enemigo = Instantiate(enemigos[0], spawnPosition, gameObject.transform.rotation);
            contenemigos++;
        Debug.Log(contenemigos);


    }
}
