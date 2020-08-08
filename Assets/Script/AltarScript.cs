using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScript : MonoBehaviour
{
    [SerializeField] private GameObject weaponObject;

    public GameObject WeaponObject { get => weaponObject; set => weaponObject = value; }

    public void Interacted()
    {
        Destroy(this.gameObject);
    }
}
