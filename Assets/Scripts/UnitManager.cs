using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public float dinero;
    public int income;

    // Ingenieros de Sistemas
    [Header("Ingenieros de sistemas")] 
    public GameObject hacker;
    public GameObject designer;
    public GameObject cio;

    // Clientes
    [Header("Clientes")] 
    public GameObject emprendedor;
    public GameObject jefe;
    public GameObject abuela;

    private UnitIA emprendedorStats;
    private UnitIA jefeStats;
    private UnitIA abuelaStats;

    private UnitIA hackerStats;
    private UnitIA designerStats;
    private UnitIA cioStats;

    private Vector3[] pos;
    private int filaP1;
    private int filaP2;


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

    // Update is called once per frame
    void Update()
    {
        dinero += Time.deltaTime * income;

        if (CompareTag("Torre")) // Ing Sistemas
        {
            // Compra hacker [Tecla A]
            if (Input.GetKeyDown(KeyCode.A) && dinero >= hackerStats.costUnit)
            {
                print("dinero actual: [" + dinero + "] | costo Unidad: [" + hackerStats.costUnit + ']');
                hacker.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(hacker, pos[filaP1], Quaternion.Euler(Vector3.zero));
                dinero -= hackerStats.costUnit;
                filaP1++;
            }

            // Compra designer [Tecla S]
            if (Input.GetKeyDown(KeyCode.S) && dinero >= designerStats.lifePoints)
            {
                print("dinero actual: [" + dinero + "] | costo Unidad: [" + designerStats.lifePoints + ']');
                designer.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(designer, pos[filaP1], Quaternion.Euler(Vector3.zero));
                dinero -= designerStats.costUnit;
                filaP1++;
            }

            //Compra CIO [Tecla D]
            if (Input.GetKeyDown(KeyCode.D) && dinero >= cioStats.costUnit)
            {
                print("dinero actual: [" + dinero + "] | costo Unidad: [" + cioStats.costUnit + ']');
                cioStats.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(cio, pos[filaP1], Quaternion.Euler(Vector3.zero));
                dinero -= cioStats.costUnit;
                filaP1++;
            }

//            print("Fila actual: [" + fila + ']');
            if (filaP1 == 3)
            {
                filaP1 = 0;
            }
        }
        else if (CompareTag("TorreEnemiga"))
        {
            // Clientes [arriba][izq][abajo][derecha]

            // Compra Emprendedor [izq]
            if (Input.GetKeyDown(KeyCode.LeftArrow) && dinero >= emprendedorStats.costUnit)
            {
                emprendedor.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(emprendedor, pos[filaP2], Quaternion.Euler(Vector3.zero));
                dinero -= emprendedorStats.costUnit;
                filaP2++;
            }

            // Compra gerente (Jefe) [abajo]
            if (Input.GetKeyDown(KeyCode.DownArrow) && dinero >= jefeStats.costUnit)
            {
                jefe.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(jefe, pos[filaP2], Quaternion.Euler(Vector3.zero));
                dinero -= jefeStats.costUnit;
                filaP2++;
            }

            // Compra abuela [der]
            if (Input.GetKeyDown(KeyCode.RightArrow) && dinero >= abuelaStats.costUnit)
            {
                abuela.GetComponent<SpriteRenderer>().sortingOrder = filaP1 + 1;
                Instantiate(abuela, pos[filaP2], Quaternion.Euler(Vector3.zero));
                dinero -= abuelaStats.costUnit;
                filaP2++;
            }

            if (filaP2 == 3)
            {
                filaP2 = 0;
            }
        }
    }
}