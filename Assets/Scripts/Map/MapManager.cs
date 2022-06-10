using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : BaseManager<MapManager>
{
	private int mapW; // ��ͼ��
	private int mapH; // ��ͼ��
	private bool insertTag;

	public MapCube[,] cubes;
	private List<MapCube> toSearchList = new List<MapCube>(); //�洢�����Ҹ���(�ܵ���ı�Ե����)
	private List<MapCube> searchedList = new List<MapCube>();//�洢�Ѳ�����ϵĸ���
	private List<MapCube> path = new List<MapCube>();

	/// <summary>
	/// ��ʼ����ͼ
	/// </summary>
	/// <param name="w">��ͼ��</param>
	/// <param name="h">��ͼ��</param>
	public void InitMap(int w, int h)
	{
		mapW = w;
		mapH = h;
		cubes = new MapCube[w, h];
		for (int i = 0; i < w; ++i)
		{
			for (int j = 0; j < h; ++j)
			{
				// ��������赲
				cubes[i, j] = new MapCube(i, j, Random.Range(0,100) < 10 ? CubeType.stop : CubeType.walk);
			}
		}
	}

	/// <summary>
	/// Ѱ·����
	/// </summary>
	/// <param name="startCube"></param>
	/// <param name="endCube"></param>
	/// <returns>·���б�</returns>
	public List<MapCube> FindPath(Vector2 startPos, Vector2 endPos)
	{
		MapCube startCube = cubes[(int) startPos.x, (int) startPos.y];
		MapCube endCube = cubes[(int)endPos.x, (int)endPos.y];
		
		// �жϿ�ʼ��ͽ������Ƿ�Ϸ����Ƿ��ͨ�У��Ƿ��ܵ��
		if (!IsCubeAvailable(startCube.x, startCube.y) || !IsCubeAvailable(endCube.x, endCube.y)) return null;

		path.Clear();
		toSearchList.Clear();
		searchedList.Clear();

		startCube.G = 0;
		startCube.H = GetDistance(startCube, endCube);
		toSearchList.Add(startCube);
		//����㿪ʼ�����ܵĵ㣬�ж��Ƿ��ڴ����ҺͲ�������б���
		while (toSearchList.Count > 0)
		{
			MapCube dealCube = toSearchList[0];
			toSearchList.RemoveAt(0);
			searchedList.Add(dealCube);
			//������յ㣬���ҽ���
			if (dealCube == endCube)
			{
				break;
			}

			// ����Χ���Ӱ�����������б�
			List<MapCube> neighbors = GetNeighbor(dealCube);
			foreach (MapCube n in neighbors){
				if (!toSearchList.Contains(n) && !searchedList.Contains(n))
				{
					n.G = dealCube.G + GetDistance(n, startCube);
					n.H = GetDistance(n, endCube);
					n.Father = dealCube;
					insertTag = false;
					for (int i = 0; i < toSearchList.Count; ++i)
					{
						if (n.F < toSearchList[i].F || n.F == toSearchList[i].F && n.H < toSearchList[i].H)
						{
							toSearchList.Insert(i, n);
							insertTag = true;
							break;
						}

					}
					if (!insertTag) toSearchList.Add(n);
				}
			}
		}

		//ͨ����Դ����·��
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
	/// ��ȡ�������ӵ������پ���
	/// </summary>
	/// <param name="startCube"></param>
	/// <param name="endCube"></param>
	/// <returns></returns>
	public int GetDistance(MapCube cube1, MapCube cube2)
	{
		return Mathf.Abs(cube1.x - cube2.x) + Mathf.Abs(cube1.y - cube2.y);
	}

	public List<MapCube> GetNeighbor(MapCube cube)
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
	/// ���������жϸ����Ƿ���Ч
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public bool IsCubeAvailable(int x, int y)
	{
		if (x < 0 || x >= mapW || y < 0 || y >= mapH) return false;
		if (cubes[x, y].type == CubeType.stop) return false;
		return true;
	}
}
