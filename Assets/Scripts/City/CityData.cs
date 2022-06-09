using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityData : MonoBehaviour
{
	private Text dataText;
	
	public void setData(int cityId)
	{
		City city = GameDataGenerator.Handle.GetCitys().GetCity(cityId);
		string[] cityInfo = city.GetCityInfo();
		for (int i = 0; i < transform.childCount; i++)
		{
			dataText = transform.GetChild(i).GetComponent<Text>();
			dataText.text = cityInfo[i];
		}
	}
}
