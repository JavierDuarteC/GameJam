using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private float attackSpeed;
    private float randomSpeed;
    private float attackTimer;

    [Header("Objetivo, por defecto null")] public GameObject objAttack;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(isEnemy ? "Torre" : "TorreEnemiga");
        randomSpeed = Random.Range(attackSpeed, attackSpeed + 0.1f);
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
            {
                AttackOver(col2D);
            }
        }
        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
            {
                AttackOver(col2D);
            }
        }
    }

    private void Attack(UnitIA other)
    {
        other.lifePoints -= attackPower;
        print("[" + gameObject.name + "] attack to [" + other.gameObject.name + "] with [" + randomSpeed +
              "] Aspd");
        print("Vida de " + gameObject.name + " --> " + lifePoints);
        print("Vida de: " + other.gameObject.name + "-->" + other.lifePoints);

        if (other.lifePoints <= 0)
        {
            print("Muere > " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    private void AttackOver(Collider2D col2D)
    {
        objAttack = col2D.gameObject;
        var otherStats = objAttack.GetComponent<UnitIA>();
        if (lifePoints > 0 && otherStats.lifePoints > 0)
        {
            print(attackTimer + " de ["+ gameObject.name +']');
            if (attackTimer <= 0)
            {
                Attack(otherStats);
                attackTimer = randomSpeed;
            }
            else
            {
                var deltaT = Time.deltaTime;
                attackTimer -= deltaT*100;
                
            }
        }
        else
        {
            objAttack = null;
        }
    }
}