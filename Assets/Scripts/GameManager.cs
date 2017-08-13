using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	void Awake()
	{
		if (instance != null) {
			Debug.LogError ("More than 1 GameManager in the scene");
		} else {			
			instance = this;
		}
	}

	public MatchSettings matchSettings;

	#region Player Tracking
	private const string PLAYER_TO_PREFIX = "Player ";


	private static Dictionary<string,Player> players = new Dictionary<string,Player> ();

	public static void RegisterPlayer(string netID,Player player)
	{
		string playerID = PLAYER_TO_PREFIX + netID;
		players.Add (playerID, player);
		player.transform.name = playerID;
	}

	public static void UnRegisterPlayer(string playerID)
	{
		players.Remove (playerID);
	}

	public static Player GetPlayer(string playerID)
	{
		return players [playerID];
	}

//	void OnGUI()
//	{
//		GUILayout.BeginArea (new Rect (200,200,200,500));
//		GUILayout.BeginVertical ();
//
//		foreach(string playerID in players.Keys)
//		{
//			GUILayout.Label (playerID + "  -  " + players [playerID].transform.name);
//		}
//			
//		GUILayout.EndVertical ();
//		GUILayout.EndArea ();
//	}
	#endregion


}
