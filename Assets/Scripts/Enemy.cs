using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private Sprite[] tankSprite;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject explosionPrefab;
    private Vector3 bulletEulerAngles;
    private SpriteRenderer spriteRenderer;
    private float timeVal;
    private float moveTime;
    private float h = 0;
    private float v = -1;

    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    void FixedUpdate () {
        if (PlayerManager.Instance.isDead) {
            return;
        }
        move ();
        if (timeVal >= 3.0f) {
            attack ();
        } else {
            timeVal += Time.fixedDeltaTime;
        }
    }

    private void move () {
        Vector3 movement = Vector3.zero;
        if (moveTime > 4.0f) {
            int num = Random.Range (0, 8);
            switch (num) {
                case 0:
                    h = 0;
                    v = 1;
                    break;
                case 1:
                case 2:
                    h = -1;
                    v = 0;
                    break;
                case 3:
                case 4:
                    h = 1;
                    v = 0;
                    break;
                default:
                    h = 0;
                    v = -1;
                    break;
            }

            moveTime = 0;
        } else {
            moveTime += Time.fixedDeltaTime;
        }

        if (h < 0) {
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3 (0, 0, 90);
        } else if (h > 0) {
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3 (0, 0, -90);
        }

        if (h != 0) {
            movement.x = h * Time.fixedDeltaTime * speed;
        } else {
            if (v > 0) {
                spriteRenderer.sprite = tankSprite[0];
                bulletEulerAngles = new Vector3 (0, 0, 0);
            } else if (v < 0) {
                spriteRenderer.sprite = tankSprite[2];
                bulletEulerAngles = new Vector3 (0, 0, 180);
            }
            movement.y = v * Time.fixedDeltaTime * speed;
        }

        transform.Translate (movement);
    }

    private void attack () {
        Instantiate (bulletPrefab, transform.position, Quaternion.Euler (transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    private void die () {
        PlayerManager.Instance.playerScore++;
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        Destroy (gameObject);
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            timeVal = 4;
        }
    }
}