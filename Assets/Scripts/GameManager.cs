using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	private static GameManager handle;

	public static GameManager Handle { get => handle; set => handle = value; }

	void Awake()
    {
        Handle=this;
	}
}
