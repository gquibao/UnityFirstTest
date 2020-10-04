using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ThirdExercise
{
    public class Missile : MonoBehaviour
    {
        public Vector3 targetPosition = new Vector3(45, 30, 400);
        public float distance;
        public Action<Missile> onTargetReached;
        private Vector3 _startingPosition;
        private int _startSpeed = 150;
        private int _speed;
        private bool _isMoving = true;
        private Coroutine _changeSpeed;

        private void Awake()
        {
            _startingPosition = GetPositionInRange();
            distance = Vector3.Distance(_startingPosition, Vector3.zero);
        }

        private void Start()
        {
            _startingPosition = GetPositionInRange();
            distance = Vector3.Distance(_startingPosition, Vector3.zero);
            transform.position = _startingPosition;
            _speed = _startSpeed;
            _changeSpeed = StartCoroutine(ChangeSpeed());
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            }

            if (transform.position == targetPosition)
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            onTargetReached.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interceptor")) return;
            _isMoving = false;
            if (_changeSpeed != null)
                StopCoroutine(_changeSpeed);
        }

        private Vector3 GetPositionInRange()
        {
            var randomPosition = Vector3.zero;
            while (randomPosition == Vector3.zero)
            {
                var x = Random.Range(-1, 2);
                var y = Random.Range(-1, 2);
                var z = Random.Range(-1, 2);
                randomPosition = Vector3.Normalize(new Vector3(x, y, z));
            }
            var positionInRange = randomPosition * Random.Range(200, 1001);
            return positionInRange;
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
