using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreater : MonoBehaviour
{
	public Citys citys;

	private void Awake()
	{
		citys = JsonTools.loadJsonFileToObj<Citys>(Application.dataPath, "Data", "Citys.json");
	}
}
