using UnityEngine;

public interface IPool<T>
{
    void Load();
    GameObject GetObject(T type);
    void ReleaseObject(GameObject obj);
    void DeactivateObjectsInPool();
}