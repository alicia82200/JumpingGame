using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CAPrintMessageForSecondsAndChangeScene : CustomActionScript {

	public float _seconds;

	public string _message;

	public int _width;

	public int _height;

	private bool _printingMessage = false;

	[Header("Levels To Load")]
	public string nextLevel;

	private int death;
	private int time;

	public int difficulty;

	public struct SavedData
	{
		public string playerLevel;
		public int playerScore;
		public int playerDeath;
		public int playerTime;
	}

	private SavedData playerData;

	public void OnGUI()
	{
		if (_printingMessage)
		{
			GUI.TextArea(new Rect(Screen.width / 2f - _width / 2f, Screen.height / 2f - _height / 2f, _width, _height), _message);
		}
	}

	public override IEnumerator DoActionOnEvent (MonoBehaviour sender, GameObject args)
	{
		_printingMessage = true;
		yield return new WaitForSeconds(_seconds);
		_printingMessage = false;

		playerData = LoadGame();		//load values

		//Get number of death
		GameObject RSzone = GameObject.Find("RespawnZone");			
		death = RSzone.GetComponent<CATeleportTarget>().numDeath();
		//Get current time
		GameObject UIcontrol = GameObject.Find("UIControl");
		time = UIcontrol.GetComponent<UIControl>().getTime();

		//calculate score
		playerData.playerScore = playerData.playerScore + (1000 - (death * (difficulty * 5) + time * 20));
		playerData.playerLevel = nextLevel;
		playerData.playerDeath = 0;
		playerData.playerTime = 0;

		SaveGame(playerData.playerLevel, playerData.playerScore, playerData.playerDeath, playerData.playerTime);
		SceneManager.LoadScene(nextLevel);
	}

	#region LoadingPrefPlayer
	public SavedData LoadGame()
	{
		string serializedObject = PlayerPrefs.GetString("playerProgress");
		SavedData data = JsonUtility.FromJson<SavedData>(serializedObject);
		return data;
	}
	#endregion

	#region saveDataPlayer
	public void SaveGame(string level, int score, int death, int time)
	{
		SavedData data = new SavedData();
		data.playerLevel = level;
		data.playerScore = score;
		data.playerDeath = death;
		data.playerTime = time;
		string serializedObject = JsonUtility.ToJson(data);
		PlayerPrefs.SetString("playerProgress", serializedObject);
	}
	#endregion
}
