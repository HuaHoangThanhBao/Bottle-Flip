using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UI_Component {

    public string food_Currrent_Selection_Name;

    public UI_Component(string name)
    {
        food_Currrent_Selection_Name = name;
    }
}
