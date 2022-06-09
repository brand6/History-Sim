using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Citys
{
	public City[] citys;

	public Citys(City[] citys)
	{
		this.citys = citys;
	}

	public int GetCityNum()
	{
		return citys.Length;
	}

	public City GetCity(int cityId)
	{
		for (int i = 0; i < citys.Length; i++)
		{
			if (cityId == citys[i].id)
			{
				return citys[i];
			}
		}
		return null;
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
	public string[] GetCityInfo()
	{
		string[] strs = new string[5];
		strs[0] = name;
		strs[1] = GameDataGenerator.Handle.GetBlocs().GetBloc( bloc).title;
		strs[2] = GameDataGenerator.Handle.GetRoles().GetRole(mayor).name;
		strs[3] = size.ToString();
		strs[4] = population.ToString();
		return strs;
	}
}

