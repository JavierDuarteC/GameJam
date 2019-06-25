using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    // Variables globales del juego
    public float dinero;
    public int income;


    #region Prefabs parameters

    // Ingenieros de Sistemas
    [Header("Ingenieros de sistemas")] 
    public GameObject hacker;
    public GameObject designer;
    public GameObject cio;
    public float maxSisGlobalCD;
    private float sisGlobalCD;
    private Image cdBarSis;

    // Clientes
    [Header("Clientes")] 
    public GameObject emprendedor;
    public GameObject jefe;
    public GameObject abuela;
    public float maxCliGlobalCD;
    private float cliGlobalCD;
    private Image cdBarCli;

    private UnitIA emprendedorStats;
    private UnitIA jefeStats;
    private UnitIA abuelaStats;

    private UnitIA hackerStats;
    private UnitIA designerStats;
    private UnitIA cioStats;

    private Vector3[] pos;
    private int filaP1;
    private int filaP2;

    #endregion


    private void Awake()
    {
        cdBarCli = GameObject.Find("CDBarP2").transform.Find("barP2").GetComponent<Image>();
        cdBarSis = GameObject.Find("CDBarP1").transform.Find("barP1").GetComponent<Image>();
        sisGlobalCD = 0f;
        cliGlobalCD = 0f;
    }

    void Start()
    {
        if (CompareTag("Torre"))
        {
            hackerStats = hacker.GetComponent<UnitIA>();
            designerStats = designer.GetComponent<UnitIA>();
            cioStats = cio.GetComponent<UnitIA>();
            pos = new[] {new Vector3(-15f, -4.81f), new Vector3(-15f, -6.5f), new Vector3(-15f, -8.5f)};
        }

        if (CompareTag("TorreEnemiga"))
        {
            emprendedorStats = emprendedor.GetComponent<UnitIA>();
            jefeStats = jefe.GetComponent<UnitIA>();
            abuelaStats = abuela.GetComponent<UnitIA>();
            pos = new[] {new Vector3(15f, -4.81f), new Vector3(15f, -6.5f), new Vector3(15f, -8.5f)};
        }
    }

    public void UnitGen(GameObject unit, Vector3 lane, UnitIA stats, int fila)
    {
//        print("dinero actual: [" + dinero + "] | costo Unidad: [" + hackerStats.costUnit + ']');
        unit.GetComponent<SpriteRenderer>().sortingOrder = fila + 1;
        Instantiate(unit, lane, Quaternion.Euler(Vector3.zero));
        dinero -= stats.costUnit;
    }

    // Update is called once per frame
    void Update()
    {
        dinero += Time.deltaTime * income;

        if (CompareTag("Torre")) // Ing Sistemas
        {
            cdBarSis.fillAmount = (sisGlobalCD / maxSisGlobalCD);
            sisGlobalCD+=5;
            print("Sistemas GCD: [" + cdBarSis.fillAmount + ']');
            // Compra hacker [Tecla A]
            if (Input.GetKeyDown(KeyCode.A) && dinero >= hackerStats.costUnit && sisGlobalCD >= maxSisGlobalCD)
            {
                UnitGen(hacker, pos[filaP1], hackerStats,filaP1);
                sisGlobalCD = 0f;
                filaP1++;
            }

            // Compra designer [Tecla S]
            if (Input.GetKeyDown(KeyCode.S) && dinero >= designerStats.lifePoints && sisGlobalCD >= maxSisGlobalCD)
            {
                UnitGen(designer, pos[filaP1]-new Vector3(0f,1f), designerStats,filaP1);
                sisGlobalCD = 0f;
                filaP1++;
            }

            //Compra CIO [Tecla D]
            if (Input.GetKeyDown(KeyCode.D) && dinero >= cioStats.costUnit && sisGlobalCD >= maxSisGlobalCD)
            {
                UnitGen(cio, pos[filaP1]-new Vector3(0f,0.2f), cioStats,filaP1);
                sisGlobalCD = 0f;
                filaP1++;
            }

//            print("Fila actual: [" + fila + ']');
            if (filaP1 == 0b11)
                filaP1 = 0b0;
        }
        else if (CompareTag("TorreEnemiga"))
        {
            if (cliGlobalCD >= maxCliGlobalCD)
                cliGlobalCD = maxCliGlobalCD;
            cdBarCli.fillAmount = (cliGlobalCD / maxCliGlobalCD);
            cliGlobalCD+=5;
//            print("Cliente GCD: [" + cliGlobalCD + ']');
            // Clientes [arriba][izq][abajo][derecha]

            // Compra Emprendedor [izq]
            if (Input.GetKeyDown(KeyCode.LeftArrow) && dinero >= emprendedorStats.costUnit &&
                cliGlobalCD >= maxCliGlobalCD)
            {
                UnitGen(emprendedor, pos[filaP2]-new Vector3(0f,1f), emprendedorStats,filaP1);
                cliGlobalCD = 0f;
                filaP2++;
            }

            // Compra Jefe [abajo]
            if (Input.GetKeyDown(KeyCode.DownArrow) && dinero >= jefeStats.costUnit && cliGlobalCD >= maxCliGlobalCD)
            {
                UnitGen(jefe, pos[filaP2]-new Vector3(0f,1.1f), jefeStats,filaP1);
                cliGlobalCD = 0f;
                filaP2++;
            }

            // Compra abuela [der]
            if (Input.GetKeyDown(KeyCode.RightArrow) && dinero >= abuelaStats.costUnit && cliGlobalCD >= maxCliGlobalCD)
            {
                UnitGen(abuela, pos[filaP2], abuelaStats,filaP1);
                cliGlobalCD = 0f;
                filaP2++;
            }

            if (filaP2 == 0b11)
                filaP2 = 0b0;
        }
    }
}