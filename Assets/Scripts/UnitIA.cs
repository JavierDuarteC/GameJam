using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIA : MonoBehaviour
{
    // Objetivo que va a perseguir
    public GameObject target;

    // Enemigo
    [SerializeField] private bool isEnemy;

    [Header("Atributos de la unidad")]
    // Vida
    public int lifePoints;

    // Velocidad de movimiento
    [SerializeField] private float speed;

    // Cantidad de vida que resta
    public int attackPower;

    // Velocidad de Ataque
    public float attackRate = 1f;
    private float _attackCountdown = 0;


    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(isEnemy ? "Torre" : "TorreEnemiga");
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(target.transform.position.x, transform.position.y, 0f), step);
    }


    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
                Attack(col2D);
        }

        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
                Attack(col2D);
        }
    }

    private void Attack(Collider2D other)
    {
        // TODO: Corregir el bug del Emprendedor que mata todo
        var otherStats = other.gameObject.GetComponent<UnitIA>();
        while (otherStats.lifePoints > 0 && lifePoints > 0)
        {
            print("[" + gameObject.name + "] attack to [" + other.name + "]");
            if (_attackCountdown <= 0)
            {
                otherStats.lifePoints -= attackPower;
                if (otherStats.lifePoints <= 0)
                {
                    Destroy(other.gameObject);
                }

                _attackCountdown = 1f / attackRate;
            }

            print("Vida resultante del objetivo:" + otherStats.lifePoints);

            _attackCountdown -= Time.deltaTime;
        }
    }
}