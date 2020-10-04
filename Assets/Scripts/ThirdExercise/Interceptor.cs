using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThirdExercise
{
    public class Interceptor : MonoBehaviour
    {
        public List<Missile> _missiles;
        public Transform _currentTarget;
        public bool _isMoving = true;

        private void Start()
        {
            var missiles = FindObjectsOfType<Missile>().ToList();
            _missiles = missiles.OrderBy(missile => missile.distance).ToList();
            FindNewTarget();
            _missiles.ForEach(missile => missile.onTargetReached += RemoveFromList);
        }

        void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position,
                    10 * Time.deltaTime);
            }

            if (_missiles.Count == 0) _isMoving = false;
        }

        private void OnDestroy()
        {
            _missiles.ForEach(missile => missile.onTargetReached -= RemoveFromList);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
            _missiles.RemoveAt(0);
            _currentTarget = null;
            FindNewTarget();
        }

        private void FindNewTarget()
        {
            if (_missiles.Count == 0) return;
            _currentTarget = _missiles[0].transform;
            _isMoving = true;
        }

        private void RemoveFromList(Missile missile)
        {
            if (missile == _missiles[0])
            {
                _isMoving = false;
                _currentTarget = null;
                if(_missiles.Count > 0) 
                    FindNewTarget();
            }

            _missiles.Remove(missile);
        }
    }
}