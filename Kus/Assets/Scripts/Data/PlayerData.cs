using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public float coin;

    public List<int> pos_List;

    public PlayerData(List<int> pos_ID, float diamond)
    {
        pos_List = pos_ID;
        coin = diamond;
    }
}