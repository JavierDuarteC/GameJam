using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public float costUnit;

    #endregion

    private bool isAttacking = false;
    private bool isMoving = true;

    private UnitManager _unitMan;
    [HideInInspector] public GameObject objAttack;

    private Image healthBarP1;
    private Image healthBarP2;
    private const float maxTowerLife = 1000;

    // Start is called before the first frame update
    void Awake()
    {
        healthBarP1 = GameObject.Find("TowerBar").transform.Find("HealthBarP1").GetComponent<Image>();
        healthBarP2 = GameObject.Find("EnemyBar").transform.Find("HealthBarP2").GetComponent<Image>();
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
        if (isMoving)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.transform.position.x, transform.position.y, 0f), step);
        }
        else
        {
            //stop
        }

        if (!isAttacking) return;
        AttackOver(objAttack);
    }

    private void OnCollisionEnter2D(Collision2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.gameObject.CompareTag("EnemyIA"))
            {
                isMoving = false;
            }
        }
        else
        {
            if (col2D.gameObject.CompareTag("UnitIA"))
            {
                isMoving = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.gameObject.CompareTag("EnemyIA"))
            {
                isMoving = true;
            }
        }
        else
        {
            if (col2D.gameObject.CompareTag("UnitIA"))
            {
                isMoving = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
            {
                isAttacking = true;
                isMoving = false;
                objAttack = col2D.gameObject;
            }
            else if (col2D.CompareTag("EnemyIA"))
            {
                //isMoving = false;
            }
        }
        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
            {
                isAttacking = true;
                isMoving = false;
                objAttack = col2D.gameObject;
            }
            else if (col2D.CompareTag("UnitIA"))
            {
                //isMoving = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col2D)
    {
        if (isEnemy)
        {
            if (col2D.CompareTag("UnitIA") || col2D.CompareTag("Torre"))
            {
                isAttacking = true;
                isMoving = false;
                objAttack = col2D.gameObject;
            }
            else if (col2D.CompareTag("EnemyIA"))
            {
                //isMoving = false;
            }
        }
        else
        {
            if (col2D.CompareTag("EnemyIA") || col2D.CompareTag("TorreEnemiga"))
            {
                isAttacking = true;
                isMoving = false;
                objAttack = col2D.gameObject;
            }
            else if (col2D.CompareTag("UnitIA"))
            {
                //isMoving = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col2D)
    {
        isAttacking = false;
        isMoving = true;
        objAttack = null;
    }

    private void Attack(UnitIA other)
    {
        other.lifePoints -= attackPower;
        if (other.CompareTag("Torre"))
        {
            healthBarP1.fillAmount = other.lifePoints / maxTowerLife;
        }

        if (other.CompareTag("TorreEnemiga"))
        {
            healthBarP2.fillAmount = other.lifePoints / maxTowerLife;
        }

        if (other.lifePoints <= 0)
        {
            if (other.CompareTag("Torre") || other.CompareTag("TorreEnemiga"))
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene(SceneManager.GetSceneAt(3).name); // Cargar pantalla de fin
            }
            else
            {
                _unitMan.dinero += other.costUnit;
                print("Banco: [" + _unitMan.dinero + "] " + "Ganancia: [" + other.costUnit + ']');
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