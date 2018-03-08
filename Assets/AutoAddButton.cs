using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAddButton : MonoBehaviour {

    public Button button;
    public MainController mainController;
    public ColorButton colorButton;

    private int currentNumberButton;

    private void Awake()
    {
        if (mainController == null) mainController = GameObject.FindObjectOfType<MainController>();
    }

    private void Update()
    {
        if(currentNumberButton != mainController.maxBlock)
        {
            // Clear Button
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            for(int i = 0; i < mainController.maxBlock; i++)
            {
                Button temp = Instantiate(button, transform.position, Quaternion.identity);
                string color = mainController.blocks[i].GetComponent<Block>().color;
                temp.name = mainController.blocks[i].GetComponent<Block>().color + " Button";
                temp.transform.SetParent(transform);

                temp.GetComponent<Image>().color = mainController.blocks[i].GetComponent<SpriteRenderer>().color;
                temp.transform.GetChild(0).GetComponent<Image>().sprite = mainController.blocks[i].transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite;

                temp.onClick.AddListener(delegate { colorButton.CastBlocks(color); });
            }
            currentNumberButton = mainController.maxBlock;
        }
    }
}
