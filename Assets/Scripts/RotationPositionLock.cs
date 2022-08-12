using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for locking the postiion and rotation of a gameobject on specific axies
// Based off https://stackoverflow.com/a/67257857
public class RotationPositionLock : MonoBehaviour
{
    [Header("Position")]
    public bool lockXPosition;
    public bool lockYPosition;
    public bool lockZPosition;

    [Header("Rotation")]
    public bool lockXRotation;
    public bool lockYRotation;
    public bool lockZRotation;

    private Vector3 startPosition;
    private Vector3 startRotation;


    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }
    void LateUpdate()
    {
        // Update positition
        Vector3 newPosition = transform.position;
        transform.position = new Vector3(
            lockXPosition ? startPosition.x : newPosition.x,
            lockYPosition ? startPosition.y : newPosition.y,
            lockZPosition ? startPosition.z : newPosition.z
        );

        // Update rotation
        Vector3 newRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(
            lockXRotation ? startRotation.x : newRotation.x,
            lockYRotation ? startRotation.y : newRotation.y,
            lockZRotation ? startRotation.z : newRotation.z
        );
    }
}