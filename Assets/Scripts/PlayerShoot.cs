using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	LayerMask mask;

	void Start()
	{
		if (cam == null) {
			Debug.Log ("PlayerShoot : No Camera reference ");
			this.enabled = false;
		}
	}

	void Update()
	{
		if (Input.GetButtonDown ("Fire1")) 
		{
			Shoot ();	
		}
	}

	[Client]
	void Shoot()
	{
		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,weapon.range,mask)) {
			if (hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot (hit.collider.name,weapon.damage);
			}
		}
	}

	[Command]
	void CmdPlayerShot(string _playerID,int _damage)
	{
		Debug.Log (_playerID + " Has been SHOT");
		Player _player =  GameManager.GetPlayer (_playerID);
		_player.RpcTakeDamage (_damage);

	}
}
