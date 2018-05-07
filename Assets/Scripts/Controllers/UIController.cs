using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Text healthLabel;
    [SerializeField]
    private InventoryPopup popup;
    [SerializeField]
    private Text levelEnding;

	private void Awake() {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
	}

	// Use this for initialization
	void Start () {
        OnHealthUpdated();
        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.M)) {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
	}

	private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
	}

    void OnHealthUpdated() {
        string healthText = "Health: " + Managers.Player.health
                                                  + "/"
                                                  + Managers.Player.maxHealth;
        healthLabel.text = healthText;
    }

    private void OnLevelComplete() {
        StartCoroutine(CompleteLevel());
    }

    private void OnLevelFailed() {
        StartCoroutine(FailLevel());
    }

    private IEnumerator CompleteLevel() {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Complete!";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private IEnumerator FailLevel() {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);

        Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }
}
