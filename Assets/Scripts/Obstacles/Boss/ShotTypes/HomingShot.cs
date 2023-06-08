using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShot : MonoBehaviour
{
    public GameObject HomingShotPrefab;
    Rigidbody[] shotMembers;

    public int memberSize = 15;
    public float radius = 12f;
    public Vector3[] startPos;
    float s = 0.005f; // Invisible speed const
    public float speed = 1f;

    GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unga bunga player");

        if (player == null) Debug.Log("COULD NOT FIND PLAYER");

        // Get the Gameobjects
        GameObject[] members = new GameObject[memberSize];
        shotMembers = new Rigidbody[memberSize];
        members = TrueCircleShot.CircleSHotFormation(transform, members, radius);

        // Assign the rigidbodies
        for (int x = 0; x < memberSize; x++) 
        {
            shotMembers[x] = members[x].GetComponent<Rigidbody>();
        }

        StartPosInit();
    }

    // Update is called once per frame
    float t = 0;
    void Update()
    {
        for (int x = 0; x < shotMembers.Length; x++)
        {
             Vector3 pivot = (startPos[x] + player.transform.position) * 0.5f;
             shotMembers[x].AddForce ( -Vector3.Slerp(startPos[x], player.transform.position, t) );
             t += (s * speed) * Time.deltaTime;
        }
    }

    void StartPosInit() 
    {
        startPos = new Vector3[memberSize];
        for (int x = 0; x < memberSize; x++) 
        {
            startPos[x] = shotMembers[x].transform.position;

            // SMALL BALLS
            shotMembers[x].transform.localScale = new Vector3(.4f, .4f, .4f);
        }
    }
}
