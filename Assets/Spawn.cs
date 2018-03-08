using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public MainController mainController;
    public int startSpawn = 10;
    

    private float timer;

    private void Awake()
    {
        if (mainController == null) mainController = GameObject.FindObjectOfType<MainController>();

        for(int i = 0; i < startSpawn; i++)
        {
            SpawnBlock(1f);
        }
    }

    private void Update()
    {

    }

    public void SpawnBlock(float incrementHeight)
    {
        if(Random.Range(0f, 100f) >= mainController.rateSpecial)
        {
            int rand = Random.Range(0, mainController.maxBlock);
            GameObject ins = Instantiate(mainController.blocks[rand], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            ins.name = ins.GetComponent<Block>().color + " Block";
            
        }
        else
        {
            int rand = Random.Range(0, mainController.specialBlocks.Count);
            GameObject ins = Instantiate(mainController.specialBlocks[rand], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            ins.name = ins.GetComponent<Block>().color + " Block";
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + incrementHeight);
    }
}
