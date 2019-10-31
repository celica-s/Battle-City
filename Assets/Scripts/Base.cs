using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    [SerializeField]
    private Sprite broken;
    [SerializeField]
    private GameObject explosionPrefab;
    public AudioClip dieAudio;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void die () {
        spriteRenderer.sprite = broken;
        Instantiate (explosionPrefab, transform.position, Quaternion.identity);
        PlayerManager.Instance.isDead = true;
        AudioSource.PlayClipAtPoint (dieAudio, transform.position);
    }
}