using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Chess : MonoBehaviour
{
    public int id;
    public bool isOnClick = false;
    private bool _isDragging;

    public bool GetIsDragging()
    {
        return _isDragging;
    }

    public void SetIsDragging(bool state)
    {
        _isDragging = state;
    }

    [SerializeField]
    Area _area1;
    public bool isArea1;

    [SerializeField]
    Area _area2;
    public bool isArea2;

    private bool _isCollisingLine = false;

    public List<ParticleSystem> particleSystems;
    private int countPar;
    private ParticleSystem _particleSystem;
    private Vector3 _spawnPointTop = new Vector3(0, 1, 3.5f);
    private Vector3 _spawnPointBottom = new Vector3(0, 1, -3.5f);

    public LineControl lineControl;

    [SerializeField]
    float _forceShoot = 10f;
    Rigidbody _rb;
    float _rateForce = 1f;

    public bool GetIsCollisingLine()
    {
        return _isCollisingLine;
    }

    private void Start()
    {
        countPar = particleSystems.Count;
        _rb = GetComponent<Rigidbody>();

        if (_area1 != null && _area2 != null)
        {
            isArea1 = _area1.CheckArea(transform.position);
            if (isArea1)
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                }
                transform.SetParent(_area1.transform);
            }
            isArea2 = _area2.CheckArea(transform.position);
            if (isArea2)
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                }
                transform.SetParent(_area2.transform);
            }
        }
    }

    private void Update()
    {
        if (_area1 != null && _area2 != null)
        {
            isArea1 = _area1.CheckArea(transform.position);
            if (isArea1)
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                }
                transform.SetParent(_area1.gameObject.transform);
            }
            isArea2 = _area2.CheckArea(transform.position);
            if (isArea2)
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                }
                transform.SetParent(_area2.gameObject.transform);
            }

            if (_area1.transform.childCount == 0)
            {
                GUIManager.Ins.SetNamePlayerWin("Player 01");
                GUIManager.Ins.gamewinDialog.Show(true);
            }
            else if (_area2.transform.childCount == 0)
            {
                GUIManager.Ins.SetNamePlayerWin("Player 02");
                GUIManager.Ins.gamewinDialog.Show(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            _isCollisingLine = true;
            lineControl = other.gameObject.GetComponent<LineControl>();
            if (_isDragging || (isOnClick && _isCollisingLine))
            {
                lineControl.SetChangePoint(transform.position);
                lineControl.CheckChangePoint();
                if (lineControl.GetChangePoint().z < 0)
                {
                    _rateForce =
                        (lineControl.GetChangePoint().z - lineControl.maxZ)
                        / (-lineControl.maxZ + lineControl.minZ);
                }
                else if (lineControl.GetChangePoint().z > 0)
                {
                    _rateForce =
                        (lineControl.GetChangePoint().z - lineControl.minZ)
                        / (-lineControl.maxZ + lineControl.minZ);
                }
                lineControl.UpdateLine();
            } 
       
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Line") && _isDragging)
        {
            //  other.gameObject.GetComponent<LineControl>().listChess.Add(this);
           // AddListChess();
        }

        if (_area1 == null && _area2 == null)
            return;
        if (other.gameObject.CompareTag("LineCheck"))
        {
            if (_rateForce < 0)
            {
                int ranInt = Random.Range(0, countPar);
                ParticleSystem newPar = particleSystems[ranInt];
                _particleSystem = Instantiate(newPar, _spawnPointTop, Quaternion.identity);
                Destroy(_particleSystem.gameObject, 3f);
            }
            else if (_rateForce > 0)
            {
                int ranInt = Random.Range(0, countPar);
                ParticleSystem newPar = particleSystems[ranInt];
                _particleSystem = Instantiate(newPar, _spawnPointBottom, Quaternion.identity);
                Destroy(_particleSystem.gameObject, 3f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Line") || !_isDragging)
        {
            _isCollisingLine = false;
            lineControl = other.gameObject.GetComponent<LineControl>();
       /*     lineControl.listChess.Remove(this);*/
             if(lineControl != null )
            {
                lineControl.ResetLine();
            }
            
            //    other.gameObject.GetComponent<LineControl>().listChess.Remove(this);
        }
    }

    public void Fire()
    {
        
        if (_rb == null)
            return;
        /*if (lineControl && lineControl.listChess[0].id==this.id)
        {
            _rb.AddForce(Vector3.forward * _forceShoot * _rateForce, ForceMode.Impulse);
        }*/
       _rb.AddForce(Vector3.forward * _forceShoot * _rateForce, ForceMode.Impulse);
    }

    public void AddListChess()
    {
        if(lineControl)
        {
            lineControl.listChess.Add(this);
        }
    }

    public void RemoveListChess()
    {
        if (lineControl)
        {
            lineControl.listChess.Remove(this);
        }
    }
}
