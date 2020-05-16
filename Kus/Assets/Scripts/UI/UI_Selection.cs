using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selection : MonoBehaviour {

    Informaion_Handler info_Handler;

    void Start()
    {
        info_Handler = FindObjectOfType<Informaion_Handler>();
    }

    public void OnSelection()
    {
        foreach (Bottle item in info_Handler.food_Obj)
        {
            if (transform.GetChild(0).GetChild(0).gameObject.activeSelf)
            {
                if (item.food_Image == transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite)
                {
                    info_Handler.Change_Model_Sprite(item.food_Image, item.name);

                    FindObjectOfType<StateManager>().Change_Food_Models(item.food_Name);

                    SaveSystem.Save_Food_Selection(item.food_Name);

                    break;
                }
            }
        }
    }
}
