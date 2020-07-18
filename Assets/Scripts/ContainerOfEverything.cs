using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ContainerOfEverything : MonoBehaviour
{
    public static ContainerOfEverything instance;
    public GameObject player;
    public GameObject gameCam;
    public Text scoreText;

    [Header("Monster Stuff")]
    public GameObject[] monsters;
    public List<GameObject> mon;
    public float minTime;
    public float maxTime;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(GameObject m in monsters)
        {
            m.SetActive(false);
        }

        StartCoroutine(GameTimer());

    }

    private int RandomMonster()
    {
        int i = Random.Range(0, monsters.Length - 1);
        if(monsters[i].activeSelf == true)
        {
            RandomMonster();
        }
        return i;
    }

    IEnumerator GameTimer()
    {
        for(int i = 0; i < monsters.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            monsters[RandomMonster()].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    


    }


}