using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class UnitIA : MonoBehaviour
{
    // Objetivo que va a perseguir
    [HideInInspector] public GameObject target;

    // Enemigo
    [SerializeField] private bool isEnemy;

    #region Atributos

    [Header("Atributos de la unidad")]
    // Vida, tambien es el costo de unidad
    public int lifePoints;

    // Velocidad de movimiento
    [SerializeField] private float speed;

    // Velocidad de Ataque
    [SerializeField] private float attackSpeed;
    private float randomSpeed;
    private float attackTimer;

    // Cantidad de vida que resta
    public int attackPower;

    #endregion

    private bool isAttacking = false;

    private UnitManager _unitMan;
    [HideInInspector] public GameObject objAttack;


    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(isEnemy ? "Torre" : "TorreEnemiga");
        randomSpeed = Random.Range(attackSpeed, attackSpeed + 0.05f);
    }

    private void Start()
    {
        attackTimer = randomSpeed;
        _unitMan = GameObject.Find(CompareTag("UnitIA") ? "Tower" : "EnemyTower").GetComponent<UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Torre") || CompareTag("TorreEnemiga")) return; // Define si la torre ataca o no ataca
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(target.transform.position.x, transform.position.y, 0f), step);
        if (!isAttacking) return;
        AttackOver(objAttack);
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
            {
                isAttacking = true;
                objAttack = col2D.gameObject;
            }
        }
        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
            {
                isAttacking = true;
                objAttack = col2D.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col2D)
    {
        isAttacking = false;
        objAttack = null;
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
            if (other.CompareTag("Torre") || other.CompareTag("TorreEnemiga"))
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene(SceneManager.GetSceneAt(3).name); // Cargar pantalla de fin
            }
            else
            {
                _unitMan.dinero += other.lifePoints * 1000;
                Destroy(other.gameObject);
            }
        }
    }

    private void AttackOver(GameObject objTarget)
    {
        var otherStats = objTarget.GetComponent<UnitIA>();
        if (lifePoints > 0 && otherStats.lifePoints > 0)
        {
            print(attackTimer + " de [" + gameObject.name + ']');
            if (attackTimer <= 0f)
            {
                Attack(otherStats);
                attackTimer = randomSpeed;
            }
            else
            {
                var deltaT = Time.deltaTime;
                attackTimer -= deltaT;
            }
        }
    }
}