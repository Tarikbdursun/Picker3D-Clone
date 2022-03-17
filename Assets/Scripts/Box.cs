using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField] private GameObject platformPart;

    [SerializeField] private float lastRotValue;

    [SerializeField] private int goal = 3;

    [SerializeField] private TextMeshPro counter;

    private Collider platformPartCollider;

    private float startGateMoveTimer = 0;
    private float startPlatformPartMoveTimer = 0;
    private float startMoveDuration = .5f;

    private float startTimer = 0;
    private float timerDuration = 1.5f;

    private int point = 0;

    private Quaternion startRot;
    private Quaternion leftGateLastRot;
    private Quaternion rightGateLastRot;

    private Vector3 platformPartFirstPos;
    private Vector3 platformPartLastPos;

    private bool openGate;
    private bool startRising;
    private bool timerStarted;
    #endregion

    void Start()
    {
        counter.text = point + " / " + goal;

        platformPartCollider = platformPart.gameObject.GetComponent<Collider>();

        startRot = leftGate.transform.rotation;
        leftGateLastRot = Quaternion.Euler(0, 0, lastRotValue);
        rightGateLastRot = Quaternion.Euler(0, 0, -lastRotValue);

        platformPartFirstPos = platformPart.transform.localPosition;
        platformPartLastPos = Vector3.up;
    }
    private void Update()
    {
        if (openGate)
        {
            GateOpening();
        }
        if (startRising)
        {
            PlatformPartRising();
        }
        if (PlayerController.Instance.IsStop)
        {
            CounterTimer();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            point++;
            collision.gameObject.tag = "tag";
            counter.text = point + " / " + goal;
            StartCoroutine(WaitAndCount());
        }
    }
    private IEnumerator WaitAndCount()
    {
        yield return new WaitForSeconds(1.5f);
        if (point >= goal)
        {
            Invoke("StartPlatformPartRising", 2.5f);
            Invoke("OpenGate", 3.5f);
            UIManager.Instance.SetBar();
        }
        else
        {
            Fail();
        }
    }
    private void OpenGate()
    {
        openGate = true;
    }
    private void StartPlatformPartRising()
    {
        startRising = true;
    }

    private void PlatformPartRising()
    {
        if (startPlatformPartMoveTimer < startMoveDuration)
        {
            startPlatformPartMoveTimer += Time.deltaTime;
            platformPart.SetActive(true);
            platformPartCollider.enabled = false;
            platformPart.transform.localPosition = Vector3.Lerp(platformPartFirstPos, platformPartLastPos, startPlatformPartMoveTimer / startMoveDuration);
        }
        else
        {
            platformPartCollider.enabled = false;
            platformPart.transform.localPosition = platformPartLastPos;
            startRising = false;
        }
    }
    private void GateOpening()
    {
        if (startGateMoveTimer < startMoveDuration)
        {
            startGateMoveTimer += Time.deltaTime;
            leftGate.transform.rotation = Quaternion.Lerp(startRot, leftGateLastRot, startGateMoveTimer / startMoveDuration);
            rightGate.transform.rotation = Quaternion.Lerp(startRot, rightGateLastRot, startGateMoveTimer / startMoveDuration);
        }
        else
        {
            leftGate.transform.rotation = leftGateLastRot;
            rightGate.transform.rotation = rightGateLastRot;
            PlayerController.Instance.StopTrigger();
            openGate = false;
        }
    }
    private void Fail()
    {
        GameManager.Instance.OnLevelEnd(false);
    }
    private void CounterTimer() // if there is no collision between box and collectable
    {
        startTimer += Time.deltaTime;
        if (startTimer >= timerDuration && point == 0)
        {
            Fail();
            startTimer = 0;
            timerStarted = false;
        }
    }
}
