using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager {

    public ManagerStatus status {
        get;
        private set;
    }

    public int curLevel {
        get;
        private set;
    }

    public int maxLevel {
        get;
        private set;
    }

    private NetworkService _networkService;

    public void Startup(NetworkService networkService) {
        Debug.Log("Mission manager started ...");
        _networkService = networkService;
        UpdateData(0, 2);
        status = ManagerStatus.Started;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToNext() {
        if(curLevel < maxLevel) {
            curLevel++;
            string name = "Level" + curLevel;
            Debug.Log("Loading " + name);
            SceneManager.LoadScene(name);
        } else {
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void ReachObjective() {
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void RestartCurrent() {
        string name = "Level" + curLevel;
        Debug.Log("Loading " + name);
        SceneManager.LoadScene(name);
    }

    public void UpdateData(int curLevel, int maxLevel){
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }

}
