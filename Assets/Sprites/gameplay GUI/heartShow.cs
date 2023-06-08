using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartShow : MonoBehaviour
{
    [SerializeField] Transform b;
    [SerializeField] int num = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        b.position = new Vector3(-6.7f + num, 4.4f, 0);
    }

    public bool SetHeartVisibility(bool a)
    {
        b.gameObject.SetActive(a);
        b.gameObject.GetComponent<SpriteRenderer>().enabled = a;

        return a;
    }
}
