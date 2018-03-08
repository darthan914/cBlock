using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public enum BlockType
    {
        Single, Triple, Clear, Anti, CrushTop, CrushBottom
    }

    public BlockType blockType;
    public string color;
    public Spawn spawn;
    public MainController mainController;

    public float bonusTime = 2f;

    private GameObject blockObjectTemp;
    private bool isDestroy;

    private void Awake()
    {
        if (spawn == null) spawn = GameObject.FindObjectOfType<Spawn>();
        if (mainController == null) mainController = GameObject.FindObjectOfType<MainController>();

        if(blockType == BlockType.Anti || blockType == BlockType.CrushTop || blockType == BlockType.CrushBottom)
        {
            int rand = Random.Range(0, mainController.maxBlock);
            gameObject.GetComponent<SpriteRenderer>().color = mainController.blocks[rand].GetComponent<SpriteRenderer>().color;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = mainController.blocks[rand].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
            color = mainController.blocks[rand].GetComponent<Block>().color;
        }

        if (blockType == BlockType.Triple)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int rand = Random.Range(0, mainController.maxBlock);
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = mainController.blocks[rand].GetComponent<SpriteRenderer>().color;
                transform.GetChild(i).GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = mainController.blocks[rand].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                transform.GetChild(i).GetComponent<Block>().color = mainController.blocks[rand].GetComponent<Block>().color;
            }
        }
    }

    private void Update()
    {
        if (isDestroy)
        {
            spawn.SpawnBlock(0);
            Destroy(this.gameObject);
        }

        if(mainController.timerSet <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public bool DestroyBlock(string col)
    {
        switch (blockType)
        {
            case BlockType.Single:
                if (color == col)
                {
                    isDestroy = true;
                    mainController.AddBlocks(1);
                    mainController.TimeBonus(.5f);
                    mainController.GenerateMessages(true);
                    return true;
                }
                mainController.BreakCombo();
                mainController.TimeBonus(-1f);
                mainController.GenerateMessages(false);
                return false;
            case BlockType.Triple:

                int currentSub = transform.childCount;
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<Block>().color == col)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                        currentSub--;
                        break;
                    }
                    else
                    {
                        mainController.TimeBonus(-1f);
                        mainController.BreakCombo();
                        mainController.GenerateMessages(false);
                    }
                }

                if(currentSub == 0)
                {
                    isDestroy = true;
                    mainController.AddBlocks(3);
                    mainController.TimeBonus(1f);
                    mainController.GenerateMessages(true);
                    return true;
                }
                return false;
            case BlockType.Clear:
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up, 1f);
                float lowestBlock = Mathf.Infinity;

                for (int i = 0; i < hit.Length; i++)
                {
                    if(hit[i].transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                    {
                        if(hit[i].transform.position.y < lowestBlock)
                        {
                            blockObjectTemp = hit[i].transform.gameObject;
                            lowestBlock = hit[i].transform.position.y;
                        }
                    }
                }

                if(hit.Length > 0 && blockObjectTemp.GetComponent<Block>().DestroyBlock(col))
                {
                    mainController.AddBlocks(1);
                    mainController.TimeBonus(.2f);
                    isDestroy = true;
                    return true;
                }
                return false;
            case BlockType.Anti:
                if (color != col)
                {
                    isDestroy = true;
                    mainController.AddBlocks(1);
                    mainController.TimeBonus(.5f);
                    mainController.GenerateMessages(true);
                    return true;
                }
                mainController.BreakCombo();
                mainController.TimeBonus(-1f);
                mainController.GenerateMessages(false);
                return false;
            case BlockType.CrushTop:
                if (color == col)
                {
                    isDestroy = true;
                    mainController.AddBlocks(1);
                    mainController.TimeBonus(.5f);
                    mainController.GenerateMessages(true);
                    return true;
                }
                mainController.BreakCombo();
                mainController.TimeBonus(-1f);
                mainController.GenerateMessages(false);
                return false;
            case BlockType.CrushBottom:
                if (color == col)
                {
                    isDestroy = true;
                    mainController.AddBlocks(1);
                    mainController.TimeBonus(.5f);
                    mainController.GenerateMessages(true);
                    return true;
                }
                mainController.BreakCombo();
                mainController.TimeBonus(-1f);
                mainController.GenerateMessages(false);
                return false;
            default:
                return false;
        }
    }

    public void CrushBlock()
    {
        if(blockType == BlockType.CrushTop)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up, 1f);
            float lowestBlock = Mathf.Infinity;

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject.GetInstanceID() != gameObject.GetInstanceID() && hit[i].transform.gameObject.tag == "Block")
                {
                    if (hit[i].transform.position.y < lowestBlock)
                    {
                        blockObjectTemp = hit[i].transform.gameObject;
                        lowestBlock = hit[i].transform.position.y;
                    }
                }
            }

            spawn.SpawnBlock(0);
            if(blockObjectTemp) Destroy(blockObjectTemp.gameObject);
        }
        else if (blockType == BlockType.CrushBottom)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);
            float highestBlock = -Mathf.Infinity;

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    if (hit[i].transform.position.y > highestBlock && hit[i].transform.gameObject.tag == "Block")
                    {
                        blockObjectTemp = hit[i].transform.gameObject;
                        highestBlock = hit[i].transform.position.y;
                    }
                }
            }

            spawn.SpawnBlock(0);
            if (blockObjectTemp) Destroy(blockObjectTemp.gameObject);
        }
        else
        {
            return;
        }
    }
}
