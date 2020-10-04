using System;
using UnityEngine;

namespace SecondExercise
{
    public class Interceptor : MonoBehaviour
    {
        private GameObject _missile;
        private bool _isMoving = true;

        private void Start()
        {
            _missile = FindObjectOfType<Missile>().gameObject;
        }

        void Update()
        {
            if(_isMoving)
                transform.position = Vector3.MoveTowards(transform.position, _missile.transform.position, 10 * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
        }
    }
}
