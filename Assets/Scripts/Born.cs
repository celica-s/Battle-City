using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {
    public GameObject playerPrefab;
    void Start () {
        Invoke ("bornPlayer", 0.8f);
        Destroy (gameObject, 0.8f);
    }

    private void bornPlayer () {
        Instantiate (playerPrefab, transform.position, Quaternion.identity);
    }
}