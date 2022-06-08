using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreater : MonoBehaviour
{
	public Citys citys;
	public List<City> cityList = new List<City>();

	private void Awake()
	{
		citys = JsonTools.loadJsonFileToObj<Citys>(Application.dataPath, "Data", "City.json");
	}
}
