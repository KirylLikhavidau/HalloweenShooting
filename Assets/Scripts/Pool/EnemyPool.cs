using Configs;
using Factory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool<T> : IPool<T>
{
    private const string PoolConfig = "Configs/PoolConfigAsset";
    private const string Skeleton = "Prefabs/Skeleton/SkeletonWithWeapon";

    private readonly DiContainer _diContainer;

    private GameObject _poolContainer;
    private int _startNumberOfObjects;
    private GameObject _skeletonPrefab;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private EnemyPool(DiContainer container)
    {
        _diContainer = container;
    }

    public void Load()
    {
        PoolConfig poolConfig = (PoolConfig)Resources.Load(PoolConfig);
        _poolContainer = poolConfig.PoolContainer;
        _startNumberOfObjects = poolConfig.StartNumberOfObjects;
        
        _skeletonPrefab = (GameObject)Resources.Load(Skeleton);

        _poolContainer = _diContainer.InstantiatePrefab(_poolContainer);
        for (int i = 0; i < _startNumberOfObjects; i++)
        {
            GameObject obj = _diContainer.InstantiatePrefab(_skeletonPrefab, Vector3.zero, Quaternion.identity, null);
            obj.transform.parent = _poolContainer.transform;
            ReleaseObject(obj);
        }
    }

    public GameObject GetObject(T type)
    {
        GameObject objToGet;

        if (_pool.Count == 0)
        {
            switch (type)
            {
                case EnemyType.Skeleton:
                    objToGet = _diContainer.InstantiatePrefab(_skeletonPrefab, Vector3.zero, Quaternion.identity, null);
                    objToGet.transform.parent = _poolContainer.transform;
                    return objToGet;
                default:
                    Debug.LogWarning("EnemyType is null or wrong!");
                    objToGet = _diContainer.InstantiatePrefab(_skeletonPrefab, Vector3.zero, Quaternion.identity, null);
                    objToGet.transform.parent = _poolContainer.transform;
                    return objToGet;
            }
        }

        objToGet = _pool.Dequeue();
        objToGet.SetActive(true);

        return objToGet;
    }

    public void ReleaseObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void DeactivateObjectsInPool()
    {
        GameObject[] children = _poolContainer.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < children.Length; i++)
        {
            ReleaseObject(children[i]);
        }
    }
}
