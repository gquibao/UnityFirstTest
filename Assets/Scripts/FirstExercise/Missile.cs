using System;
using UnityEngine;

namespace FirstExercise
{
    public class Missile : MonoBehaviour
    {
        public Vector3 targetPosition = new Vector3(45, 30, 400);
        private bool _isMoving = true;

        private void Update()
        {
            if(_isMoving)
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _isMoving = false;
        }
    }
}
