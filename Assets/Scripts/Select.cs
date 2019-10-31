using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour {
    public Transform pos1;
    public Transform pos2;
    private int selection = 1;
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.W)) {
            selection = 1;
            transform.position = pos1.position;
        } else if (Input.GetKeyDown (KeyCode.S)) {
            selection = 2;
            transform.position = pos2.position;
        }

        if (selection == 1 && Input.GetKeyDown (KeyCode.Space)) {
            SceneManager.LoadScene("Game");
        }
    }
}