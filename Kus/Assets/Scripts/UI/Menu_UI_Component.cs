using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI_Component : MonoBehaviour {

    public void Active_Refercene_Process()
    {
        FindObjectOfType<StateManager>().Active_Refercene_Process();
    }

    public void Disable_Refercene_Process()
    {
        FindObjectOfType<StateManager>().Disable_Refercene_Process();
    }
}
