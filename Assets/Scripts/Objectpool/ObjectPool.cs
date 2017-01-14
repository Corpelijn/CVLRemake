using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ObjectPool : MonoBehaviour
{
    #region "Fields"

    public PoolObject[] objects = new PoolObject[] { };

    private List<ObjectList> lists;

    private static ObjectPool instance;

    #endregion

    #region "Constructors"



    #endregion

    #region "Properties"



    #endregion

    #region "Methods"

    public void CreateObjectLists()
    {
        foreach (PoolObject obj in objects)
        {
            lists.Add(new ObjectList(obj, transform));
        }
    }

    public void Merge(ObjectPool merge)
    {
        foreach (ObjectList list in merge.lists)
        {
            if (lists.FirstOrDefault(x => x.Name == list.Name) != null)
            {
                Debug.LogError("ObjectPool Merge Conflict: Object with the name \'" + list.Name + "\' already exists");
            }
            else
            {
                lists.Add(list);
                list.UpdateParentTransform(transform);
            }
        }
    }

    #endregion

    #region "Static Methods"

    public static GameObject GetNewObject(string name)
    {
        return instance.lists.Find(l => l.Name == name).GetNext();
    }

    public static GameObject Instantiate(string name)
    {
        return GetNewObject(name);
    }

    public static void GiveBackObject(GameObject obj)
    {
        int uid = obj.GetComponent<PoolIdentifier>().UID;
        instance.lists.Find(l => l.UID == uid).GiveBackObject(obj);
    }

    public static void Destroy(GameObject obj)
    {
        GiveBackObject(obj);
    }

    public static string ResolveName(GameObject obj)
    {
        int uid = obj.GetComponent<PoolIdentifier>().UID;
        return instance.lists.Find(l => l.UID == uid).Name;
    }

    #endregion

    #region "Inherited Methods"

    public void Awake()
    {
        lists = new List<ObjectList>();

        CreateObjectLists();

        if (instance == null)
            instance = this;
        else
        {
            instance.Merge(this);
            Destroy(this);
        }
    }

    #endregion
}
