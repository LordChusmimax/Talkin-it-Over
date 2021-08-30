using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkScript : MonoBehaviour
{
    public GameObject BeamV;

    SpriteRenderer spriteRenderer,beamHSprite;
    GameObject beamH;
    Transform beamHTransform;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        beamH = transform.parent.gameObject;
        beamHSprite = beamH.GetComponent<SpriteRenderer>();
        beamHTransform = beamH.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
