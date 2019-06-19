using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitIA : MonoBehaviour
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

    [Header("Vida")] public float lifePoints;

    [Header("Velocidad de movimiento"), SerializeField]
    private float speed;

    [Header("Objetivo")] public GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Torre");
    }

    // Update is called once per frame
    void Update()
    {
        print(UnitState);
        switch (UnitState)
        {
            case UnitStates.Walking:
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                break;
            case UnitStates.Attacking:
                // play attack animation
                // play attack sound
                break;
            case UnitStates.Dead:
                Destroy(gameObject);
                break;
            default:
                _unitState = UnitStates.Walking;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
        {
            _unitState = UnitStates.Attacking;
        }
    }
}