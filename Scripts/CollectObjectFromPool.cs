using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class CollectObjectFromPool : MonoBehaviour
{
    private int collectionCount = 0;

    private const int maxCollectionCount = 3;  // Límite antes de volver al pool
    private EnemyPooling pool;  // Referencia al pool manager

    private void Start()
    {
        // Obtiene la referencia al ObjectPoolManager en la escena
        pool = FindObjectOfType<EnemyPooling>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        collectionCount++;

        if (collectionCount >= maxCollectionCount)
        {
            pool.RemoveAndDestroyObject(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
