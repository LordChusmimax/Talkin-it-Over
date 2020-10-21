using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    [SerializeField] protected float cadence;
    [SerializeField] private bool aim;
    protected float currentCd;   
    [HideInInspector]public bool faceLeft;
    protected Quaternion rotationHandling;
    protected Vector3 positionHandling;
    protected Vector3 scaleHandling;

    public bool Aim { get => aim; set => aim = value; }

    public virtual void Update()
    {
        HandleCd();
    }

    public virtual void onPick()
    {
        transform.localPosition = positionHandling;
        transform.localRotation = rotationHandling;
        transform.localScale = scaleHandling;
    }

    public abstract void Shoot();

    public void HandleCd()
    {
        if (currentCd > 0)
        {
            currentCd -= Time.deltaTime;
        }
    }

    public virtual void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }
}