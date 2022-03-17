using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float sideMovementSensitivity;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sidespeed;

    [SerializeField] private Rigidbody rb;

    private Plane plane;

    private float posDifference;
    private float sideMovementTarget = 0;

    
    private Vector2 inputDrag;

    private Vector3 startPos;

    public bool IsStop;
    #endregion

    #region SINGLETON
    private static PlayerController instance;
    public static PlayerController Instance => instance ??= FindObjectOfType<PlayerController>();
    #endregion
    private void Start()
    {
        plane = new Plane(transform.position, Vector3.up);
        startPos = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                hitPoint = new Vector3(Mathf.Clamp(hitPoint.x, -1.35f, 1.35f), 0, 0);
                posDifference = hitPoint.x - transform.position.x;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            posDifference = 0;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.GameStarted)
        {
            if (!IsStop)
            {
                HandleMovement();
            }
            else 
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    public void StopTrigger()
    {
        if (IsStop)
        {
            IsStop = false;
        }
    }
    private void HandleMovement()
    {
        sideMovementTarget += inputDrag.x * sideMovementSensitivity;
        rb.velocity = (posDifference * sidespeed * Vector3.right / Time.fixedDeltaTime) + transform.forward * forwardSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stop"))
        {
            IsStop = true;
            rb.velocity = Vector3.zero;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Finishline"))
        {
            rb.velocity = Vector3.zero;
            GameManager.Instance.OnLevelEnd(true);
        }
    }
    public void ResetPlayer()
    {
        transform.position = startPos;
        IsStop = false;
    }
}
