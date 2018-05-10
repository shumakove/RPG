using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager {

    public ManagerStatus status {
        get;
        private set;
    }

    private string _filename;
    private NetworkService _network;

    public void Startup(NetworkService networkService) {
        Debug.Log("Data manager starting ...");
        _network = networkService;
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }

    public void SaveGameState() {
        Dictionary<string, object> gameState = new Dictionary<string, object>();
        gameState.Add("inventory", Managers.Inventory.GetData());
        gameState.Add("health", Managers.Player.health);
        gameState.Add("maxHealth", Managers.Player.maxHealth);
        gameState.Add("curLevel", Managers.Mission.curLevel);
        gameState.Add("maxLevel", Managers.Mission.maxLevel);

        FileStream file = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, gameState);
        file.Close();
    }

    public void LoadGameState() {
        if(!File.Exists(_filename)) {
            Debug.Log("No saved game");
            return;
        }
        Dictionary<string, object> gameState;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(_filename, FileMode.Open);
        gameState = formatter.Deserialize(file) as Dictionary<string, object>;
        file.Close();

        Managers.Inventory.UpdateData((Dictionary<string, int>)gameState["inventory"]);
        Managers.Player.UpdateData((int)gameState["health"], (int)gameState["maxHealth"]);
        Managers.Mission.UpdateData((int)gameState["curLevel"], (int)gameState["maxLevel"]);
        Managers.Mission.RestartCurrent();
    }

    // Use this for initialization
    void Start () {
		
	}
	
    void Update() {
        
    }
}
