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

    // Vida
    public int lifePoints;

    // Velocidad de movimiento
    [SerializeField] private float speed;

    // Cantidad de vida que resta
    public int attackPower;

    // Enemigo
    [SerializeField] private bool isEnemy;

    [Header("Objetivo")] public GameObject target;

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