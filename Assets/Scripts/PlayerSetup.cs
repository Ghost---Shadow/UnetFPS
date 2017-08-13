using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour 
{
	[SerializeField]
	Behaviour[] componentToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start()
	{
		if (!isLocalPlayer) 
		{
			DisableComponent ();
			AssignRemoteLayer ();
		} 
		else 
		{
			sceneCamera = Camera.main;

			if (sceneCamera != null) 
			{
				sceneCamera.gameObject.SetActive (false);
			}
		}
		GetComponent<Player> ().Setup ();
		RegisterPlayer ();
	
	}

	public override void OnStartClient()
	{
		base.OnStartClient ();

		string netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player player = GetComponent<Player> ();

		GameManager.RegisterPlayer (netID, player);
	}

	void RegisterPlayer()
	{
		string id = "Player " + GetComponent<NetworkIdentity> ().netId;
		transform.name = id;
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponent()
	{
		foreach (Behaviour comp in componentToDisable) 
		{
			comp.enabled = false;
		}
	}

	void OnDisable()
	{
		if (sceneCamera != null) 
		{
			sceneCamera.gameObject.SetActive (true);
		}
		GameManager.UnRegisterPlayer (transform.name);
	}

}
