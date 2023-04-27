using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Robot : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _deltaEnergy = 5f;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _walk;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _lose;

    private int _maxDirt;
    private int _currentDirt = 0;
    private bool _isMove = false;
    private float _dealtaDirection = 100;
    private float _deltaWall = 0.8f;
    private Vector3 _direction;
    private Coroutine _coroutine;

    public UnityAction _finishLevel; 
    public UnityAction _loseLevel;
    public UnityAction<int> _dirtCleaned;

    public float Energy { get; private set; } = 100;

    private void Update()
    {
        Energy -= _deltaEnergy * Time.deltaTime;

        if (Energy <= 0)
        {
            _source.Stop();
            _loseLevel?.Invoke();
            _source.loop = false;
            _source.clip = _lose;
            _source.Play();
        }
    }

    public void Up()
    {
        if (!_isMove)
        {
            _direction = new Vector3(transform.position.x, transform.position.y, transform.position.z + _dealtaDirection);
            _coroutine = StartCoroutine(StartMove(_direction));
        }
    }

    public void Down()
    {
        if (!_isMove)
        {
            _direction = new Vector3(transform.position.x, transform.position.y, transform.position.z - _dealtaDirection);
            _coroutine = StartCoroutine(StartMove(_direction));
        }
    }

    public void Left()
    {
        if (!_isMove)
        {
            _direction = new Vector3(transform.position.x - _dealtaDirection, transform.position.y, transform.position.z);
            _coroutine = StartCoroutine(StartMove(_direction));
        }
    }

    public void Right()
    {
        if (!_isMove)
        {
            _direction = new Vector3(transform.position.x + _dealtaDirection, transform.position.y, transform.position.z);
            _coroutine = StartCoroutine(StartMove(_direction));
        }
    }

    private IEnumerator StartMove(Vector3 direction)
    {
        _source.clip = _walk;
        _source.loop = true;
        _source.Play();
        _isMove = true;

        while (_isMove)
        {
            transform.LookAt(direction);

            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, _deltaWall);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Wall wall))
                {
                    StopCoroutine(_coroutine);
                    _isMove = false;
                    _source.Stop();
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetValues(int count)
    {
        _maxDirt = count;
    }

    public void SetCount()
    {
        _currentDirt++;
        _dirtCleaned?.Invoke(_currentDirt);

        if (_currentDirt == _maxDirt)
        {
            _source.Stop();
            _finishLevel?.Invoke();
            _source.loop = false;
            _source.clip = _win;
            _source.Play();
        }
    }
}
