using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class TrueCircleShot : MonoBehaviour
{
    [SerializeField]GameObject shot;
    GameObject[] shots;
    public int shotMemberSize = 15;
    public float speed = 5f;
    public float radius = 12f;

    // Start is called before the first frame update
    void Start()
    {
        shots = new GameObject[shotMemberSize];
        Shot();
    }

    int ded;
    // Update is called once per frame
    void Update()
    {
        ded = 0;
        for (int x = 0; x < shots.Length; x++) 
        {
            shots[x].transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (!shots[x].activeInHierarchy) 
            {
                ded++;
            }
        }


        if (ded >= shots.Length) 
        {
            Debug.Log("DED");
            Destroy(gameObject);
        }
    }

    void Shot ()
    {
        shots = CircleSHotFormation(transform, shots, radius);

    }

    public static GameObject[] CircleSHotFormation(Transform parent, GameObject[] circleShot, float radius) 
    {
        for (int x = 0; x < circleShot.Length; x++)
        {
            circleShot[x] = SpawnManager.instance.SpawnEnemy();
            circleShot[x].transform.position = new Vector3(radius, 0, parent.gameObject.transform.position.z);
            circleShot[x].transform.RotateAround(new Vector3(parent.position.x, parent.position.y, circleShot[x].transform.position.z), new Vector3(0f, 0f, 1f), (360 / circleShot.Length) * x);
            circleShot[x].GetComponent<Enemy>().selfMoving = false;
        }

        return circleShot;
    }
}
