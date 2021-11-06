using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnInformation
{
    public Transform spawnTrans;
    public List<GameObject> spawnMonsters;
    public Vector2 defaultDir;
}


public class MonsterSpawner : MonoBehaviour
{
    private MonsterSpawner() { }

    private static MonsterSpawner instance;
    public static MonsterSpawner Instance => instance;

    [SerializeField] public List<SpawnInformation> spawnInformations;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        SpawningMonsters();
    }

    private void SpawningMonsters()
    {
        for (int i = 0; i < spawnInformations.Count; i++)
        {
            string monsterName = spawnInformations[i].spawnMonsters[Random.Range(0, spawnInformations[i].spawnMonsters.Count)].name;
            GameObject monster = ObjectPooler.Instance.GetPooledObject(monsterName);
            monster.transform.position = spawnInformations[i].spawnTrans.position;

            switch (monsterName)
            {
                case "Bird":
                    monster.GetComponent<ObstacleBird>().dir = spawnInformations[i].defaultDir;
                    break;
                case "Dron":
                    monster.GetComponent<ObstacleDron>().dir = spawnInformations[i].defaultDir;
                    break;
                case "Robot":
                    monster.GetComponent<ObstacleRobot>().moveDir = spawnInformations[i].defaultDir;
                    break;
                default:
                    break;
            }
            ObstacleObject oo = monster.GetComponent<ObstacleObject>();
            oo.parentIndex = i;
            oo.hp.curHP = oo.hp.maxHP;
            monster.gameObject.SetActive(true);
        }
    }

    public IEnumerator SpawningSpecificTrans(float coolTime,int index)
    {
        yield return new WaitForSeconds(coolTime);
        string monsterName = spawnInformations[index].spawnMonsters[Random.Range(0, spawnInformations[index].spawnMonsters.Count)].name;
        GameObject monster = ObjectPooler.Instance.GetPooledObject(monsterName);
        monster.transform.position = spawnInformations[index].spawnTrans.position;

        switch (monsterName)
        {
            case "Bird":
                monster.GetComponent<ObstacleBird>().dir = spawnInformations[index].defaultDir;
                break;
            case "Dron":
                monster.GetComponent<ObstacleDron>().dir = spawnInformations[index].defaultDir;
                break;
            case "Robot":
                monster.GetComponent<ObstacleRobot>().moveDir = spawnInformations[index].defaultDir;
                break;
        }

        ObstacleObject oo = monster.GetComponent<ObstacleObject>();
        oo.parentIndex = index;
        oo.hp.curHP = oo.hp.maxHP;

        monster.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
