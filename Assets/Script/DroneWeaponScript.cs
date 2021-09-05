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
    [SerializeField] protected float cadence;
    public LayerMask layerMask;
    public GameObject bulletGameObject;
    public Transform hole;

    public float dispersion;
    public float range;

    private float currentCd;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentCd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RotationCorrection();
        LaserScript();
        ShootScript();
        HandleCd();
    }

    void RotationCorrection()
    {
        var dirVec = (Vector3)Target.transform.position - transform.position;
        var dirAngle = math.atan2(dirVec.y, dirVec.x) * 180 / math.PI;


        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, dirAngle), 0.5f);
    }

    void LaserScript()
    {
        var direction = new Vector2(Mathf.Cos(laser.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(laser.eulerAngles.z * Mathf.PI / 180));

        hit2D = Physics2D.Raycast(laser.position, direction, Mathf.Infinity, layerMask);

        var endLaser = hit2D.point;

        if (hit2D.rigidbody == null)
        {
            endLaser = new Vector2(laser.position.x + direction.x * 100, laser.position.y + direction.y * 100);
        }
        lineRenderer.SetPosition(0, laser.position);
        lineRenderer.SetPosition(1, endLaser);
    }

    void ShootScript()
    {
        if (hit2D.rigidbody != null)
        {
            if (hit2D.rigidbody.gameObject.layer >= 8 && hit2D.rigidbody.gameObject.layer <= 12)
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                Shoot();
            }
            else
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
        }
    }

    public void Shoot()
    {
        if (currentCd <= 0)
        {
            currentCd = cadence;
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        var bulletDispersion = (UnityEngine.Random.Range(-dispersion, dispersion));
        var bullet = Instantiate(bulletGameObject);
        bullet.transform.position = hole.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.Rotate(bulletDispersion * Vector3.forward);

        var bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.range = range;
        bulletScript.faceLeft = true;
        bulletScript.gameObject.layer = gameObject.layer;
        bulletScript.enabled = true;
    }


    public void HandleCd()
    {
        if (currentCd > 0)
        {
            currentCd -= Time.deltaTime;
        }
    }

    public void Die()
    {
        lineRenderer.enabled = false;
        this.enabled = false;
    }
}
