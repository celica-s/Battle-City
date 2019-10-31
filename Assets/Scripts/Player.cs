using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private Sprite[] tankSprite;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject defendPrefab;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] tankAudio;
    private Vector3 bulletEulerAngles;
    private SpriteRenderer spriteRenderer;
    private float timeVal;
    private float defendTimeVal = 3.0f;
    private bool isDefended;

    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        audioSource = GetComponent<AudioSource> ();
        isDefended = true;
    }

    void Update () {
        if (isDefended) {
            defendPrefab.SetActive (true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0) {
                isDefended = false;
                defendPrefab.SetActive (false);
            }
        }

    }

    void FixedUpdate () {
        if (PlayerManager.Instance.isDead) {
            return;
        }
        move ();
        if (timeVal >= 0.4f) {
            attack ();
        } else {
            timeVal += Time.fixedDeltaTime;
        }
    }

    private void move () {
        Vector3 movement = Vector3.zero;

        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");

        if (v > 0) {
            spriteRenderer.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3 (0, 0, 0);
        } else if (v < 0) {
            spriteRenderer.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3 (0, 0, 180);
        }

        if (v != 0) {
            movement.y = v * Time.fixedDeltaTime * speed;
            // audioSource.clip = tankAudio[1];
            // if (!audioSource.isPlaying) {
            //     audioSource.Play ();
            // }
        } else {
            if (h < 0) {
                spriteRenderer.sprite = tankSprite[3];
                bulletEulerAngles = new Vector3 (0, 0, 90);
            } else if (h > 0) {
                spriteRenderer.sprite = tankSprite[1];
                bulletEulerAngles = new Vector3 (0, 0, -90);
            }
            if (h != 0) {
                movement.x = h * Time.fixedDeltaTime * speed;
                // audioSource.clip = tankAudio[1];
                // if (!audioSource.isPlaying) {
                //     audioSource.Play ();
                // }
            }

        }

        // if (movement == Vector3.zero) {
        //     audioSource.clip = tankAudio[0];
        //     if (!audioSource.isPlaying) {
        //         audioSource.Play ();
        //     }
        // }

        transform.Translate (movement);
    }

    private void attack () {
        // Debug.Log("Fire");
        if (Input.GetKey (KeyCode.Space)) {
            // Debug.Log("Fire");
            Instantiate (bulletPrefab, transform.position, Quaternion.Euler (transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    private void die () {
        if (isDefended) {
            return;
        }
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        Destroy (gameObject);
        PlayerManager.Instance.isDefeated = true;
    }
}