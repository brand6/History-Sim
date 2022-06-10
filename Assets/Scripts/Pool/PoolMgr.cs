using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolData
{
	public GameObject fatherObj; // ��Ŀ¼�ڵ�
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
		// ��ʾ����
		obj.transform.parent = null;
		obj.SetActive(true);
		return obj;
	}

	//���뻺���
	public void PushObj(GameObject obj)
	{
		obj.transform.parent = fatherObj.transform;
		pooList.Add(obj);
		// ���ض���
		obj.SetActive(false);
	}
}

public class PoolMgr : BaseManager<PoolMgr>
{
	// ���������
	public Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();

	private GameObject poolObj = new GameObject("Pool");

	// �ӻ����ȡ
	public GameObject GetObject(string name)
	{
		if(!poolDict.ContainsKey(name))
		{
			poolDict.Add(name, new PoolData(name, poolObj));
		}
		return poolDict[name].GetObject(name);
	}

	//���뻺���
	public void PushObj(string name,GameObject obj)
	{	
		if (!poolDict.ContainsKey(name))
		{
			poolDict.Add(name, new PoolData(name, poolObj));
		}
		poolDict[name].PushObj(obj);	
	}

	// ��ջ���أ���Ҫ���ڳ����л�ʱ
	public void ClearPool()
	{
		poolDict.Clear();
		poolObj = null;
	}
}
