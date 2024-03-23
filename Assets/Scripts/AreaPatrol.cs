
using UnityEngine;

[RequireComponent (typeof(Animator))]

public class AreaPatrol : MonoBehaviour
{
    readonly private string _isRun = "isRun";

    private Transform _wayPoint;
    private Transform[] _wayPoints;
    private Player _player;
    private float _maxVisible = 8f;
    private float _attackDistance = 2f;
    private float _distance;
    private float _speed = 3f;
    private int _targetpoint;
    private float _waitTime = 0f;
    private float _startWaitTime=2f;
    private Animator _animator;
    private bool _isLeft = false;
    private bool _isVisible = false;
    private bool _isAttack = false;
    private float _checkPoint = 0.2f;

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
        SawPlayer();

        if (_isVisible && _isAttack==false)
        {
            TrackTarget(_player.transform);
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            Animate(true);
        }
        else if(_isAttack==false)
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

    public void SetPlayer(Player player, Transform wayPoint)
    {
        _player = player;
        _wayPoint = wayPoint;
    }

    private void Animate(bool isRun)
    {
        _animator.SetBool(_isRun, isRun);
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

        transform.Rotate(0f, 180f, 0f);
    }

    private void GetWayPoint()
    {
        _targetpoint = Random.Range(0, _wayPoints.Length);
        TrackTarget(_wayPoints[_targetpoint]);
        _waitTime = _startWaitTime;
    }

    private void SawPlayer()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);

        if (_distance <= _attackDistance)
            Attack();
        else if (_distance <= _maxVisible)
        {
            _isVisible = true;
            _isAttack = false;
        }
        else
        {
            _isVisible = false;
            _isAttack = false;
        }
    }

    private void Attack()
    {
        _isAttack = true;
    }
}
