using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int poolSize = 10;
    public int activeObjectLimit = 5;

    private List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(collectiblePrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private void Update()
    {
        while (CountActiveObjects() < activeObjectLimit)
        {
            GameObject obj = GetObjectFromPool();
            if (obj != null)
            {
                obj.transform.position = GetRandomPosition();
                obj.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }

    private GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    private int CountActiveObjects()
    {
        int count = 0;
        foreach (GameObject obj in pool)
        {
            if (obj.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }

    public void RemoveAndDestroyObject(GameObject obj)
    {
        pool.Remove(obj);
        Destroy(obj);      
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        return new Vector3(x, y, 0);
    }
}
