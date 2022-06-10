using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType
{
	walk, //可行走
	stop  //不可行走
}

public class MapCube
{
	//坐标
	public int x;
	public int y;
	public CubeType type;
	/// <summary>
	///  构造函数
	/// </summary>
	/// <param name="x">横坐标</param>
	/// <param name="y">纵坐标</param>
	/// <param name="type">枚举类型CubeType</param>
	public MapCube(int x, int y, CubeType type)
	{
		this.x = x;
		this.y = y;
		this.type = type;
	}

	private int f; // 寻路消耗
	private int g; // 离起点距离
	private int h; // 离终点距离
	private MapCube father; // 前一格

	public int F { get => g+h; set => f = value; }
	public int G { get => g; set => g = value; }
	public int H { get => h; set => h = value; }
	public MapCube Father { get => father; set => father = value; }

	
}
  
