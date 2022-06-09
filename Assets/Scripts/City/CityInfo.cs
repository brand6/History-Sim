using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CityInfo : MonoBehaviour
{
    public int CityId;
	public GameObject cityInfoUI;
	public Text nameText;

	private City city;
	private CityData cityData;

    // Start is called before the first frame update
    void Start()
    {
		city = GameDataGenerator.Handle.GetCitys().GetCity(CityId);
		nameText.text = city.name;
		cityData = GetComponentInChildren<CityData>();
	}

	private void OnMouseEnter()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			cityData.setData(city);
			cityInfoUI.transform.position = transform.position + new Vector3(4, 1, 0);
			cityInfoUI.SetActive(true);
		}
		
	}

	private void OnMouseExit()
	{
		cityInfoUI.SetActive(false);
	}
}
