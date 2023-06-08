
using System.Collections;
using UnityEngine;

/*
Alex Gulewich
Feb, 25, 2023
Enemy Ship
LASER SHIP GO BRRRR
*/
public class EnemyShip : MonoBehaviour
{
    // Laser Charge
    ParticleSystem charge;
    ParticleSystem death;
    public float chargeDelay = 5f;
    [SerializeField] bool charging, chargingNotStarted, firing, cannonCoolingDown, fireable, moving;

    // Laser cannon
    GameObject laser;
    Light light;

    float t = 0; // timer
    Rigidbody rb;

    // Player
    [SerializeField] GameObject player;

    // Ship
    public float speed = 40f;
    public float maxSpeed = 80f;
    bool dead;


    public void Innit() 
    {
        // Charge setup
        charge.Stop();
        chargingNotStarted = true;

        // Laser setup
        laser.SetActive(false);
        light.gameObject.SetActive(false);

        fireable = true;

        // Ship setup
        GetComponent<SpriteRenderer>().enabled = true;
        moving = true;
        dead = false;

        death.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get charge
        charge = transform.GetChild(0).GetComponent<ParticleSystem>();

        // Get player
        player = GameObject.Find("unga bunga player");

        // Get Laser
        laser = transform.GetChild(1).gameObject;
        light = transform.GetChild(2).gameObject.GetComponent<Light>();

        // Get Ship
        rb = GetComponent<Rigidbody>();
        death = transform.GetChild(3).GetComponent<ParticleSystem>();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            move();

            if (fireable && !moving)
            {
                Charge();
            }
        }
    }

    void Charge() 
    {
        if (chargingNotStarted)
        {
            charge.Play();
            charge.startSpeed = -0.97f;
            charge.startLifetime = 1.28f;
            chargingNotStarted = false;
            charging = true;
        }

        else if (chargeDelay < t)
        {
            chargingNotStarted = false;
            t = 0;
            charge.Stop();
            fireable = false;
            Fire();
        }

        else
        {
            t += Time.deltaTime;
            charge.startSpeed = -0.97f - (t);
            charge.startLifetime = 1.28f / (t / 2);
            charge.emissionRate = 1000 * t;
        }
    }

    // Default range = 5.2
    // Default intensity = 1.27
    void Fire() 
    {
        light.gameObject.SetActive(true);
        light.range = 1f;
        light.intensity = 109f-25;
        firing = true;
        charging = false;
        StartCoroutine(PreFire());
    }

    // Prepare to fire!
    int skip;
    IEnumerator PreFire() 
    { 
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (dead) 
            {
                break;
            }

            skip = 0;

            if (light.range < 3f)
            {
                light.range += 0.3f;
            }
            else 
            {
                skip++;
            }

            if (light.intensity > 50f-25)
            {
                light.intensity -= 10f * Time.deltaTime;
            }
            else 
            {
                skip++;
            }

            if (skip > 1) 
            {
                break;
            }
        }
        if (!dead)
        {
            Shoot();
        }
        else 
        {
            StartCoroutine(CannonCoolDown());
        }
    }

    // SHOOT THE LASER
    void Shoot() 
    {
        laser.SetActive(true);
        StartCoroutine(ShootTimeLimit());
    }

    // Limits the fire time
    IEnumerator ShootTimeLimit() 
    {
        yield return new WaitForSeconds(4.5f);
        laser.SetActive(false);
        while (light.range > 0)
        {
            if (dead) 
            {
                break;
            }

            light.range -= 3f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CannonCoolDown());
    }

    IEnumerator CannonCoolDown() 
    {
        light.gameObject.SetActive(false);
        firing = false;
        cannonCoolingDown = true;
        yield return new WaitForSeconds(3f);
        cannonCoolingDown = false;
        chargingNotStarted = true;
        fireable = true;
    }

    void move() 
    {
        moving = true;
        Vector3 move = new Vector3(0,0,0);
        if (gameObject.transform.position.z - player.transform.position.z > 25f)
        {
            float s = DifficultyScale.scale;
            if (speed * DifficultyScale.scale > maxSpeed)
            {
                s = maxSpeed / speed;
            }
            move = (Vector3.back * (speed * s) * Time.deltaTime);
        }

        else 
        {
            moving = false;
            if (cannonCoolingDown || charging)
            {
                if (player.transform.position.x < 0.1 && gameObject.transform.position.x < 2)
                {
                    move = (Vector3.right * speed * Time.deltaTime);
                }

                else if (player.transform.position.x > 0 && gameObject.transform.position.x > -2)
                {
                    move = (Vector3.left * speed * Time.deltaTime);
                }

                if (player.transform.position.y < 0.1 && gameObject.transform.position.y < 2)
                {
                    move = (Vector3.up * speed * Time.deltaTime);
                }

                else if (player.transform.position.y > 0 && gameObject.transform.position.y > -2)
                {
                    move = (Vector3.down * speed * Time.deltaTime);
                }

                move.z = 0;
            }

            if (firing)
            {
                //Vector3 mt = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, 0);
                Vector3 mt = new Vector3(gameObject.transform.position.x - player.transform.position.x, gameObject.transform.position.y - player.transform.position.y, 0);
                Vector3 mtN;

                int[] normal = {0,0,0};

                if (mt.x > 0) 
                {
                    normal[0] = -1;
                }

                if (mt.y > 0)
                {
                    normal[1] = -1;
                }

                mtN = new Vector3( (mt.x / mt.x ) * normal[0], (mt.y / mt.y) * normal[1] , 0);

                move = ((mtN * speed * Time.deltaTime));
            }
        }
        rb.MovePosition(transform.position + move);
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SpawnManager.instance.DespawnBullet(collision.gameObject);
            StartCoroutine(playDeathAnimation());
        }
    }

    IEnumerator playDeathAnimation()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider bc = GetComponent<BoxCollider>();

        sr.enabled = false;
        bc.enabled = false;
        
        dead = true;
        charge.gameObject.SetActive(false);
        laser.SetActive(false);

        death.gameObject.SetActive(true);
        death.Play();

        yield return new WaitForSeconds(6);

        charge.gameObject.SetActive(true);
        laser.SetActive(true);
        sr.enabled = true;
        bc.enabled = true;

        gameObject.SetActive(false);
        SpawnManager.instance.SpawnPowerup(gameObject.transform.position);
    }
}
