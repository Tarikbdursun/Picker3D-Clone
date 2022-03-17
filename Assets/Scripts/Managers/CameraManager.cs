using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject target;
    //resetleme kodu yazýlacak örnek: 
    private Vector3 startPos;
    //transform.position = vector3.zero
    private void Start()
    {
        startPos = mainCamera.transform.localPosition;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(0, transform.position.y, target.transform.position.z);
    }

    private void ResetCamera() 
    {
        transform.position = Vector3.zero;
    }
}
