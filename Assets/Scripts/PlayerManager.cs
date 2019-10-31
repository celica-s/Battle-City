using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDefeated = false;
    public bool isDead = false;
    public GameObject bornPrefab;
    public Text scoreText;
    public Text lifeValueText;
    public GameObject gameoverUI;
    private static PlayerManager instance;

    public static PlayerManager Instance { get; set; }
    void Awake () {
        Instance = this;
        // GameObject born = Instantiate (bornPrefab, new Vector3 (-2, -8, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
        if (isDead) {
            gameoverUI.SetActive (true);
            Invoke ("returnToMainMenu", 3.0f);
            return;
        }
        if (isDefeated) {
            recover ();
        }
        scoreText.text = playerScore.ToString ();
        lifeValueText.text = lifeValue.ToString ();
    }

    private void recover () {
        if (lifeValue <= 0) {
            isDead = true;
        } else {
            lifeValue--;
            GameObject born = Instantiate (bornPrefab, new Vector3 (-2, -8, 0), Quaternion.identity);
            isDefeated = false;
        }
    }

    private void returnToMainMenu () {
        SceneManager.LoadScene ("Start");
    }
}