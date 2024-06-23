using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public Transform _plateform;
    public Transform _startPoint;
    public Transform _endPoint;
    public float _speed = 1.5f;
    int _direction = 1;

    private void Update()
    {
        Vector2 target = _currentMovementTraget();

        _plateform.position = Vector2.Lerp(_plateform.position, target, _speed * Time.deltaTime);

        float distance = (target - (Vector2)_plateform.position).magnitude;

        if (distance <= 0.1f)
        {
            _direction *= -1;
        }
    }

    Vector2 _currentMovementTraget()
    {
        if (_direction == 1)
        {
            return _startPoint.position;
        }
        else
        {
            return _endPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if(_plateform!=null && _startPoint != null && _endPoint)
        {
            Gizmos.DrawLine(_plateform.transform.position, _startPoint.position);
            Gizmos.DrawLine(_plateform.transform.position, _endPoint.position);
        }
    }


}
