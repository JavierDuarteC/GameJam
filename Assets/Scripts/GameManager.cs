using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool introSoundFlag;
    private bool playSoundFlag;
    private bool winSoundFlag;

    public SoundManager _Sound;

    private Scene _gameScene;

    public Scene GameScene
    {
        get => _gameScene;
        set => _gameScene = value;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _Sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        GameScene = SceneManager.GetActiveScene();
        if (GameScene.name.Equals("GameMenu") || GameScene.name.Equals("Characters") || GameScene.name.Equals("Intro"))
        {
            if (!introSoundFlag)
            {
                _Sound.playIntroEfx(_Sound.introEfx.clip);
                introSoundFlag = true;
            }
        }
        
        if (GameScene.name.Equals("GameScene"))
        {
            print(playSoundFlag);
            if (!playSoundFlag || winSoundFlag){
                _Sound.stopIntroEfx();
                _Sound.stopEndEfx();
                _Sound.playPlayingEfx(_Sound.playingEfx.clip);
                playSoundFlag = true;
                winSoundFlag = false;
            }
        }
        
        if (GameScene.name.Equals("Winner"))
        {
            if (!winSoundFlag)
            {
                _Sound.stopPlayingEfx();
                _Sound.playEndEfx(_Sound.endEfx.clip);
                winSoundFlag = true;
            }
        }
    }
}
