using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
	public GameObject fatherObj; // 子目录节点
	public List<GameObject> pooList;

	public PoolData(string name, GameObject poolObj)
	{
		fatherObj = new GameObject(name);
		fatherObj.transform.parent = poolObj.transform;
		pooList = new List<GameObject>();
	}

	public GameObject GetObject(string name)
	{
		GameObject obj;
		if (pooList.Count > 0)
		{
			obj = pooList[0];
			pooList.RemoveAt(0);
		}
		else
		{
			obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
			obj.name = name;
		}
		// 显示对象
		obj.transform.parent = null;
		obj.SetActive(true);
		return obj;
	}

	//存入缓存池
	public void PushObj(GameObject obj)
	{
		obj.transform.parent = fatherObj.transform;
		pooList.Add(obj);
		// 隐藏对象
		obj.SetActive(false);
	}
}

public class PoolMgr : BaseManager<PoolMgr>
{
	// 缓存池容器
	public Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();

	private GameObject poolObj = new GameObject("Pool");

	// 从缓存池取
	public GameObject GetObject(string name)
	{
		if(!poolDict.ContainsKey(name))
		{
			poolDict.Add(name, new PoolData(name, poolObj));
		}
		return poolDict[name].GetObject(name);
	}

	//存入缓存池
	public void PushObj(string name,GameObject obj)
	{	
		if (!poolDict.ContainsKey(name))
		{
			poolDict.Add(name, new PoolData(name, poolObj));
		}
		poolDict[name].PushObj(obj);	
	}

	// 清空缓存池，主要用于场景切换时
	public void ClearPool()
	{
		poolDict.Clear();
		poolObj = null;
	}
}
