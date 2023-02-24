using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour{
    [SerializeField] private TMP_Text screenTime, screenScore, endgameScore;
    private int minutes, seconds;
    [SerializeField] private RectTransform GameOverScreen;

    public void ShowScreenTime(float seconds){
        minutes = (int) seconds / 60;
        seconds = (int) seconds % 60;
        screenTime.text = minutes+":"+(seconds < 10 ? "0" : "")+seconds;
    }

    public void ShowScore(int score){
        screenScore.text = "Score: "+score;
        endgameScore.text = screenScore.text;
    }
    public void ShowGameOverScreen(){
        GameOverScreen.gameObject.SetActive(true);
    }
    public void RestartGame(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void BackToMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
