using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : BaseManager<MapManager>
{
	private int mapW; // 地图宽
	private int mapH; // 地图高

	public MapCube[,] cubes;
	private List<MapCube> toSearchList = new List<MapCube>(); //存储待查找格子(能到达的边缘格子)
	private List<MapCube> searchedList = new List<MapCube>();//存储已查找完毕的格子
	private List<MapCube> path = new List<MapCube>();

	private LinkedList<MapCube> toSearchLinkedList = new LinkedList<MapCube>();
	private LinkedList<MapCube> searchedLinkedList = new LinkedList<MapCube>();
	private LinkedListNode<MapCube> node;

	/// <summary>
	/// 初始化地图
	/// </summary>
	/// <param name="w">地图宽</param>
	/// <param name="h">地图高</param>
	public void InitMap(int w, int h)
	{
		mapW = w;
		mapH = h;
		cubes = new MapCube[w, h];
		for (int i = 0; i < w; ++i)
		{
			for (int j = 0; j < h; ++j)
			{
				// 生成随机阻挡
				cubes[i, j] = new MapCube(i, j, Random.Range(0,100) < 10 ? CubeType.stop : CubeType.walk);
			}
		}
	}

	/// <summary>
	/// 寻路方法-使用List
	/// </summary>
	/// <param name="startCube"></param>
	/// <param name="endCube"></param>
	/// <returns>路径列表</returns>
	public List<MapCube> FindPath(Vector2 startPos, Vector2 endPos)
	{
		MapCube startCube = cubes[(int) startPos.x, (int) startPos.y];
		MapCube endCube = cubes[(int)endPos.x, (int)endPos.y];
		
		// 判断开始点和结束点是否合法（是否可通行，是否能到达）
		if (!IsCubeAvailable(startCube.x, startCube.y) || !IsCubeAvailable(endCube.x, endCube.y)) return null;

		path.Clear();
		toSearchList.Clear();
		searchedList.Clear();

		startCube.G = 0;
		startCube.H = GetDistance(startCube, endCube);
		toSearchList.Add(startCube);
		//从起点开始找四周的点，判断是否在待查找和查找完毕列表内
		while (toSearchList.Count > 0)
		{
			MapCube dealCube = toSearchList[toSearchList.Count-1];
			toSearchList.RemoveAt(toSearchList.Count - 1);
			searchedList.Add(dealCube);
			//如果是终点，查找结束
			if (dealCube == endCube)
			{
				break;
			}

			// 将周围格子按序加入待检测列表
			List<MapCube> neighbors = GetNeighbor(dealCube);
			foreach (MapCube n in neighbors){
				if (!toSearchList.Contains(n) && !searchedList.Contains(n))
				{
					n.G = dealCube.G + GetDistance(n, startCube);
					n.H = GetDistance(n, endCube);
					n.Father = dealCube;
					toSearchList.Insert(getInsertIndex(toSearchList,n), n);					
				}
			}
		}

		//通过溯源生成路径
		if (searchedList[searchedList.Count-1] == endCube)
		{
			MapCube cube = endCube;
			while (cube != startCube)
			{
				path.Add(cube);
				cube = cube.Father;
			}
		}
		path.Reverse();

		return path;
	}

	/// <summary>
	/// 寻路方法-使用LinkedList
	/// </summary>
	/// <param name="startCube"></param>
	/// <param name="endCube"></param>
	/// <returns>路径列表</returns>
	public List<MapCube> FindPath2(Vector2 startPos, Vector2 endPos)
	{
		MapCube startCube = cubes[(int)startPos.x, (int)startPos.y];
		MapCube endCube = cubes[(int)endPos.x, (int)endPos.y];

		// 判断开始点和结束点是否合法（是否可通行，是否能到达）
		if (!IsCubeAvailable(startCube.x, startCube.y) || !IsCubeAvailable(endCube.x, endCube.y)) return null;

		path.Clear();
		toSearchLinkedList.Clear();
		searchedLinkedList.Clear();

		startCube.G = 0;
		startCube.H = GetDistance(startCube, endCube);
		toSearchLinkedList.AddFirst(startCube);
		//从起点开始找四周的点，判断是否在待查找和查找完毕列表内
		while (toSearchLinkedList.Count > 0)
		{
			MapCube dealCube = toSearchLinkedList.First.Value;
			toSearchLinkedList.RemoveFirst();
			searchedLinkedList.AddLast(dealCube);
			//如果是终点，查找结束
			if (dealCube == endCube)
			{
				break;
			}

			// 将周围格子按序加入待检测列表
			List<MapCube> neighbors = GetNeighbor(dealCube);
			foreach (MapCube n in neighbors)
			{
				if (!toSearchLinkedList.Contains(n) && !searchedLinkedList.Contains(n))
				{
					n.G = dealCube.G + GetDistance(n, startCube);
					n.H = GetDistance(n, endCube);
					n.Father = dealCube;
					if (toSearchLinkedList.Count == 0) toSearchLinkedList.AddFirst(n);
					else
					{
						node = toSearchLinkedList.First;
						while (node != null)
						{
							if (n.F < node.Value.F || n.F == node.Value.F && n.H < node.Value.F)
							{
								toSearchLinkedList.AddBefore(node, n);
								break;
							}
							else
							{
								if (node == toSearchLinkedList.Last)
								{
									toSearchLinkedList.AddLast(n);
									break;
								}
							}
							node = node.Next;
						}
					}
				}
			}
		}

		//通过溯源生成路径
		if (searchedLinkedList.Last.Value == endCube)
		{
			MapCube cube = endCube;
			while (cube != startCube)
			{
				path.Add(cube);
				cube = cube.Father;
			}
		}
		path.Reverse();

		return path;
	}


	/// <summary>
	/// 从后向前查找（List从大到小排序）
	/// </summary>
	/// <param name="searchList"></param>
	/// <param name="cube"></param>
	/// <returns></returns>
	private int getInsertIndex(List<MapCube> toSearchList, MapCube n)
	{
		if (toSearchList.Count == 0) return 0;
		else
		{
			for (int i = toSearchList.Count - 1; i >= 0; --i)
			{
				if (n.F < toSearchList[i].F || n.F == toSearchList[i].F && n.H <= toSearchList[i].H)
				{
					return i + 1;
				}
				else if (i == 0) return 0;
			}
		}
		return -1;
	}


	/// <summary>
	/// 二分查找（List从大到小排序）
	/// </summary>
	/// <param name="searchList"></param>
	/// <param name="cube"></param>
	/// <returns></returns>
	private int getInsertIndex2(List<MapCube> toSearchList, MapCube n)
	{
		int firstIndex = 0;
		int lastIndex= toSearchList.Count - 1;
		int i;
		if (toSearchList.Count == 0) return 0;
		else if (n.F < toSearchList[lastIndex].F || n.F == toSearchList[lastIndex].F && n.H <= toSearchList[lastIndex].H) return toSearchList.Count;
		else
		{
			i = (int)Mathf.Ceil((firstIndex + lastIndex) / 2.0f);

			while (lastIndex - lastIndex >1)
			{
				if (n.F < toSearchList[i].F || n.F == toSearchList[i].F && n.H <= toSearchList[i].H)
				{
					firstIndex = i;
				}
				else
				{
					lastIndex = i;
				}
				i = (firstIndex + lastIndex) / 2;
			}
			for (int j = lastIndex; j >= firstIndex; --j)
			{
				if (n.F < toSearchList[j].F || n.F == toSearchList[j].F && n.H <= toSearchList[j].H)
				{
					return j + 1;
				}
				else if (j == firstIndex) return firstIndex;
			}

		}
		return i+1;
	}


	/// <summary>
	/// 获取两个格子的曼哈顿距离
	/// </summary>
	/// <param name="startCube"></param>
	/// <param name="endCube"></param>
	/// <returns></returns>
	private int GetDistance(MapCube cube1, MapCube cube2)
	{
		return Mathf.Abs(cube1.x - cube2.x) + Mathf.Abs(cube1.y - cube2.y);
	}

	/// <summary>
	/// 获取目标格子周围有效的格子
	/// </summary>
	/// <param name="cube"></param>
	/// <returns></returns>
	private List<MapCube> GetNeighbor(MapCube cube)
	{
		List<MapCube> neighbors = new List<MapCube>();
		if (IsCubeAvailable(cube.x-1,cube.y))
		{
			neighbors.Add(cubes[cube.x - 1, cube.y]);
		}
		if (IsCubeAvailable(cube.x + 1, cube.y))
		{
			neighbors.Add(cubes[cube.x + 1, cube.y]);
		}
		if (IsCubeAvailable(cube.x, cube.y-1))
		{
			neighbors.Add(cubes[cube.x, cube.y - 1]);
		}
		if (IsCubeAvailable(cube.x , cube.y+1))
		{
			neighbors.Add(cubes[cube.x, cube.y + 1]);
		}
		return neighbors;
	}

	/// <summary>
	/// 根据坐标判断格子是否有效
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	private bool IsCubeAvailable(int x, int y)
	{
		if (x < 0 || x >= mapW || y < 0 || y >= mapH) return false;
		if (cubes[x, y].type == CubeType.stop) return false;
		return true;
	}
}