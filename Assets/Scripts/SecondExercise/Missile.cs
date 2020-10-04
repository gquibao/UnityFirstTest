using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SecondExercise
{
    public class Missile : MonoBehaviour
    {
        public Vector3 targetPosition = new Vector3(45, 30, 400);
        private int _startSpeed = 5;
        private int _speed;
        private bool _isMoving = true;
        private Coroutine _changeSpeed;

        private void Start()
        {
            _speed = _startSpeed;
            _changeSpeed = StartCoroutine(ChangeSpeed());
        }

        private void Update()
        {
            if(_isMoving)
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
            if(_changeSpeed != null)
                StopCoroutine(_changeSpeed);
        }

        private IEnumerator ChangeSpeed()
        {
            var random = Random.Range(0.2f, 0.81f);
            yield return new WaitForSeconds(random);
            _speed = _startSpeed + Random.Range(-2, 3);
            _changeSpeed = StartCoroutine(ChangeSpeed());
        }
    }
}
