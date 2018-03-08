using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour {

    private List<GameObject> sceneObjects = new List<GameObject>();
    GameObject[] tempBlocks;

    // Update is called once per frame
    public void CastBlocks (string color) {
        sceneObjects.Clear();
        tempBlocks = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < tempBlocks.Length; i++)
        {
            sceneObjects.Add(tempBlocks[i]);
        }

        sceneObjects.Sort((x,y) => x.transform.position.y.CompareTo(y.transform.position.y));

        for(int i = 0; i < sceneObjects.Count; i++)
        {
            if(sceneObjects[i].GetComponent<Block>().blockType == Block.BlockType.CrushBottom || sceneObjects[i].GetComponent<Block>().blockType == Block.BlockType.CrushTop)
            {
                sceneObjects[i].GetComponent<Block>().CrushBlock();
            }
        }

        if(sceneObjects[0] && Time.timeScale > 0f) sceneObjects[0].SendMessage("DestroyBlock", color);
    }
}
