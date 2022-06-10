using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance; 

	/// <summary>
	/// 通过挂载脚本实现时，不需要把脚本拖到场景内
	/// </summary>
	/// <returns></returns>
    public static T Instance()
	{
        if (instance == null)
		{
			GameObject obj = new GameObject();
			// 设置对象名为脚本名，并在切场景时不被销毁
			obj.name = typeof(T).ToString();
			DontDestroyOnLoad(obj);
			instance = obj.AddComponent<T>();
		}
        return instance;
	}

	/*
	/// <summary>
	/// 通过虚函数实现时，继承类重写Awake时需调用base.Awake()
	/// </summary>
	protected virtual void Awake()
	{
		instance = this as T;
	}
	*/

}
