﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour {

    [SerializeField] private Slider progressBar;

    private void Awake() {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS,
                                       OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnManagersProgress(int numReady, int numModules) {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }

    public void OnManagersStarted() {
        Managers.Mission.GoToNext();
    }

    private void OnDestroy() {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS,
                                       OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }
}
