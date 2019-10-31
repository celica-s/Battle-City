using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
    public GameObject[] items;

    private List<Vector3> list = new List<Vector3> ();
    void Awake () {
        createItem (items[0], new Vector3 (0, -8, 0), Quaternion.identity);
        createItem (items[1], new Vector3 (-1, -8, 0), Quaternion.identity);
        createItem (items[1], new Vector3 (1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++) {
            createItem (items[1], new Vector3 (i, -7, 0), Quaternion.identity);
        }

        createItem (items[1], new Vector3 (1, -8, 0), Quaternion.identity);

        for (int i = 0; i < 3; i++) {
            createItem (items[5], createRandomPosition (), Quaternion.identity);
        }

        for (int i = 0; i < 20; i++) {
            createItem (items[1], createRandomPosition (), Quaternion.identity);
            createItem (items[2], createRandomPosition (), Quaternion.identity);
            createItem (items[3], createRandomPosition (), Quaternion.identity);
            createItem (items[4], createRandomPosition (), Quaternion.identity);
        }
    }

    private Vector3 createRandomPosition () {
        while (true) {
            Vector3 position = Vector3.zero;
            position.x = Random.Range (-9, 10);
            position.y = Random.Range (-7, 8);
            if (hasPosition (position)) {
                return position;
            }
        }
    }

    private bool hasPosition (Vector3 position) {
        foreach (Vector3 item in list) {
            if (position == item) {
                return false;
            }
        }
        return true;
    }

    private void createItem (GameObject createdObject, Vector3 position, Quaternion rotation) {
        GameObject item = Instantiate (createdObject, position, rotation);
        item.transform.SetParent (gameObject.transform);
        list.Add (position);
    }
}