using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
	private GameManager game;

	[UnityTest]
	public IEnumerator AsteroidsMovesAfterSpawned()
	{
		// Makes the game as a GameObject = gameGameObject.
		GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
		// Gets the GameManager, which is on the game.
		game = gameGameObject.GetComponent<GameManager>();
		// Gets the AsteroidSpawner, which is a GameObject in the children of the gameGameObject. 
		AsteroidSpawner asteroidSpawner = gameGameObject.GetComponentInChildren<AsteroidSpawner>();
		// Spawns one astroid as asteroid.
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// Gets its initial Position as a Vector 2.
		Vector2 initialPos = asteroid.transform.position;
		// Waits one second, because all tests are a coroutine you need to have one return.
		yield return new WaitForSeconds(0.1f);
		// Checks if the new position is different to the old and if so it passes the test.
		Assert.AreNotEqual(asteroid.transform.position, initialPos);
		// Destroy the game after the test so nothing is left in the scene.
		Object.Destroy(game.gameObject);
	}
	
	[UnityTest]
	public IEnumerator GameOverOccursOnAsteroidCollision()
	{
		// Makes the game as a GameObject = gameGameObject.
		GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
		// Gets the GameManager, which is on the game.
		game = gameGameObject.GetComponent<GameManager>();
		// Get the current amount of lives.
		int initialLives = game.lives;
		// Gets the AsteroidSpawner, which is a GameObject in the children of the gameGameObject. 
		AsteroidSpawner asteroidSpawner = gameGameObject.GetComponentInChildren<AsteroidSpawner>();
		// Gets the AsteroidSpawner, which is a GameObject in the children of the gameGameObject. 
		Player player = gameGameObject.GetComponentInChildren<Player>();
		// Spawns one astroid as asteroid.
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// Puts the asteroid ontop of the player.
		asteroid.transform.position = player.gameObject.transform.position;
		// Waits 0.1 Seconds, as yes this is a coroutine.
		yield return new WaitForSeconds(0.1f);
		// Checks if the player has lost lives.
		Assert.Less(game.lives, initialLives);
		// Tears down the scene so there is nothing left between tests.
		Object.Destroy(game.gameObject);
	}
}