using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public float dinero;
    public int income;

    // Ingenieros de Sistemas
    [Header("Ingenieros de sistemas")] public GameObject hacker;
    public GameObject designer;
    public GameObject cio;

    // Clientes
    [Header("Clientes")] public GameObject emprendedor;
    public GameObject jefe;
    public GameObject abuela;


    private UnitIA hackerStats;
    private UnitIA designerStats;
    private UnitIA cioStats;

    private Vector3[] pos;


    void Start()
    {
        
        hackerStats = hacker.GetComponent<UnitIA>();
        designerStats = designer.GetComponent<UnitIA>();
        cioStats = cio.GetComponent<UnitIA>();
        pos = new[] {new Vector3(-15f, -4.81f), new Vector3(-15f, -6.5f), new Vector3(-15f, -8.5f)};
    }

    // Update is called once per frame
    void Update()
    {
        
        dinero += Time.deltaTime * income;
        
        if (CompareTag("Torre"))
        {
            
            // Compra hacker [Tecla Q]
            if (Input.GetKeyDown(KeyCode.Q) && dinero >= hackerStats.lifePoints)
            {
                print("dinero actual: [" + dinero + "] | costo Unidad: [" + hackerStats.lifePoints + ']');
                Instantiate(hacker, pos[0], Quaternion.Euler(Vector3.zero));
                dinero -= hackerStats.lifePoints;
            }
            // Compra designer [Tecla A]
            if (Input.GetKeyDown(KeyCode.A) && dinero >= designerStats.lifePoints)
            {
                print("dinero actual: [" + dinero + "] | costo Unidad: [" + designerStats.lifePoints + ']');
                Instantiate(designer, pos[0], Quaternion.Euler(Vector3.zero));
                dinero -= designerStats.lifePoints;
            }
        }
        else if (CompareTag("TorreEnemiga"))
        {
        }
    }
}