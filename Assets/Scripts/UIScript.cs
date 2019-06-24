using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text dineroP1;
    public Text dineroP2;
    private UnitManager _unitMan;
    [SerializeField] private bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _unitMan = GameObject.Find(!isEnemy ? "Tower" : "EnemyTower").GetComponent<UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_unitMan.CompareTag("Torre"))
        {
            var dineroClamp = Mathf.Clamp(_unitMan.dinero, 0, Mathf.Infinity);
//            dineroP1.text = string.Format("{0.000", dineroClamp);
            dineroP1.text = "Crypto: "+ Mathf.Round(dineroClamp).ToString();
        }

        if (_unitMan.CompareTag("TorreEnemiga"))
        {
            var dineroClamp = Mathf.Clamp(_unitMan.dinero, 0, Mathf.Infinity);
            
            dineroP2.text = "Pesos: "+Mathf.Round(dineroClamp).ToString();
        }
    }
}