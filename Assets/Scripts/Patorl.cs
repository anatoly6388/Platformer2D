
using UnityEngine;

public class Patorl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _groundDetection;

    private bool _isLeft = true;
    private int _rotate = 180;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            Animate(true);
            RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);

            if (groundInfo.collider == false)
            {
                if (_isLeft == true)
                {
                    transform.eulerAngles = new Vector3(0, _rotate, 0);
                    _isLeft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    _isLeft = true;
                }
            }
    }

    private void Animate(bool isRun)
    {
        _animator.SetBool("isRun", isRun);
    }
}
