using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public class Cell
    {
        public bool bIsVisited = false;
        public bool[] status = new bool[4];
        
    }

    [SerializeField] private string currrentWorldName = "The Basement";
    

    [SerializeField] private Vector2 size;
    [SerializeField] private int startPos;

    private List<Cell> dungeon;


    void MazeGenerator()
    {
        dungeon = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                dungeon.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;
            dungeon[currentCell].bIsVisited = true;
            
            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //Down or Right
                    if (newCell - 1 == currentCell)
                    {
                        dungeon[currentCell].status[2] = true;
                        currentCell = newCell;
                        dungeon[currentCell].status[3] = true;
                    }
                    else
                    {
                        dungeon[currentCell].status[1] = true;
                        currentCell = newCell;
                        dungeon[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //Down or Right
                    if (newCell - 1 == currentCell)
                    {
                        dungeon[currentCell].status[2] = true;
                        currentCell = newCell;
                        dungeon[currentCell].status[3] = true;
                    }
                    else
                    {
                        dungeon[currentCell].status[1] = true;
                        currentCell = newCell;
                        dungeon[currentCell].status[0] = true;
                    }
                }
            }
        }
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        
        //Check North neighbour
        if (cell - size.x >= 0 && !dungeon[Mathf.FloorToInt(cell-size.x)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell-size.x));
        }
        
        //Check South neighbour
        if (cell + size.x < dungeon.Count && !dungeon[Mathf.FloorToInt(cell+size.x)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell+size.x));
        }
        
        //Check East neighbour
        if ((cell+1) % size.x < 0 && !dungeon[Mathf.FloorToInt(cell+size.x)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell+size.x));
        }
        
        //Check West neighbour
        if ((cell+1) % size.x != 0 && !dungeon[Mathf.FloorToInt(cell + 1)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell-size.x));
        }
        
        
        return neighbors;
    }
}

