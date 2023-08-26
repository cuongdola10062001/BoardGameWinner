using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class LineControl : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public List<Chess> listChess = new List<Chess>();

    public Vector3 startPoint;
    public Vector3 endPoint;
    private Vector3 _defaultPoint;
    public float minZ;
    public float maxZ;
    private Vector3 _changePoint;
    private List<Vector3> _linePoints = new List<Vector3>();
    public List<Chess> listChesses = new List<Chess>();

    public Vector3 GetChangePoint()
    {
        return _changePoint;
    }

    public Vector3 GetDefaultPoint()
    {
        return _defaultPoint;
    }

    public void SetChangePoint(Vector3 point)
    {
        _changePoint = new Vector3(point.x, _defaultPoint.y, point.z);
    }

    void Start()
    {
        _defaultPoint = (startPoint + endPoint) * 0.5f;
        _lineRenderer = GetComponent<LineRenderer>();
        _changePoint = _defaultPoint;
        _lineRenderer.enabled = true;
        UpdateLine();
    }

    public void CheckChangePoint()
    {
        _changePoint.x = Mathf.Clamp(_changePoint.x, -5.5f, 5.5f);
        _changePoint.z = Mathf.Clamp(_changePoint.z, minZ, maxZ);
    }

    public void UpdateLine()
    {
        _linePoints.Clear();
        _linePoints.Add(startPoint);
        _linePoints.Add(_changePoint);
        _linePoints.Add(endPoint);

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void ResetLine()
    {
        _changePoint = _defaultPoint;
        UpdateLine();
    }
}
