using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
	private GameManager gameManager;

// 1
	[UnityTest]
	public IEnumerator AsteroidsMovesAfterSpawned()
	{
		// 2
		GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
		gameManager = gameGameObject.GetComponent<GameManager>();
		AsteroidSpawner asteroidSpawner = gameGameObject.GetComponentInChildren<AsteroidSpawner>();
		// 3
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// 4
		Vector2 initialPos = asteroid.transform.position;
		// 5
		yield return new WaitForSeconds(0.1f);
		// 6
		Assert.AreNotEqual(asteroid.transform.position, initialPos);
		// 7
		Object.Destroy(gameManager.gameObject);
	}
}