using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jefe : MonoBehaviour
{
    public GameObject bala;
    public Transform spawner;
    public float velocidad = 2f;
    public float velocidad_bala = 10f;
    private int modo = 1;
    private Vector3 posicionObjetivo; // Posición objetivo para la transición entre modos
    private bool enTransicion = false; // Indicador de transición entre modos

    // Start is called before the first frame update
    void Start()
    {
        posicionObjetivo = transform.position;
        StartCoroutine(Patron());
    }

    // Update is called once per frame
    void Update()
    {
        if (!enTransicion)
        {
            if (modo == 1)
            {
                // Movimiento en forma de "8" en X y Z
                float tiempo = Time.time * velocidad;
                float x = Mathf.Sin(tiempo) * 10;
                float z = Mathf.Sin(tiempo * 2) * 5;
                posicionObjetivo = new Vector3(x, transform.position.y, z);
            }
            else if (modo == 2)
            {
                // Movimiento en zigzag en X y Z
                float zigzagSpeed = velocidad * 0.5f;
                posicionObjetivo = new Vector3(Mathf.Sin(Time.time * zigzagSpeed) * 10, transform.position.y, Mathf.Cos(Time.time * zigzagSpeed) * 10);
            }
            else if (modo == 3)
            {
                // Movimiento en arcos en X y Z
                float arcoSpeed = velocidad * 1.5f;
                float x = Mathf.PingPong(Time.time * arcoSpeed, 20) - 10;
                float z = Mathf.Sin(Time.time * arcoSpeed) * 5;
                posicionObjetivo = new Vector3(x, transform.position.y, z);
            }

            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, Time.deltaTime * velocidad);
        }
    }

    void disparar(Quaternion rotation, float speed = -1)
    {
        GameObject bal = Instantiate(bala, spawner.position, spawner.rotation * rotation);
        Rigidbody fisica = bal.GetComponent<Rigidbody>();
        fisica.velocity = bal.transform.forward * (speed > 0 ? speed : velocidad_bala);
    }

    IEnumerator Patron()
    {
        while (true)
        {
            float startTime = Time.time;

            if (modo == 1)
            {
                while (Time.time - startTime < 10f)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        float offset = Mathf.Sin(Time.time * 5 + i) * 15; 
                        disparar(Quaternion.Euler(180, offset, 0));
                    }
                    yield return new WaitForSeconds(0.3f); // Intervalo entre disparos
                }
            }
            else if (modo == 2)
            {
                while (Time.time - startTime < 10f)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        float angle = i * 30; // 360 / 12 = 30 grados entre cada bala
                        disparar(Quaternion.Euler(0, angle, 0));
                    }
                    yield return new WaitForSeconds(0.3f); // Intervalo entre disparos
                }
            }
            else if (modo == 3)
            {
                while (Time.time - startTime < 10f)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        float angle = i * 36;
                        float petalAngleOffset = Mathf.Sin(Time.time * 5 + i) * 15; 
                        disparar(Quaternion.Euler(0, angle + petalAngleOffset, 0));
                    }
                    yield return new WaitForSeconds(0.3f); // Intervalo entre disparos
                }
            }

            // Transición suave al siguiente modo
            enTransicion = true;
            yield return StartCoroutine(TransicionAlSiguienteModo());
            modo = (modo % 3) + 1; // Cambiar al siguiente modo
            enTransicion = false;
        }
    }

    IEnumerator TransicionAlSiguienteModo()
    {
        // Transición suave entre modos en X y Z
        Vector3 nuevaPosicionObjetivo = new Vector3(
            Random.Range(-10, 10), 
            transform.position.y, 
            Random.Range(-10, 10)
        );

        while (Vector3.Distance(transform.position, nuevaPosicionObjetivo) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, nuevaPosicionObjetivo, Time.deltaTime * velocidad);
            yield return null;
        }
    }
}
