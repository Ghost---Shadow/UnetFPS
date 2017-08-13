using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour 
{

	[SerializeField]
	private Camera cam;

	private float cameraRotationX = 0f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Rigidbody rb;
	Vector3 thrusterForce  = Vector3.zero;
	[SerializeField]
	private float cameraRotationLimit = 85f;

	float currentCameraRotation = 0f;
	

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void ApplyThrustForce(Vector3 _thrust)
	{
		thrusterForce = _thrust;
	}

	public void RotateCamera(float _cameraRotationX)
	{
		cameraRotationX = _cameraRotationX;
	}
	void FixedUpdate()
	{
		PerformMovement ();
		PerformRotation ();
	}

	void PerformMovement()
	{
		if (velocity != Vector3.zero) 
		{
			rb.MovePosition (transform.position + velocity * Time.deltaTime);
		}
		if (thrusterForce != Vector3.zero) 
		{
			rb.AddForce (thrusterForce*Time.fixedDeltaTime,ForceMode.Acceleration);
		}
	}

	void PerformRotation()
	{
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if (cam != null) {
			currentCameraRotation -= cameraRotationX;
			currentCameraRotation = Mathf.Clamp (currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);
			cam.transform.localEulerAngles = new Vector3 (currentCameraRotation, 0f, 0f);
		}
	}


}
