using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIA : MonoBehaviour
{
    public enum UnitStates
    {
        Walking,
        Attacking,
        Dead
    }

    private UnitStates _unitState;

    public UnitStates UnitState
    {
        get => _unitState;
        set => _unitState = value;
    }

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
        print(UnitState);
        switch (UnitState)
        {
            case UnitStates.Walking:
                print(transform.position);
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(target.transform.position.x, transform.position.y, 0f), step);
                break;
            case UnitStates.Attacking:

                //TODO
                // play attack animation
                // play attack sound
                break;
            case UnitStates.Dead:
                //TODO
                //play dead animation
                //play dead sound
                Destroy(gameObject);
                break;
            default:
                _unitState = UnitStates.Walking;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (isEnemy)
        {
           
            if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
            {
                _unitState = UnitStates.Attacking;
                //TODO: Arreglar bug de Ataque
                print("[" + gameObject.name + "] attack to [" + col2D.name + "] --> " + "life:" + lifePoints);
                var otherStats = col2D.gameObject.GetComponent<UnitIA>();
                while (otherStats.lifePoints > 0)
                {
                    if (_attackCountdown <= 0)
                    {
                        otherStats.lifePoints -= attackPower;
                        if (otherStats.lifePoints <= 0)
                        {
                            _unitState = UnitStates.Walking;
                            Destroy(col2D.gameObject);
                        }
                        _attackCountdown = 1f / attackRate;
                    }
                    _attackCountdown -= Time.deltaTime;
                }
                print(col2D.name + "->" + otherStats.lifePoints);
            }
        }
        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
            {
                _unitState = UnitStates.Attacking;
            }
        }
    }
}