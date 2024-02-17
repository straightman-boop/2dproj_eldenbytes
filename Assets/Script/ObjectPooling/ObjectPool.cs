using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private PoolableObject prefab;
    private List<PoolableObject> objects;

    private ObjectPool(PoolableObject prefab, int size)
    {
        this.prefab = prefab;
        objects = new List<PoolableObject>();
    }

    public static ObjectPool CreateInstance(PoolableObject prefab, int size)
    {
        ObjectPool pool = new ObjectPool(prefab, size);

        GameObject poolObject = new GameObject(prefab.name + "Pool");
        pool.CreateObjects(poolObject.transform, size);

        return pool;
    }

    private void CreateObjects(Transform parent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector2.zero, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        objects.Add(poolableObject);
    }

    public PoolableObject GetObject()
    {
        if (objects.Count > 0)
        {
            PoolableObject instance = objects[0];
            objects.RemoveAt(0);

            instance.gameObject.SetActive(true);

            return instance;
        }

        else
        {
            Debug.LogError("adw");
            return null;
        }
    }

}
