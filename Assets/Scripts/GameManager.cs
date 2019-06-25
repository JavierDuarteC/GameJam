using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [HideInInspector] public string winner;

    public GameObject guarida;
    public GameObject sistemas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    private void Start()
    {
        print(winner);
        if (winner == "Sistemas")
        {
            Instantiate(guarida);
            
        }

        if (winner == "Clientes")
        {
            Instantiate(sistemas);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}