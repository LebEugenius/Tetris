using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour
{

    float fallSpeed = 1;
    float timer = 0;
    public string typeOfGroup;

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            if (!Grid.insideBorder(v))
                return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < Grid.h; ++y)
        {
            for (int x = 0; x < Grid.w; x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

                }
            }
        }
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void Update()
    {
        if (!DataBase.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    UpdateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                {
                    UpdateGrid();
                }
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
            else if (typeOfGroup != "O" && Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    UpdateGrid();
                else
                    // It's not valid. revert.
                    transform.Rotate(0, 0, 90);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                timer -= 0.2f;
            }

            if (Time.time > timer)
            {
                transform.position += new Vector3(0, -1, 0);
                if (isValidGridPos())
                {
                    UpdateGrid();

                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);

                    Grid.DeleteFullRows();

                    DataBase.score += 4 * DataBase.level;

                    FindObjectOfType<Spawner>().MoveToGame();

                    enabled = false;
                }
                timer = Time.time + fallSpeed;
            }
        }
    }

    void Start()
    {
        fallSpeed -= 0.1f * (DataBase.level - 1);
        if (DataBase.score > DataBase.scoreToNextLvl)
        {
            DataBase.level += 1;
            DataBase.scoreToNextLvl = DataBase.score + 25 * (DataBase.level * 5);
        }

        if (!isValidGridPos())
        {
            GameObject.Find("Helpers").GetComponent<Menu>().GameOver();
            transform.position += new Vector3(0, 1, 0);
            DataBase.gameOver = true;
        }
    }
}
