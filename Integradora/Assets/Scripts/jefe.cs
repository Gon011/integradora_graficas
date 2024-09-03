using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class jefe : MonoBehaviour
{
    public GameObject bala;
    public Transform spawner;
    public float velocidad = 5f;
    public float velocidad_bala = 20f;
    private int modo = 1;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Patron());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void disparar(Quaternion rotation, float speed = -1)
    {
        GameObject bal = Instantiate(bala, spawner.position, spawner.rotation * rotation);
        Rigidbody fisica = bal.GetComponent<Rigidbody>();
        fisica.velocity = bal.transform.forward * (speed > 0 ? speed : velocidad_bala)
    }

    IEnumerator Patron()
    {
        while (true)
        {
            if (modo == 1)
            {

            }
            else if (modo == 2)
            {

            }
            else if (modo == 3)
            {

            }

            modo = (modo % 3) + 1;
        }
    }


}
