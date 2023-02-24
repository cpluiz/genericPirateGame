using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour{
    [SerializeField] private Transform firePoint;
    [SerializeField] private CannonBall ballPrefab;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ExplosionEffect cannonExplosion;
    [Header("Configurações do canhão")]
    [SerializeField, Range(2, 20)] private int ballSpoolSize;
    [SerializeField] private AudioClip[] shotAudio;
    private CannonBall[] ballSpool;
    private LayerMask targetLayer;
    private bool shooted = false;
    public void SetTarget(LayerMask target){
        targetLayer = target;
        foreach(CannonBall ball in ballSpool){
            ball.SetTarget(target);
        }
    }
    public void PrepareCannon(){
        ballSpool = new CannonBall[ballSpoolSize];
        for(int i=0; i<ballSpoolSize ; i++){
            ballSpool[i] = Instantiate<CannonBall>(ballPrefab, firePoint.position, firePoint.rotation);
            ballSpool[i].gameObject.SetActive(false);
            cannonExplosion.gameObject.SetActive(false);
        }
    }

    public bool Shoot(){
        shooted = false;
        foreach(CannonBall ball in ballSpool){
            if(!ball.gameObject.activeSelf){
                shooted = true;
                ball.transform.localPosition = firePoint.transform.position;
                ball.transform.localRotation = firePoint.transform.rotation;
                ball.gameObject.SetActive(true);
                cannonExplosion.gameObject.SetActive(true);
                audioSource.PlayOneShot(shotAudio[Random.Range(0, shotAudio.Length)]);
                break;
            }
        }
        return shooted;
    }
}
