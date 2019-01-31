using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]                              
    private Transform[] enemyPrefabs;       //iteretator = 0 for the wisp, 1 for the spider, 2 for the bat eye and 3 for the skull
    private static bool waitingFinished;    //true when the waiting before each wave finishes
    private static int numberOfEnemies;
    private List<Transform> spawnPoints;    //iterator = 0, 1 or 2 for wisp's spawn point, 3, 4 or 5 for spider's spawn point, 6, 7 or 8 for bat eye's spawn point and 9 for skull's spawn point

    public static int NumberOfEnemies       //when this property is set to 0, the next wave begins
    {
        get
        {
            return numberOfEnemies;
        }
        set
        {
            numberOfEnemies = value;
            if (numberOfEnemies == 0)
            {
                if (Wave > -1 && Wave < 9)
                    Wave++;
                else
                    Wave = -1;
                waitingFinished = false;
            }
        }
    }

    public static int Wave { get; private set; }

    private void Start()
    {
        waitingFinished = false;
        NumberOfEnemies = 0;
        Wave = 0;
        spawnPoints = new List<Transform>();
        spawnPoints = transform.GetChildren();
    }

    private void Update()
    {
        if(Wave <= 0)
            StartCoroutine(Wait(0.5f));
        else
            StartCoroutine(Wait(2f));
        if (NumberOfEnemies == 0 && waitingFinished)
            switch(Wave)
            {
                case 0:
                    Spawn(0, 0);
                    Spawn(0, 2);
                    NumberOfEnemies = 2;
                    break;
                case 1:
                    Spawn(0, 0);
                    Spawn(0, 1);
                    Spawn(0, 2);
                    NumberOfEnemies = 3;
                    break;
                case 2:
                    Spawn(0, 0);
                    Spawn(1, 4);
                    Spawn(0, 2);
                    NumberOfEnemies = 3;
                    break;
                case 3:
                    Spawn(1, 3);
                    Spawn(0, 1);
                    Spawn(1, 5);
                    NumberOfEnemies = 3;
                    break;
                case 4:
                    Spawn(1, 3);
                    Spawn(1, 4);
                    Spawn(1, 5);
                    NumberOfEnemies = 3;
                    break;
                case 5:
                    Spawn(0, 0);
                    Spawn(0, 1);
                    Spawn(0, 2);
                    Spawn(2, 7);
                    NumberOfEnemies = 4;
                    break;
                case 6:
                    Spawn(0, 0);
                    Spawn(0, 1);
                    Spawn(0, 2);
                    Spawn(2, 6);
                    Spawn(2, 8);
                    NumberOfEnemies = 5;
                    break;
                case 7:
                    Spawn(0, 0);
                    Spawn(0, 1);
                    Spawn(0, 2);
                    Spawn(2, 6);
                    Spawn(2, 8);
                    Spawn(1, 4);
                    NumberOfEnemies = 6;
                    break;
                case 8:
                    Spawn(0, 0);
                    Spawn(0, 1);
                    Spawn(0, 2);
                    Spawn(2, 6);
                    Spawn(2, 8);
                    Spawn(1, 3);
                    Spawn(1, 5);
                    NumberOfEnemies = 7;
                    break;
                case 9:
                    Spawn(3, 9);
                    NumberOfEnemies = 1;
                    break;
                case -1:
                    GameObject.Find("Pause Menu").GetComponent<PauseMenu>().QuitToMenu();
                    break;
            }
    }

    private void Spawn(int prefabID, int spawnPointID)
    {
        Transform enemy = Instantiate(enemyPrefabs[prefabID], spawnPoints[spawnPointID].position, spawnPoints[spawnPointID].rotation);
        enemy.name = enemyPrefabs[prefabID].name;
    }

    private IEnumerator Wait(float seconds)
    {
        if (!waitingFinished)
        {
            yield return new WaitForSeconds(seconds);
            waitingFinished = true;
        }
    }
}
