﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float drag = 5;
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 scanPos;

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    // Start is called before the first frame update
    void Start()
    {
        scanPos = transform.position;

        _sensitivity = 40f;
        _rotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;
        // store mouse
        _mouseReference = transform.position;

        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        Debug.Log(screenPoint);
        offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        offset.z += drag;

        SlingshotManager.instance.aimer.eulerAngles = new Vector3(90, 0, 0);
        //SlingshotManager.instance.setPath(true);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        _isRotating = false;

        var direction = scanPos - transform.position;
        Debug.Log(direction);
        SlingshotManager.instance.throwBall(direction);
        Invoke("ResetDirection", 1f);
        transform.position = scanPos;
    }

    void ResetDirection()
    {
        SlingshotManager.instance.aimer.eulerAngles = new Vector3(90, 0, 0);
        //SlingshotManager.instance.setPath(false);
        SlingshotManager.instance.ObjectHolder.GetComponent<Collider>().enabled = true;
    }

    void SetDirection()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (transform.position - _mouseReference);

            // apply rotation
            _rotation.x = (_mouseOffset.y) * _sensitivity;
            _rotation.z = (_mouseOffset.x) * _sensitivity;

            // rotate
            SlingshotManager.instance.aimer.Rotate(new Vector3(_rotation.x, 0f, 0f), Space.World);
            SlingshotManager.instance.aimer.Rotate(new Vector3(0f, _rotation.y, _rotation.z), Space.Self);

            // store mouse
            _mouseReference = transform.position;

        }
    }
}

