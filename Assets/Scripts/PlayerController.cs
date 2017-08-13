using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;
	private PlayerMotor motor;

	[Header("Spring Settings : ")]
	[SerializeField]
	private JointDriveMode jointMode = JointDriveMode.Position;
	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;
	[SerializeField]
	private float thrusterForce = 1000f;
	private ConfigurableJoint joint;

	void Start()
	{
		motor = GetComponent<PlayerMotor> ();
		joint = GetComponent<ConfigurableJoint> ();
		SetJointSetting (jointSpring);
	}
	void Update()
	{
		float xMove = Input.GetAxisRaw ("Horizontal");
		float zMove = Input.GetAxisRaw ("Vertical");
		Vector3 moveHorizontal = transform.right * xMove;
		Vector3 moveVertical = transform.forward * zMove;

		Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed; //final movement vector
		motor.Move(velocity);

		//rotation (Turning)
		float yRot =Input.GetAxisRaw("Mouse X");
		Vector3 rotation = new Vector3 (0, yRot, 0)*lookSensitivity;

		motor.Rotate (rotation);

		//Camera rotation
		float xRot =Input.GetAxisRaw("Mouse Y");
		float cameraRotationX = xRot*lookSensitivity;

		motor.RotateCamera (cameraRotationX);

		//Apply thrust force
		Vector3 _thrust = Vector3.zero;
		if (Input.GetButton ("Jump")) {
			_thrust = Vector3.up * thrusterForce;
			SetJointSetting (0f);
		} else {
			SetJointSetting (jointSpring);
		}

		motor.ApplyThrustForce (_thrust);
	}

	private void SetJointSetting(float _jointSpring)
	{
		joint.yDrive = new JointDrive { mode = jointMode, positionSpring = _jointSpring, maximumForce = jointMaxForce };
	}


}