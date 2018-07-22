using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{

    public static GameObject[] goForColor;
    public GameObject[] textsForColor;
    public GameObject[] btnsForColor;

    public void Start()
    {
        AddSpriteToColor();
        Colorize();
    }
    public static void AddSpriteToColor()
    {
        goForColor = GameObject.FindGameObjectsWithTag("Sprite");

        for (int i = 0; i < goForColor.Length; i++)
        {
            goForColor[i].GetComponent<SpriteRenderer>().color = DataBase.gameColor;
        }
    }

    public void Colorize()
    {
        textsForColor = GameObject.FindGameObjectsWithTag("Text");
        btnsForColor = GameObject.FindGameObjectsWithTag("Image");

        for (int i = 0; i < textsForColor.Length; i++)
        {
            textsForColor[i].GetComponent<Text>().color = DataBase.gameColor;
        }

        for (int i = 0; i < btnsForColor.Length; i++)
        {
            btnsForColor[i].GetComponent<Image>().color = DataBase.gameColor;
        }

        for (int i = 0; i < goForColor.Length; i++)
        {
            if(goForColor[i])
                goForColor[i].GetComponent<SpriteRenderer>().color = DataBase.gameColor;
        }
    }
}
