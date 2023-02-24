using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsControlle : MonoBehaviour{
    public TMP_Text gameplayTimeText, spawnIntervalTimeText;
    public Slider gameplayTimeSlider, spawnIntervalTimeSlider;
    private float gamePlayTime, spawnIntervalTime;
    void OnEnable(){
        gamePlayTime = PlayerPrefs.GetFloat("gameplayTime", 60f);
        spawnIntervalTime = PlayerPrefs.GetFloat("spawnIntervalTime", 7f);
        Debug.Log(gamePlayTime);
        gameplayTimeSlider.value = gamePlayTime;
        spawnIntervalTimeSlider.value = spawnIntervalTime;
        UpdateGameplayTime();
        UpdateSpawnTime();
    }
    public void UpdateGameplayTime(){
        gamePlayTime = gameplayTimeSlider.value;
        int minutes = (int)(gamePlayTime / 60);
        int seconds = (int)(gamePlayTime % 60);
        gameplayTimeText.text = minutes + ":" + (seconds.ToString().Length < 2 ? "0" + seconds : seconds);
    }
    public void UpdateSpawnTime(){
        spawnIntervalTime = spawnIntervalTimeSlider.value;
        spawnIntervalTimeText.text = spawnIntervalTime.ToString("0.00")+" seconds";
    }
    public void Save(){
        PlayerPrefs.SetFloat("gameplayTime", gamePlayTime);
        PlayerPrefs.SetFloat("spawnIntervalTime", spawnIntervalTime);
        PlayerPrefs.Save();
    }

    public void BackToMenu(){
        MainMenuController.ShowMainMenu();
    }
}
