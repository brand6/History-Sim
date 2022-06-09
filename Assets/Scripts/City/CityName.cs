using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityName : MonoBehaviour
{
    public int CityId;
    public Text cityText;

    // Start is called before the first frame update
    void Start()
    {
        cityText.text = GameDataGenerator.Handle.GetCitys().GetCity(CityId).name;
    }
}
