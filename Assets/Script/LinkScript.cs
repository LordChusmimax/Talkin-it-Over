using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class LinkScript : MonoBehaviour
{
    public GameObject Targer;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var dirVec = (Vector3)Targer.transform.position - transform.position;
        var dirAngle = math.atan2(dirVec.y, dirVec.x) * 180 / math.PI;
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, dirAngle-90);

        var distance = Vector2.Distance(Targer.transform.position, transform.position)*20;
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, distance);
    }
}
