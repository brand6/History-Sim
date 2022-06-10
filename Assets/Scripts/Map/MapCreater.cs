using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public int beginX = -20;
    public int beginY = -20;
    public int offsetX = 1;
    public int offsetY = 1;

    public int mapW = 40;
    public int mapH = 40;

    public Material defalultMaterial;
    public Material banMaterial;
    public Material startMaterial;
    public Material endMaterial;
    public Material pathMaterial;

    public GameObject CubePrefab;

    private GameObject startObj;
    private List<MapCube> path;

    public Dictionary<string,GameObject> cubeDict = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //初始化格子的数据
        MapManager.Instance.InitMap(mapW, mapH);
        //初始化格子显示对象
        for (int i = 0; i < mapW; ++i)
		{
            for(int j = 0; j < mapH; ++j)
			{
                GameObject obj = Instantiate(CubePrefab, new Vector3(beginX + offsetX * i, 0, beginY + offsetY * j), Quaternion.identity);
                obj.name = i+"_"+j;
                obj.transform.SetParent(transform);
                // 将有效格子按坐标存入字典方便获取
                if (MapManager.Instance.cubes[i, j].type == CubeType.walk) cubeDict.Add(obj.name, obj);
                else obj.GetComponent<MeshRenderer>().material = banMaterial;
            }
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, 1000))
			{
                GameObject obj = hit.collider.gameObject;
                if(obj.GetComponent<MeshRenderer>().material != banMaterial)
				{
                    //如果存在路径，则清空
                    if (path != null)
                    {
                        startObj.GetComponent<MeshRenderer>().material = defalultMaterial;
                        startObj = null;
                        foreach (MapCube cube in path)
                        {
                            string cubeName = cube.x + "_" + cube.y;
                            cubeDict[cubeName].GetComponent<MeshRenderer>().material = defalultMaterial;
                        }
                        path = null;
                    }
                    //如果没有开始点，并设置开始点
                    if (startObj == null)
                    {
                        startObj = obj;
                        obj.GetComponent<MeshRenderer>().material = startMaterial;
                    }
                    //如果有开始点，且存在路径，则显示到开始点的路径
                    else
                    {
                        path = MapManager.Instance.FindPath(GetVector2FromName(startObj.name), GetVector2FromName(obj.name));
                        if (path != null)
                        {
                            foreach (MapCube cube in path)
                            {
                                string cubeName = cube.x + "_" + cube.y;
                                cubeDict[cubeName].GetComponent<MeshRenderer>().material = pathMaterial;
                            }
                            obj.GetComponent<MeshRenderer>().material = endMaterial;
                        }

                    }
                }
            }
		}
    }

    private Vector2 GetVector2FromName(string name)
	{
        Vector2 pos = new Vector2();
        pos.x = float.Parse(name.Split('_')[0]);
        pos.y = float.Parse(name.Split('_')[1]);
        return pos;
	}
}
