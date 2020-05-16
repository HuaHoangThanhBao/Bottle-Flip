using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "New Food")]
public class Bottle : ScriptableObject {

    public string food_Name;

    public Sprite food_Image;

    public bool owned;
}
