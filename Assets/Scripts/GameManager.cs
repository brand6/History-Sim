using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	private static GameManager handle;
	public GameObject CityInfo;
	private CityData cityData;	

	public static GameManager Handle { get => handle; set => handle = value; }

	void Awake()
    {
        Handle=this;
		cityData = CityInfo.GetComponentInChildren<CityData>();

	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (EventSystem.current.IsPointerOverGameObject()== false)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if( Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("City")))
				{
					cityData.setData(hit.collider.GetComponent<CityName>().CityId);
					CityInfo.transform.position = hit.transform.position + new Vector3(4,1,0);
					CityInfo.SetActive(true);
				}
				else
				{
					CityInfo.SetActive(false);
				}
				
			}
		}
	}


}
