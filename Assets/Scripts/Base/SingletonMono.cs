using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance; 

	/// <summary>
	/// ͨ�����ؽű�ʵ��ʱ������Ҫ�ѽű��ϵ�������
	/// </summary>
	/// <returns></returns>
    public static T Instance()
	{
        if (instance == null)
		{
			GameObject obj = new GameObject();
			// ���ö�����Ϊ�ű����������г���ʱ��������
			obj.name = typeof(T).ToString();
			DontDestroyOnLoad(obj);
			instance = obj.AddComponent<T>();
		}
        return instance;
	}

	/*
	/// <summary>
	/// ͨ���麯��ʵ��ʱ���̳�����дAwakeʱ�����base.Awake()
	/// </summary>
	protected virtual void Awake()
	{
		instance = this as T;
	}
	*/

}
