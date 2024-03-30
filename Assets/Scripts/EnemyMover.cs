
using UnityEngine;

[RequireComponent (typeof(Animator))]

public class EnemyMover : MonoBehaviour
{
    public readonly int isRun = Animator.StringToHash(nameof(isRun));

    private Transform _wayPoint;
    private Transform[] _wayPoints;
    private Transform _target;
    private float _speed = 3f;
    private int _targetpoint;
    private float _waitTime = 0f;
    private float _startWaitTime=2f;
    private Animator _animator;
    private bool _isLeft = false;
    private bool _isFollow = false;
    private bool _isPatrol = true;
    private float _checkPoint = 0.2f;
    private float _rotate = 180f;

    private void Start()
    {
        _wayPoints = new Transform[_wayPoint.childCount];

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = _wayPoint.GetChild(i);
        }

        GetWayPoint();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isFollow)
        {
            TrackTarget(_target.transform);
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            Animate(true);
        }
        else if(_isPatrol)
        {
            TrackTarget(_wayPoints[_targetpoint]);
            transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_targetpoint].position, _speed * Time.deltaTime);
            Animate(true);

            if (Vector2.Distance(transform.position, _wayPoints[_targetpoint].position) < _checkPoint)
            {
                if (_waitTime <= 0)
                {
                    GetWayPoint();
                }
                else
                {
                    Animate(false);
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }

    public void SetWaypoint(Transform wayPoint)
    {
        _wayPoint = wayPoint;
    }

    public void SetDirection(bool isFollow, bool isPatrol, Transform target)
    {
        _isFollow = isFollow;
        _isPatrol = isPatrol;
        _target = target;
    }

    private void Animate(bool IsRun)
    {
        _animator.SetBool(isRun, IsRun);
    }

    private void TrackTarget(Transform target)
    {
        if (transform.position.x > target.position.x && _isLeft || transform.position.x < target.position.x && _isLeft == false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isLeft = !_isLeft;
        transform.Rotate(0f, _rotate, 0f);
    }

    private void GetWayPoint()
    {
        _targetpoint = Random.Range(0, _wayPoints.Length);
        TrackTarget(_wayPoints[_targetpoint]);
        _waitTime = _startWaitTime;
    }
}
