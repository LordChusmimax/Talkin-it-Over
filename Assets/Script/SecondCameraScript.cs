using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraScript : MonoBehaviour
{

    Camera mainCamera,camera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.parent.GetComponent<Camera>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = 25 +  mainCamera.orthographicSize*0.3f;
    }
}
