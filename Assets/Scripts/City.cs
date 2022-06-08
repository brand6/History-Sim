using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Citys
{
	public List<City> citys;
}

[System.Serializable]
public class City
{
	public int cityId;
	public string cityName;
	public int size;
	public int population;
	public int bloc;
	public int mayor;

	public City(int cityId, string cityName, int size, int population, int bloc, int mayor)
	{
		this.cityId = cityId;
		this.cityName = cityName;
		this.size = size;
		this.population = population;
		this.bloc = bloc;
		this.mayor = mayor;
	}
}
