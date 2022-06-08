using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Citys
{
	public City[] citys;

	public Citys(City[] citys)
	{
		this.citys = citys;
	}
}

[System.Serializable]
public class City
{
	public int id;
	public string name;
	public int size;
	public int population;
	public int bloc;
	public int mayor;

	public City(int id, string name, int size, int population, int bloc, int mayor)
	{
		this.id = id;
		this.name = name;
		this.size = size;
		this.population = population;
		this.bloc = bloc;
		this.mayor = mayor;
	}
}

