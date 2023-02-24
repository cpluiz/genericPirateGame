using UnityEngine;

public class LevelController : MonoBehaviour{
    private static LevelController _instance;
    public static LevelController instance{get{return _instance;}}
    public BoatController Player;
    public float timeLimit {get; private set;}
    public float totalTime {get; private set;}
    public int score{get; private set;}
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private LevelUI levelUI;
    void Awake(){
        if(_instance != null)
            Destroy(this.gameObject);
        
        _instance = this;

        if(Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>();

        if(levelUI == null)
            levelUI = GetComponentInChildren<LevelUI>();
    }

    void Start(){
        enemySpawner.StartSpawn();
        totalTime = PlayerPrefs.GetFloat("gameplayTime", 60f);
        timeLimit = totalTime;
    }
    void Update(){
        timeLimit -= Time.deltaTime;
        levelUI.ShowScreenTime(timeLimit);
        if(timeLimit <- 0){
            
        }
    }
    public static void SetScore(int points){
        _instance.score += points;
        _instance.levelUI.ShowScore(_instance.score);
    }
    public static void GameOver(){
        Time.timeScale = 0;
        _instance.levelUI.ShowGameOverScreen();
    }
}