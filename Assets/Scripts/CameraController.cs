using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    private Vector3 _currentVelocity;
    private Camera _camera;

    public List<GameObject> targets;
    public Vector3 offset;
    public float smoothness = 0.5f;
    public float minZoom = 5f;
    public float maxZoom = 10f;

    private void Awake()
    {
        targets = GameObject.FindGameObjectsWithTag("Player").ToList();
        _camera = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        if(targets.Count == 0)
            return;
        
        Move();
        Zoom();
    }

    private void Zoom()
    {
        var zoomValue = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 10f);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomValue,Time.deltaTime);
    }

    private void Move()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _currentVelocity, smoothness);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);

        foreach (var target in targets)
            bounds.Encapsulate(target.transform.position);

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].transform.position;

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);

        foreach (var target in targets)
            bounds.Encapsulate(target.transform.position);

        return bounds.center;
    }
}
