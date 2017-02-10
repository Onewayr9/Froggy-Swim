using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Vector3 screenBoundary;

	// objects
	public GameObject hazard;
	public GameObject gem;
	public GameObject mediPack;


	// medipack arg
	public float spawnMediWaitBase;
	public float spawnMediWait;

	// gem arg
	public Vector2 lengthRange;
	public Vector3 spawnWoodPosition;
//	public Vector3 spawnGemValues;

	public float gemDeltaY;
	public float spawnGemWaitBase;
	public float spawnGemWait;

	// start wait
	public float startWait;

	// wood arg
	public float spawnWoodWaitBase;
	public float spawnWoodWait;
	public float woodWaveWaitBase;

	//public float waveWait;
	public int woodHazardCount;
	public int gemHazardCount;


	void Start() {
		StartCoroutine( SpawnWoodWaves ());
		StartCoroutine (SpawnGemWaves ());
		StartCoroutine (SpawnMedipackWaves ());
	}

	IEnumerator SpawnWoodWaves() {
		yield return new WaitForSeconds (startWait);
		while (true) {
			// woodHazardCounts' hazards in each wave
			for(int i = 0; i < woodHazardCount; i++) {
				// wood, random height
				float var_y = Random.Range (lengthRange.x, lengthRange.y);
				hazard.transform.localScale += new Vector3 (0, var_y, 0);
				float item = Random.Range(0.0f, 1.0f);
				Vector3 spawnPosition = new Vector3 (spawnWoodPosition.x, item > 0.5f ? spawnWoodPosition.y : -spawnWoodPosition.y, spawnWoodPosition.z);
				
				Instantiate(hazard, spawnPosition, Quaternion.identity);
				hazard.transform.localScale += new Vector3 (0, -var_y, 0);

				yield return new WaitForSeconds(spawnWoodWaitBase + Random.Range(0.0f, spawnWoodWait));
			}
			yield return new WaitForSeconds(woodWaveWaitBase);
		}

	}

	IEnumerator SpawnGemWaves() {
		yield return new WaitForSeconds (startWait);
		while (true) {
			float produceGems = Random.Range (0.0f, 1.0f);
			float positionFlag = Random.Range (0.0f, 1.0f);
			float directionFlag = Random.Range (0.0f, 1.0f);
			// produce gem randomly
			// triangle form
			if(produceGems > 0.75f) {
				float gemStartY = positionFlag > 0.5f? screenBoundary.x : positionFlag > 0.25f? 0.0f : screenBoundary.y;
				float deltaY = gemStartY == screenBoundary.x? -gemDeltaY : gemStartY == -5.0f? gemDeltaY : directionFlag > 0.5f? -gemDeltaY : gemDeltaY;
				// spawn gem position in triangle form
				Vector3 spawnGemPosition = new Vector3(0.0f, gemStartY, screenBoundary.z);
				
				for(int j = 0; j < gemHazardCount; j++) {
					Instantiate(gem, spawnGemPosition, Quaternion.identity);
					spawnGemPosition.y += deltaY;
					yield return new WaitForSeconds(spawnGemWaitBase + Random.Range(0.0f, spawnGemWait));
				}
				spawnGemPosition.y += -deltaY * 2;
				// opposite direction
				for(int j = 0; j < gemHazardCount - 1; j++) {
					Instantiate(gem, spawnGemPosition, Quaternion.identity);
					spawnGemPosition.y += -deltaY;
					yield return new WaitForSeconds(spawnGemWaitBase + Random.Range(0.0f, spawnGemWait));
				}
			}
			else if(produceGems > 0.50f) {
				float gemStartY = Random.Range(-1.5f, 1.5f);
				float deltaY = gemDeltaY;
				Vector3 spawnGemPosition = new Vector3(0.0f, gemStartY, screenBoundary.z);

				produceStraightLineGems(gemStartY, deltaY, spawnGemPosition);

				spawnGemPosition.y = gemStartY;
				for(int i = 0; i < gemHazardCount - 2; i++) {
					yield return new WaitForSeconds(spawnGemWait);
					Instantiate(gem, spawnGemPosition, Quaternion.identity);
				}
				yield return new WaitForSeconds(spawnGemWait);

				produceStraightLineGems(gemStartY, deltaY, spawnGemPosition);
			}
			// diamond form
			else if(produceGems > 0.25f) {
				float gemStartY = Random.Range(-1.5f, 1.5f);
				float deltaY = gemDeltaY; // wait to be modified
				// spawn gem position in diamond form
				Vector3 tempGemPosition = new Vector3(0.0f, gemStartY, screenBoundary.z);
				int i;

				Instantiate(gem, tempGemPosition, Quaternion.identity);
				yield return new WaitForSeconds(spawnGemWaitBase + Random.Range(0.0f, spawnGemWait));

				for(i = 1; i < gemHazardCount; i++) {
					tempGemPosition.y += i * deltaY;
					Instantiate(gem, tempGemPosition, Quaternion.identity);
					tempGemPosition.y -= 2 * i * deltaY;
					Instantiate(gem, tempGemPosition, Quaternion.identity);
					tempGemPosition.y = gemStartY;
					yield return new WaitForSeconds(spawnGemWaitBase + Random.Range(0.0f, spawnGemWait));
				}
	
				for(i = gemHazardCount - 2; i >= 1; i--) {
					tempGemPosition.y += i * deltaY;
					Instantiate(gem, tempGemPosition, Quaternion.identity);
					tempGemPosition.y -= 2 * i * deltaY;
					Instantiate(gem, tempGemPosition, Quaternion.identity);
					tempGemPosition.y = gemStartY;
					yield return new WaitForSeconds(spawnGemWaitBase + Random.Range(0.0f, spawnGemWait));
				}

				Instantiate(gem, tempGemPosition, Quaternion.identity);
			}
			yield return new WaitForSeconds(spawnGemWaitBase * 2);
		}

	}

	void produceStraightLineGems(float gemStartY, float deltaY, Vector3 spawnGemPosition) {
		for(int i = 0; i < gemHazardCount - 1; i++) {
			Instantiate(gem, spawnGemPosition, Quaternion.identity);
			spawnGemPosition.y += deltaY;
		}
		spawnGemPosition.y = gemStartY - deltaY;
		for(int i = 1; i < gemHazardCount - 1; i++) {
			Instantiate(gem, spawnGemPosition, Quaternion.identity);
			spawnGemPosition.y += -deltaY;
		}
	}

	IEnumerator SpawnMedipackWaves() {
		yield return new WaitForSeconds (startWait * 3);
		while (true) {
			Vector3 spawnMediPosition = new Vector3(0.0f, Random.Range(screenBoundary.y, screenBoundary.x), screenBoundary.z);

			Instantiate(mediPack, spawnMediPosition, Quaternion.identity);
			yield return new WaitForSeconds(spawnMediWaitBase + Random.Range(0.0f, spawnMediWait));
		}
	}
}
