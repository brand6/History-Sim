using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType
{
	walk, //������
	stop  //��������
}

public class MapCube
{
	//����
	public int x;
	public int y;
	public CubeType type;
	/// <summary>
	///  ���캯��
	/// </summary>
	/// <param name="x">������</param>
	/// <param name="y">������</param>
	/// <param name="type">ö������CubeType</param>
	public MapCube(int x, int y, CubeType type)
	{
		this.x = x;
		this.y = y;
		this.type = type;
	}

	private int f; // Ѱ·����
	private int g; // ��������
	private int h; // ���յ����
	private MapCube father; // ǰһ��

	public int F { get => g+h; set => f = value; }
	public int G { get => g; set => g = value; }
	public int H { get => h; set => h = value; }
	public MapCube Father { get => father; set => father = value; }

	
}
  
