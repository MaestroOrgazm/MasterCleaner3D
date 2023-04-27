using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _destroyTime;
    [SerializeField] private AudioSource _source;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Robot robot))
        {
            _source.Play();
            robot.SetCount();
            Instantiate(_particleSystem, transform);
            _particleSystem.Play();
            Destroy(this.gameObject, _destroyTime);
        }
    }
}
