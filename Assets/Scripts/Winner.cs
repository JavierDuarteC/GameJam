using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    [HideInInspector] public GameObject guarida;
    [HideInInspector] public GameObject pozo;

    private void Awake()
    {
        guarida = GameObject.Find("Guarida");
        guarida.SetActive(false);
        pozo = GameObject.Find("Pozo");
        pozo.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (WinnerHolder.winner == "Sistemas")
        {
            guarida.SetActive(true);
        }

        if (WinnerHolder.winner == "Clientes")
        {
            pozo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}