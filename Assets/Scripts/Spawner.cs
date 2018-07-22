using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] groups;

    GameObject group;
    GameObject nextGroup;
    public void SpawnNext()
    {
        if (!DataBase.isPause && !DataBase.gameOver)
        {
            int i = Random.Range(0, groups.Length);
            // new Vector3(4, 17)
            nextGroup = Instantiate(groups[i], transform.position, Quaternion.identity) as GameObject;
            nextGroup.GetComponent<Group>().enabled = false;
            Menu.nextGroup = nextGroup;
            ChangeColor.AddSpriteToColor();
        }
    }

    public void MoveToGame()
    {
        group = nextGroup;
        group.transform.position = new Vector2(4, 17);
        group.GetComponent<Group>().enabled = true;
        SpawnNext();
    }
}
