using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonTools 
{
	/// <summary>
	/// 将对象保存为Json文本
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <param name="paths">保存路径</param>
	public static void SaveObjAsJsonStr<T>(T obj,params string[] paths)
	{
		string str = ObjToJsonStr(obj);
		SaveTextToFile(str, paths);
	}

	/// <summary>
	/// 从Json文本读取对象
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="paths">路径</param>
	/// <returns>对象</returns>
	public static T loadJsonFileToObj<T>(params string[] paths)
	{
		string jsonStr = LoadTextFromFile(paths);
		return JsonStrToObj<T>(jsonStr);
	}

	/// <summary>
	/// 对象转Json字符串
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="target">目标对象</param>
	/// <returns>Json字符串</returns>
	public static string ObjToJsonStr<T>(T target)
	{
		return JsonUtility.ToJson(target);
	}

	/// <summary>
	/// Json字符串转对象
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="jsonStr">Json字符串</param>
	/// <returns>目标对象</returns>
	public static T JsonStrToObj<T>(string jsonStr)
	{
		return JsonUtility.FromJson<T>(jsonStr);
	}

	/// <summary>
	/// 保存文件
	/// </summary>
	/// <param name="text">保存的文本</param>
	/// <param name="paths">保存路径</param>
	public static void SaveTextToFile(string text,params string[] paths)
	{
		// 合并路径数组
		string savePath = CombineJsonPath(paths);
		// 获取文件夹路径
		string directoryName = Path.GetDirectoryName(savePath);
		if (!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(directoryName);
		}
		File.WriteAllText(savePath, text);
	}

	/// <summary>
	/// 根据路径加载文本
	/// </summary>
	/// <param name="paths">路径数组</param>
	/// <returns>Json字符串</returns>
	public static string LoadTextFromFile(params string[] paths)
	{
		string str = File.ReadAllText(CombineJsonPath(paths));
		return str;
	}

	/// <summary>
	///  合并路径
	/// </summary>
	/// <param name="paths">路径数组</param>
	/// <returns>路径</returns>
	static string CombineJsonPath(params string[] paths)
	{
		string pathStr = "";
		for (int i = 0; i < paths.Length; i++)
		{
			pathStr = Path.Combine(pathStr, paths[i]);
		}
		return pathStr;
	}


}
