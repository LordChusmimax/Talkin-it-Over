using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScript : MonoBehaviour
{
    [SerializeField] private GameObject weaponObject;

    [SerializeField]private bool renewable;
    [SerializeField] private float renewTime;
    protected AudioSource pickUpSound;

    public GameObject WeaponObject { get => weaponObject; set => weaponObject = value; }
    private BoxCollider2D collider;
    private SpriteRenderer weaponSprite;
    private SpriteRenderer doorSprite;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        var childObjects = GetComponentsInChildren<SpriteRenderer>();
        weaponSprite = childObjects[1];
        doorSprite = childObjects[2];
        pickUpSound = GetComponent<AudioSource>();
    }

    public void Interacted()
    {
        pickUpSound.Play();
        weaponSprite.enabled = false;
        doorSprite.enabled = true;
        collider.enabled = false;
        if (renewable)
        {
            StartCoroutine(Renew());
        }
    }

    private IEnumerator Renew() {
        yield return new WaitForSeconds(renewTime*0.75f);
        weaponSprite.enabled = true;
        yield return new WaitForSeconds(renewTime * 0.25f);
        doorSprite.enabled = false;
        collider.enabled = true;
        
    }

    
}
