using UnityEngine;

public class BoatController : MonoBehaviour{
    [Header("Referências de componentes")]
    [SerializeField] protected BoatMovement movement;
    [SerializeField] protected BoatAppearence appearence;
    [SerializeField] protected GameObject cannonContainer;
    [SerializeField] protected Cannon forwardCannon;
    [SerializeField] protected Cannon[] leftCannonGroup;
    [SerializeField] protected Cannon[] rightCannonGroup;
    [SerializeField] protected GameObject explosionEffect;
    [SerializeField] protected BoatUI uiCanvas;
    [SerializeField]
    [Header("Configurações do barco")]
    [Range(1,20)] public int maxHealth = 3;
    [Range(100,400)] public int pointsPerKill = 0;
    public BoatType boatType = BoatType.Shooter;
    public LayerMask targetLayer;
    [Range(0.1f, 10f)]
    public float moveSpeed;
    [Range(0.001f, 1f)]
    public float turnSpeed;
    [Range(30, 500)]
    public float maxTurnSpeed;
    [Range(0.01f, 3f)]
    public float shootInterval;
    private float lastShootTimeForward, lastShootTimeLeft, lastShootTimeRight;
    public bool canShootForward {private set; get;}
    public bool canShootLeft {private set; get;}
    public bool canShootRight {private set; get;}
    public float dammageTaken {private set; get;}
    protected void Awake(){
        if(movement != null)
            movement.SetController(this);
        lastShootTimeForward = Time.time - shootInterval;
        lastShootTimeLeft = Time.time - shootInterval;
        lastShootTimeRight = Time.time - shootInterval;
        dammageTaken = 0;
    }
    protected void Start(){
        cannonContainer.SetActive(boatType == BoatType.Shooter);
        if(cannonContainer.activeSelf){
            foreach(Cannon cannon in leftCannonGroup){
                cannon.PrepareCannon();
                cannon.SetTarget(targetLayer);
            }
            foreach(Cannon cannon in rightCannonGroup){
                cannon.PrepareCannon();
                cannon.SetTarget(targetLayer);
            }
            forwardCannon.PrepareCannon();
            forwardCannon.SetTarget(targetLayer);
        }
        uiCanvas.PrepareHealth(maxHealth);
    }
    protected void Update(){
        canShootForward = lastShootTimeForward + shootInterval <= Time.time;
        canShootLeft = lastShootTimeLeft + shootInterval <= Time.time;
        canShootRight = lastShootTimeRight + shootInterval <= Time.time;
    }
    protected void LateUpdate(){
        uiCanvas.CorrectTransform(transform);
    }
    public void ShootForard(bool enemyShoot = false){
        if(enemyShoot && (!canShootForward || !canShootLeft || !canShootRight)) return;
        if(!canShootForward) return;
        canShootForward = false;
        forwardCannon.Shoot();
        lastShootTimeForward = Time.time;
    }
    private void GroupShoot(Cannon[] cannons){
        foreach(Cannon cannon in cannons){
            cannon.Shoot();
        }
    }
    public void ShootLeft(bool enemyShoot = false){
        if(enemyShoot && (!canShootForward || !canShootLeft || !canShootRight)) return;
        if(!canShootLeft) return;
        canShootLeft = false;
        lastShootTimeLeft = Time.time;
        GroupShoot(leftCannonGroup);
    }
    public void ShootRight(bool enemyShoot = false){
        if(enemyShoot && (!canShootForward || !canShootLeft || !canShootRight)) return;
        if(!canShootRight) return;
        canShootRight = false;
        lastShootTimeRight = Time.time;
        GroupShoot(rightCannonGroup);
    }
    public void TakeDammage(float dammage){
        dammageTaken += dammage;
        appearence.SetBoatAppearenceByDammage(dammageTaken, maxHealth);
        uiCanvas.SetHealth(maxHealth - dammageTaken);
        if(dammageTaken >= maxHealth){
            explosionEffect.SetActive(true);
        }
    }
    public void ExplosionIsOver(){
        if(gameObject.CompareTag("Player")){
            LevelController.GameOver();
        }else{
            if(dammageTaken < maxHealth * 100) //Caso o inimigo que colide exploda, não concede pontos
                LevelController.SetScore(pointsPerKill);
            Destroy(gameObject);
        }
    }
}
[System.Flags]
public enum BoatType{
    Shooter = 0x1,
    Chaser = 0x2
}