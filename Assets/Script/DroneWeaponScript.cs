using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class DroneWeaponScript : MonoBehaviour
{

    public GameObject Target;
    private LineRenderer lineRenderer;
    private RaycastHit2D hit2D;
    public Transform laser;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var dirVec = (Vector3)Target.transform.position - transform.position;
        var dirAngle = math.atan2(dirVec.y, dirVec.x) * 180 / math.PI;
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, dirAngle);

        hit2D = Physics2D.Raycast(laser.position, new Vector2(Mathf.Cos(laser.eulerAngles.z * Mathf.PI / 180) , Mathf.Sin(laser.eulerAngles.z * Mathf.PI / 180)), Mathf.Infinity);
        lineRenderer.SetPosition(0, laser.position);
        lineRenderer.SetPosition(1, hit2D.point);
    }
}
