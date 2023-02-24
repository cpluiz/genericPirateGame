using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAppearence : MonoBehaviour{
    
    [Header("Referências aos renderizadores de imagem do barco")]
    [SerializeField] private SpriteRenderer shipHull;
    [SerializeField] private SpriteRenderer shipSail, shipMast;
    private Sprite[] currentBoat, currentSail;
    [Header("Imagens dos tipos de corpo do barco")]
    [Range(0, 1)]
    public int boatStyle;
    [SerializeField] private Sprite[] boatSprite0, boatSprite1;
    private Dictionary<int, Sprite[]> availableBoat = new Dictionary<int, Sprite[]>();
    [Header("Imagens dos tipos de velas do barco")]
    [Range(0, 5)]
    public int sailType;
    [SerializeField] private Sprite[] sailSprite0, sailSprite1, sailSprite2, sailSprite3, sailSprite4, sailSprite5;
    [SerializeField] private Color[] mastColor;
    private Dictionary<int, Sprite[]> availableSail = new Dictionary<int, Sprite[]>();
    void Awake(){
        availableBoat.TryAdd(0, boatSprite0);
        availableBoat.TryAdd(1, boatSprite1);
        //
        availableSail.TryAdd(0, sailSprite0);
        availableSail.TryAdd(1, sailSprite1);
        availableSail.TryAdd(2, sailSprite2);
        availableSail.TryAdd(3, sailSprite3);
        availableSail.TryAdd(4, sailSprite4);
        availableSail.TryAdd(5, sailSprite5);
    }
    // Start is called before the first frame update
    void Start(){
        availableBoat.TryGetValue(boatStyle, out currentBoat);
        availableSail.TryGetValue(sailType, out currentSail);
        shipMast.color = mastColor[sailType];
        SetBoatAppearenceByDammage(0, 1);
    }
    public void SetBoatAppearenceByDammage(float dammage, float maxHealth){
        shipHull.sprite = currentBoat[(int)Mathf.Clamp((dammage / maxHealth) * (currentBoat.Length -1), 0, currentBoat.Length -1)];
        shipSail.sprite = currentSail[(int)Mathf.Clamp((dammage / maxHealth) * (currentSail.Length -1), 0, currentSail.Length -1)];
    }
}
