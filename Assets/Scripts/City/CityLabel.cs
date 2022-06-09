using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityLabel : MonoBehaviour
{
    private string[] textList = { "城市名", "势力", "太守", "规模", "人口" };
    private Text labelText;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
		{
            labelText = transform.GetChild(i).gameObject.GetComponent<Text>();
            labelText.text = textList[i];
        }
    }

}
