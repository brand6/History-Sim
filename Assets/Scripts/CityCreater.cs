using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreater : MonoBehaviour
{
	public Citys Citys;

	private void Awake()
	{
		Citys = JsonTools.loadJsonFileToObj<Citys>(Application.dataPath, "Data", "citys.json");
	}
}
