using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SmokeMethod());
    }

    IEnumerator SmokeMethod()
    {
        var grad = 1 / sprites.Length;
        for (int i = 1; i < sprites.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.sprite = sprites[i];
            spriteRenderer.color = new Color(1,1,1,1-grad*i);
        }
        Destroy(gameObject);
    }

}
