/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class Shop : MonoBehaviour
{
    public string objectName;
    public int price, access;
    public GameObject block;
    public Text objectPrice;


    void Awake()
    {
        access = PlayerPrefs.GetInt(objectName + "Access");
        objectPrice.text = price.ToString();

        if (access == 1)
        {
            block.SetActive(false);
            objectPrice.gameObject.SetActive(false);
        }
    }



    void OnButtonDown()
    {
        int coins = PlayerPrefs.GetInt("coins");
        if(access==0)
        {
            if (coins>=price)
            {
                PlayerPrefs.SetInt(objectName + "Access", 1);
                PlayerPrefs.SetInt("coins", coins - price);
            }
        }
    }
}
*/ 