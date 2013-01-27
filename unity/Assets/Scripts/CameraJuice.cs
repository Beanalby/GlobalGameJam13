using UnityEngine;
using System.Collections;

public class CameraJuice : MonoBehaviour {

    private Camera cam;
    private float startSize, midSize;
    private float moveDuration;
    private float moveStart=-1;

    Vector3 startPos;
    private float shakeAmount = 5;
    private float shakeDuration;
    private float shakeStart = -1;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        // +++ debugging
        if (Input.GetKeyDown(KeyCode.Alpha1))
            BeatGood();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            BeatFair();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            BeatBad();

        UpdateMove();
        UpdateShake();
    }
    void UpdateMove()
    {
        if (moveStart == -1)
            return;
        if (Time.time - moveStart > moveDuration * 2)
        {
            cam.orthographicSize = startSize;
            moveStart = -1;
        }
        else if (Time.time - moveStart > moveDuration)
        {
            cam.orthographicSize = Mathf.Lerp(midSize, startSize, (Time.time - moveStart - moveDuration) / moveDuration);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(startSize, midSize, (Time.time - moveStart) / moveDuration);
        }
    }
    void UpdateShake()
    {
        if (shakeStart == -1)
            return;
        if (Time.time - shakeStart > shakeDuration)
        {
            transform.position = startPos;
            shakeStart = -1;
        }
        else
        {
            float percent = (Time.time - shakeStart) / shakeDuration;
            float amount = shakeAmount * (1-percent);
            if (transform.position.x > startPos.x)
            {
                transform.position = new Vector3(startPos.x - amount, startPos.y, startPos.z);
            }
            else
            {
                transform.position = new Vector3(startPos.x + amount, startPos.y, startPos.z);
            }
        }
    }
    public void BeatGood()
    {
        if (moveStart != -1 || shakeStart != -1)
            return;
        startSize = cam.orthographicSize;
        midSize = startSize -1;
        moveDuration = .1f;
        moveStart = Time.time;
    }
    public void BeatFair()
    {
        if (moveStart != -1 || shakeStart != -1)
            return;
        startSize = cam.orthographicSize;
        midSize = startSize - .5f;
        moveDuration = .1f;
        moveStart = Time.time;

        startPos = transform.position;
        shakeAmount = .5f;
        shakeDuration = .2f;
        shakeStart = Time.time;
    }
    public void BeatBad()
    {
        if (moveStart != -1 || shakeStart != -1)
            return;

        startPos = transform.position;
        shakeAmount = 3f;
        shakeDuration = .25f;
        shakeStart = Time.time;
    }
}
