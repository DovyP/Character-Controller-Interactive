using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSway : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float amount = 0.02f;
    [SerializeField] private float maxAmount = 0.06f;
    [SerializeField] private float smoothAmount = 6f;

    [Header("Rotation")]
    [SerializeField] private float rotationAmount = 4f;
    [SerializeField] private float maxRotationAmount = 5f;
    [SerializeField] private float smoothRotation = 12f;

    [Space]
    [SerializeField] private bool rotationX = true;
    [SerializeField] private bool rotationY = true;
    [SerializeField] private bool rotationZ = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float InputX;
    private float InputY;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        InputX = -Input.GetAxis("Mouse X");
        InputY = -Input.GetAxis("Mouse Y");
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(InputX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(InputY * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(InputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(InputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, Time.deltaTime * smoothRotation);
    }
}
