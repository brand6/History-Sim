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
        //��ʼ�����ӵ�����
        MapManager.Instance.InitMap(mapW, mapH);
        //��ʼ��������ʾ����
        for (int i = 0; i < mapW; ++i)
		{
            for(int j = 0; j < mapH; ++j)
			{
                GameObject obj = Instantiate(CubePrefab, new Vector3(beginX + offsetX * i, 0, beginY + offsetY * j), Quaternion.identity);
                obj.name = i+"_"+j;
                obj.transform.SetParent(transform);
                // ����Ч���Ӱ���������ֵ䷽���ȡ
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
                    //�������·���������
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
                    //���û�п�ʼ�㣬�����ÿ�ʼ��
                    if (startObj == null)
                    {
                        startObj = obj;
                        obj.GetComponent<MeshRenderer>().material = startMaterial;
                    }
                    //����п�ʼ�㣬�Ҵ���·��������ʾ����ʼ���·��
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
