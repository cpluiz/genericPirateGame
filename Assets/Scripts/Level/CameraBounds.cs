using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour{
    public Vector2 left, top, right, bottom;
    private Vector2 lastLeft, lastTop, lastRight, lastBottom;
    [SerializeField] private float screenWidth, screenHeight;
    [SerializeField] private Transform leftBorder, rightBorder, topBorder, bottomBorder;

    public static CameraBounds instance { get { return _instance; } }
    private static CameraBounds _instance;

    void Awake(){
        if(_instance != null)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    void Start(){
        UpdateScreenBounds(true);
    }
    void LateUpdate(){
        UpdateScreenBounds();
    }
    void UpdateScreenBounds(bool force = false){
        //Registra a última posição das bordas da tela
        lastLeft = left;
        lastTop = top;
        lastRight = right;
        lastBottom = bottom;
        //Registra a posição atual das bordas da tela
        left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
        top = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));
        right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
        bottom = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, Camera.main.nearClipPlane));
        /***
        Caso a resolução da tela tenha sido alterada, ou a função tenha sido executada para 
        forçar a atualização do tamanho, registra o tamanho da tela em unidades e reposiciona
        as barreiras/bordas da tela.
        ***/
        if(lastLeft != left || lastRight != right || lastTop != top || lastBottom != bottom || force){
            screenWidth = right.x - left.x;
            screenHeight = top.y - bottom.y;
            leftBorder.localScale = rightBorder.localScale = new Vector2(0.01f, screenHeight);
            topBorder.localScale = bottomBorder.localScale = new Vector2(screenWidth, 0.01f);
            leftBorder.position = left;
            rightBorder.position = right;
            topBorder.position = top;
            bottomBorder.position = bottom;
        }
    }
}
