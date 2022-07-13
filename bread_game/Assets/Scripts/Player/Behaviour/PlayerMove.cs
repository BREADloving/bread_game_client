using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bread
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rigidbody = null;
		[SerializeField] private float m_speed = 0;
		[SerializeField] private float m_rotationAngle = 0;
		[SerializeField] private float m_jumpForce = 0;
		[SerializeField] private Animator m_animator = null;

		private Vector3 UpDirection => Vector3.up;
		private Vector3 ForwardDirection => Vector3.forward;
		private Vector3 BackDirection => Vector3.back;

		private bool m_jumpFlab = false;

		public void Update()
		{
			var velocity = Vector3.zero;
			if (Input.GetKey(KeyCode.W)) velocity += CulcVelocity(ForwardDirection);
			if (Input.GetKey(KeyCode.S)) velocity += CulcVelocity(BackDirection);

			if (Input.GetKey(KeyCode.A)) Rotation(m_rotationAngle);
			if (Input.GetKey(KeyCode.D)) Rotation(-m_rotationAngle);

			if (Input.GetKeyDown(KeyCode.Space)) Jump();

			m_rigidbody.AddForce(velocity, ForceMode.Force);
			m_animator.SetFloat("Speed", m_rigidbody.velocity.magnitude);
			m_animator.SetBool("Jump", m_jumpFlab);
			m_jumpFlab = false;
		}

		private Vector3 CulcVelocity(Vector3 localDirection)
        {
			var direction = GetWorldDirection(localDirection);

			return direction * m_speed;
        }

		private Vector3 GetWorldDirection(Vector3 localDiretion)
		{
			return transform.rotation * localDiretion;
		}

		private void Rotation(float angle)
		{
			var rot = Quaternion.AngleAxis(angle, UpDirection);
			transform.rotation *= rot;
		}

		private void Jump()
		{
			m_rigidbody.AddForce(UpDirection * m_jumpForce, ForceMode.Force);
			m_jumpFlab = true;
		}
	}
}