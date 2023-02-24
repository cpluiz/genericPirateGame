using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour{
    private static MainMenuController _instance;
    public static MainMenuController instance {get{return _instance;}}
    public RectTransform mainMenuTransform, optionsTransform;
    void Awake(){
        if(_instance != null){
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    public static void ShowMainMenu(){
        _instance.mainMenuTransform.gameObject.SetActive(true);
        _instance.optionsTransform.gameObject.SetActive(false);
    }
    public void ShowOptions(){
        _instance.mainMenuTransform.gameObject.SetActive(false);
        _instance.optionsTransform.gameObject.SetActive(true);
    }

    public void StartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
