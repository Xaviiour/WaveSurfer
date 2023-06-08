using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform parentObj;
    private Queue<GameObject> objectPool = new Queue<GameObject>();
    public Queue<GameObject> ObjectPool { get { return objectPool; } }
    private int poolAmount = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        SetPool();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPool()
    {
        for (int i = 0; i < poolAmount; ++i)
        {
            GameObject newObj = Instantiate(objectPrefab, parentObj);
            newObj.SetActive(false);
            objectPool.Enqueue(newObj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        GameObject nextObj = null;

        if (objectPool.Count > 0)
        {
            nextObj = objectPool.Dequeue();
            nextObj.SetActive(true);
        }

        else
        {
            nextObj = Instantiate(objectPrefab, parentObj);
        }

        return nextObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        objectPool.Enqueue(obj);
        obj.SetActive(false);
    }
}
