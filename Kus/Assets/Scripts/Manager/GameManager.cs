using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    void Awake()
    {
        int index = Random.Range(1, 10);

        SceneManager.LoadScene(index);
    }
}
