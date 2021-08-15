using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
	private GameObject gameGameObject;
	private GameManager game;
	private AsteroidSpawner asteroidSpawner;
	private Player player;

	[SetUp]
	public void Setup()
	{
		// Makes the game as a GameObject = gameGameObject.
		gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
		// Gets the GameManager, which is on the game.
		game = gameGameObject.GetComponent<GameManager>();
		// Gets the AsteroidSpawner, which is a GameObject in the children of the gameGameObject. 
		asteroidSpawner = gameGameObject.GetComponentInChildren<AsteroidSpawner>();
		// Gets the Player, which is a GameObject in the children of the gameGameObject. 
		player = gameGameObject.GetComponentInChildren<Player>();
	}
	
	[TearDown]
	public void Teardown()
	{
		// Destroy the game after the test so nothing is left in the scene.
		Object.Destroy(game.gameObject);
	}
	
	[UnityTest]
	public IEnumerator AsteroidsMovesAfterSpawned()
	{
		// Spawns one astroid as asteroid.
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// Gets its initial Position as a Vector 2.
		Vector2 initialPos = asteroid.transform.position;
		// Waits one second, because all tests are a coroutine you need to have one return.
		yield return new WaitForSeconds(0.1f);
		// Checks if the new position is different to the old and if so it passes the test.
		Assert.AreNotEqual(asteroid.transform.position, initialPos);
	}
	
	[UnityTest]
	public IEnumerator GameOverOccursOnAsteroidCollision()
	{
		// Get the current amount of lives.
		int initialLives = game.lives;
		// Spawns one astroid as asteroid.
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// Puts the asteroid ontop of the player.
		asteroid.transform.position = player.gameObject.transform.position;
		// Waits 0.1 Seconds, as yes this is a coroutine.
		yield return new WaitForSeconds(0.1f);
		// Checks if the player has lost lives.
		Assert.Less(game.lives, initialLives);
	}
	
	[UnityTest]
	public IEnumerator NewGameRestartsGame()
	{
		// Makes the isGameOver true.
		game.isGameOver = true;
		// Runs the UIElements 'new game'.
		game.NewGame();
		// Checks if the game over is still true.
		Assert.False(game.isGameOver);
		// Returns after done.
		yield return null;
	}
	
	[UnityTest]
	public IEnumerator BulletsMove()
	{
		// Shoots bullet.
		GameObject bullet = player.Shoot();
		// Gets bullet position.
		Vector2 initialYPos = bullet.transform.position;
		// Waits 0.1 Seconds.
		yield return new WaitForSeconds(0.1f);
		// Checks if it now has a different position.
		Assert.AreNotEqual(bullet.transform.position, initialYPos);
	}
	
	[UnityTest]
	public IEnumerator BulletsDestroyAsteroid()
	{
		// Spawns one astroid as asteroid.
		GameObject asteroid = asteroidSpawner.SpawnOneAsteroid();
		// Put asteroid at 0,0.
		asteroid.transform.position = Vector3.zero;
		// Shoots bullet.
		GameObject bullet = player.Shoot();
		// Put bullet at 0,0.
		bullet.transform.position = Vector3.zero;
		// Wait 0.1 of a second.
		yield return new WaitForSeconds(0.1f);
		// Use Unity to check if null.
		UnityEngine.Assertions.Assert.IsNull(asteroid);
	}
}