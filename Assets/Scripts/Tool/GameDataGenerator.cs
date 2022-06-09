using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataGenerator : MonoBehaviour
{
	private static GameDataGenerator handle;
	public static GameDataGenerator Handle { get => handle; set => handle = value; }

	private Citys _citys;
	private Roles _roles;
	private Blocs _blocs;

	private void Awake()
	{
		Handle = this;
		_citys = JsonTools.loadJsonFileToObj<Citys>(Application.dataPath, "Data", "citys.json");
		_roles = JsonTools.loadJsonFileToObj<Roles>(Application.dataPath, "Data", "roles.json");
		_blocs = JsonTools.loadJsonFileToObj<Blocs>(Application.dataPath, "Data", "blocs.json");
	}

	public Citys GetCitys()
	{
		return _citys;
	}

	public Roles GetRoles()
	{
		return _roles;
	}

	public Blocs GetBlocs()
	{
		return _blocs;
	}
}
