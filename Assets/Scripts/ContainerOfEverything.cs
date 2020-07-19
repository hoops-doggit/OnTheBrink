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

    [Header("End game stuff")]
    public GameObject endGameScreen;
    public bool endGameStarted = true;
    public Text endGameTextBox;


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

    public void EndGameScene()
    {
        endGameTextBox.text = "You harvested " + ScoreManager.instance.score + " gems from your hamsters while they lived";
        endGameScreen.SetActive(true);
        endGameStarted = true;
    }

    IEnumerator AnnoyingMenuWorkAround()
    {
        yield return new WaitForSeconds(3);
        endGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.instance.hampsters == 0 && endGameStarted == false)
        {
            EndGameScene();
        }
    


    }


}