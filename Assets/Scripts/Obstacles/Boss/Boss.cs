using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Behaviour Info")]
    [SerializeField] string[] behaviourList = {}; //The list of things the boss can do
    [SerializeField] string currentBehaviour; //The current behaviour active rn
    [SerializeField] float behaviourTimeChange; //The time it takes to change it's behaviour
    [SerializeField] float behaviourTimer; //The overall timer

    [Header("Boss Properties")]
    [SerializeField] float maxHitpoints;
    [SerializeField] float currentHitPoints;
    [SerializeField] float speed;
    [SerializeField] Vector3 currentPosition;

    [Header("Projectile Properties")]
    [SerializeField] bool circleShotActive;
    [SerializeField] bool trueCircleShotActive;
    [SerializeField] bool volleyActive;
    [SerializeField] float fireRate;
    [SerializeField] float noOfProjectilesFired;
    [SerializeField] int noOfProjectiles;
    [SerializeField] GameObject circleShot;
    [SerializeField] GameObject trueCircleShot;
    [SerializeField] GameObject volley;
    [SerializeField] GameObject projectileLauncher;

    [Header("PlayerStats")]
    [SerializeField] GameObject player;

    [Header("Movement Properties")]
    Vector3 moveLeft;
    Vector3 moveRight;

    private LevelManager levelManager;
    private Animator bossAnimator;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        bossAnimator = GetComponent<Animator>();
        behaviourTimeChange = 1;
    }

    private void OnEnable()
    {
        Shooting.BombUsed += TakeBombDamage;
        isDead = false;
    }

    private void OnDisable()
    {
        Shooting.BombUsed -= TakeBombDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
        behaviourTimer += Time.deltaTime;
        BehaviourList();
        if(behaviourTimeChange <= behaviourTimer && !isDead)
        {
            BehaviourChange();
            behaviourTimer = 0;
            behaviourTimeChange = Random.Range(9.5f, 4f);
        }

        CheckIfDead();
    }
    void BehaviourChange()
    {
        behaviourTimer = 0;
        currentBehaviour = behaviourList[Random.Range(0, behaviourList.Length)];
    }
    void BehaviourList()
    {
        if (isDead)
            return;

        if (currentBehaviour == "SWAY LEFT")
        {
            currentPosition = transform.position;
            moveLeft = new Vector3(-6, 0, 50);
            transform.position = Vector3.MoveTowards(currentPosition, moveLeft, speed);
        }
        else if (currentBehaviour == "SWAY RIGHT")
        {
            currentPosition = transform.position;
            moveRight = new Vector3(6, 0, 50);
            transform.position = Vector3.MoveTowards(currentPosition, moveRight, speed);
        }
        else if (currentBehaviour == "CENTERING")
        {
            currentPosition = transform.position;
            Vector3 center = new Vector3(0, 0, 50);
            transform.position = Vector3.MoveTowards(currentPosition, center, speed);
        }
        else if (currentBehaviour == "CIRCLE SHOT")
        {
            if (!circleShotActive)
            {
                circleShotActive = true;
                noOfProjectiles = 1;
                InvokeRepeating("ShootingCircleShot", 0, fireRate);
            }
        }
        else if (currentBehaviour == "TRUE CIRCLE SHOT")
        {
            if (trueCircleShotActive)
            {
               
            }
            else 
            {
                trueCircleShotActive = true;
                noOfProjectiles = 1;
                InvokeRepeating("ShootingTrueCircle", 0, fireRate);
            }
        }

        else if (currentBehaviour == "VOLLEY") 
        {
            if (!volleyActive)
            {
                volleyActive = true;
                noOfProjectiles = 1;
                InvokeRepeating("Volley", 0, fireRate);
            }
        }


    }
    void ShootingCircleShot()
    {
        if(noOfProjectilesFired < noOfProjectiles)
        {
            Instantiate(circleShot, projectileLauncher.transform.position, transform.rotation);
            noOfProjectilesFired += 1;
        }
        else
        {
            CancelInvoke("ShootingCircleShot");
            noOfProjectilesFired = 0;
            circleShotActive = false;
            BehaviourChange();
        }
    }
    void ShootingTrueCircle()
    {

       Instantiate(trueCircleShot);
       //tmp.GetComponent<TrueCircleShot>().shotMemberSize = noOfProjectiles;
       //BehaviourChange();
        
    }

    void Volley()
    {
        if (noOfProjectilesFired < noOfProjectiles)
        {
            Instantiate(volley, projectileLauncher.transform.position, transform.rotation);
            noOfProjectilesFired += 1;
        }
        else
        {
            CancelInvoke("Volley");
            noOfProjectilesFired = 0;
            volleyActive = false;
            BehaviourChange();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)    // if boss touches player bullet
        {
            SpawnManager.instance.DespawnBullet(other.gameObject);
            AudioManager.instance.PlayClip("EnemyDead");
            Debug.Log($"Boss took damage. HP is at {currentHitPoints}.");
            currentHitPoints -= 5;
        }
    }

    private void TakeBombDamage()
    {
        currentHitPoints -= 20;
        Debug.Log($"$Boss was hurt by player's bomb. HP is at {currentHitPoints}.");
    }

    private void CheckIfDead()
    {
        if (currentHitPoints <= 0)
        {
            // play death animation
            bossAnimator.SetBool("BossDead", true);
            isDead = true;
            SpawnManager.instance.ClearAllEnemies();
            AudioManager.instance.PlayClip("BossDeath");
            //
            
            
        }
    }

    private void WinScene()
    {
        gameObject.SetActive(false);
        AudioManager.instance.StopClip("Boss");
        levelManager.currentState = LevelManager.State.WIN;
    }
}
