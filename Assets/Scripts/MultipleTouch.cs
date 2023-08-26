using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class MultipleTouch : MonoBehaviour
{
    public List<Chess> chessList = new List<Chess>();
    private Vector3 _curScreenPos;

    [SerializeField]
    Area _area1;

    [SerializeField]
    Area _area2;

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            _curScreenPos = new Vector3(t.position.x, t.position.y, 0);

            if (t.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(_curScreenPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.CompareTag("Chess"))
                    {
                        Chess newChess = hitObject.GetComponent<Chess>();
                        newChess.id = t.fingerId;
                        newChess.isOnClick = true;
                        chessList.Add(newChess);
                    }
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Chess thisChess = chessList.Find(Chess => Chess.id == t.fingerId);

                if (thisChess == null)
                    return;

                thisChess.SetIsDragging(false);
                thisChess.isOnClick = false;
                //thisChess.RemoveListChess();
                if (thisChess.GetIsCollisingLine() )
                {
                    thisChess.Fire();
                }

                chessList.RemoveAt(chessList.IndexOf(thisChess));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Chess thisChess = chessList.Find(Chess => Chess.id == t.fingerId);

                if (thisChess == null)
                    return;

                thisChess.SetIsDragging(true);
                Vector3 objectPos = thisChess.transform.position;

                Vector3 newPosition = GetTouchPosition(objectPos, _curScreenPos);
                if (thisChess.isArea1)
                {
                    newPosition.x = Mathf.Clamp(
                        newPosition.x,
                        _area1.minLimmitedX,
                        _area1.maxLimmitedX
                    );
                    newPosition.z = Mathf.Clamp(
                        newPosition.z,
                        _area1.minLimmitedZ,
                        _area1.maxLimmitedZ
                    );
                }
                else if (thisChess.isArea2)
                {
                    newPosition.x = Mathf.Clamp(
                        newPosition.x,
                        _area2.minLimmitedX,
                        _area2.maxLimmitedX
                    );
                    newPosition.z = Mathf.Clamp(
                        newPosition.z,
                        _area2.minLimmitedZ,
                        _area2.maxLimmitedZ
                    );
                }

                thisChess.transform.position = newPosition;
            }
            ++i;
        }
    }

    Vector3 GetTouchPosition(Vector3 touchPosition, Vector3 curScreenPos)
    {
        float z = Camera.main.WorldToScreenPoint(touchPosition).z;

        return Camera.main.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
    }
}
