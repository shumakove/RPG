using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	private List<GameObject> _enemies;
    [SerializeField] public int enemyCount;

	private void Start() {
        loadEnemies();
	}
	private void Update() {
		
	}
	void loadEnemies() {
        Vector3[] positions = new Vector3[4];
        positions[0] = GameObject.FindWithTag("point1").transform.position;
        positions[1] = GameObject.FindWithTag("point2").transform.position;
        positions[2] = GameObject.FindWithTag("point3").transform.position;
        positions[3] = GameObject.FindWithTag("point4").transform.position;
        _enemies = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++) {
            _enemies.Add((GameObject)Instantiate(enemyPrefab));
            _enemies[i].transform.position = 
                positions[Random.Range(0,positions.Length -1)];
            float angle = Random.Range(0, 360);
            _enemies[i].transform.Rotate(0, angle, 0);
        }

    }
}
