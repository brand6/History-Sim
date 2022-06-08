using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonTools 
{
	/// <summary>
	/// �����󱣴�ΪJson�ı�
	/// </summary>
	/// <typeparam name="T">��������</typeparam>
	/// <param name="obj">����</param>
	/// <param name="paths">����·��</param>
	public static void SaveObjAsJsonStr<T>(T obj,params string[] paths)
	{
		string str = ObjToJsonStr(obj);
		SaveTextToFile(str, paths);
	}

	/// <summary>
	/// ��Json�ı���ȡ����
	/// </summary>
	/// <typeparam name="T">��������</typeparam>
	/// <param name="paths">·��</param>
	/// <returns>����</returns>
	public static T loadJsonFileToObj<T>(params string[] paths)
	{
		string jsonStr = LoadTextFromFile(paths);
		return JsonStrToObj<T>(jsonStr);
	}

	/// <summary>
	/// ����תJson�ַ���
	/// </summary>
	/// <typeparam name="T">��������</typeparam>
	/// <param name="target">Ŀ�����</param>
	/// <returns>Json�ַ���</returns>
	public static string ObjToJsonStr<T>(T target)
	{
		return JsonUtility.ToJson(target);
	}

	/// <summary>
	/// Json�ַ���ת����
	/// </summary>
	/// <typeparam name="T">��������</typeparam>
	/// <param name="jsonStr">Json�ַ���</param>
	/// <returns>Ŀ�����</returns>
	public static T JsonStrToObj<T>(string jsonStr)
	{
		return JsonUtility.FromJson<T>(jsonStr);
	}

	/// <summary>
	/// �����ļ�
	/// </summary>
	/// <param name="text">������ı�</param>
	/// <param name="paths">����·��</param>
	public static void SaveTextToFile(string text,params string[] paths)
	{
		// �ϲ�·������
		string savePath = CombineJsonPath(paths);
		// ��ȡ�ļ���·��
		string directoryName = Path.GetDirectoryName(savePath);
		if (!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(directoryName);
		}
		File.WriteAllText(savePath, text);
	}

	/// <summary>
	/// ����·�������ı�
	/// </summary>
	/// <param name="paths">·������</param>
	/// <returns>Json�ַ���</returns>
	public static string LoadTextFromFile(params string[] paths)
	{
		string str = File.ReadAllText(CombineJsonPath(paths));
		return str;
	}

	/// <summary>
	///  �ϲ�·��
	/// </summary>
	/// <param name="paths">·������</param>
	/// <returns>·��</returns>
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
