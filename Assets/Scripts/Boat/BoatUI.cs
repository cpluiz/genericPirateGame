using UnityEngine;
using UnityEngine.UI;

public class BoatUI : MonoBehaviour{
    [SerializeField] private Slider healthIndicator;
    [SerializeField] private RectTransform rectTransform;

    public void PrepareHealth(float maxHealth){
        healthIndicator.maxValue = maxHealth;
        healthIndicator.value = maxHealth;
    }

    public void SetHealth(float currentHealth){
        healthIndicator.value = currentHealth;
        
    }

    public void CorrectTransform(Transform parentAngle){
        rectTransform.rotation = Quaternion.Euler(parentAngle.eulerAngles - new Vector3(0, 0, parentAngle.eulerAngles.z));
    }
}
