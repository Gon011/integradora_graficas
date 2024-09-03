using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class contador : MonoBehaviour
{
    public TextMeshProGUI texto;
    

    // Update is called once per frame
    void Update()
    {
        int cuenta = GameObject.FindGameObjectsWithTag("Bala").Length;

        texto.text = "Número de balas: " + cuenta.ToString();
    }
}
