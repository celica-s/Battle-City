using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed = 10.0f;
    public AudioClip hitAudio;
    public bool isPlayerBullet;

    // Update is called once per frame
    void Update () {
        transform.Translate (Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other) {
        switch (other.tag) {
            case "Tank":
                if (!isPlayerBullet) {
                    other.SendMessage ("die");
                    Destroy (gameObject);
                }
                break;
            case "Base":
                other.SendMessage ("die");
                Destroy (gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet) {
                    other.SendMessage ("die");
                    Destroy (gameObject);
                }
                break;
            case "Wall":
                if (isPlayerBullet) {
                    AudioSource.PlayClipAtPoint (hitAudio, gameObject.transform.position);
                }
                Destroy (other.gameObject);
                Destroy (gameObject);
                break;
            case "Barrier":
                if (isPlayerBullet) {
                    AudioSource.PlayClipAtPoint (hitAudio, gameObject.transform.position);
                }
                Destroy (gameObject);
                break;
            default:
                break;
        }
    }
}