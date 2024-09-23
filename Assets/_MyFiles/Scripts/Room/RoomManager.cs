using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public class Cell
    {
        public bool bIsVisited = false;
        public bool[] bKeepDoor = new bool[4];
        
    }

    [SerializeField] private string currrentWorldName = "The Basement";


    [SerializeField] private Vector2 size;
    [SerializeField] private int startPos;
    [SerializeField] private GameObject room;
    [SerializeField] private Vector2 offset;

    private List<Cell> dungeon;
    private void Start()
    {
        Algorithim();
    }
    

    private void GenterateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<Room>();
                newRoom.UpdateRoom(dungeon[Mathf.FloorToInt(i+j*size.x)].bKeepDoor);
                
                newRoom.name += $" {i}-{j}";
            }
        }
    }

    private void Algorithim()
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
                        dungeon[currentCell].bKeepDoor[2] = true;
                        currentCell = newCell;
                        dungeon[currentCell].bKeepDoor[3] = true;
                    }
                    else
                    {
                        dungeon[currentCell].bKeepDoor[1] = true;
                        currentCell = newCell;
                        dungeon[currentCell].bKeepDoor[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        dungeon[currentCell].bKeepDoor[3] = true;
                        currentCell = newCell;
                        dungeon[currentCell].bKeepDoor[2] = true;
                    }
                    else
                    {
                        dungeon[currentCell].bKeepDoor[0] = true;
                        currentCell = newCell;
                        dungeon[currentCell].bKeepDoor[1] = true;
                    }
                }
            }
        }
        GenterateDungeon();
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
        if ((cell+1) % size.x != 0 && !dungeon[Mathf.FloorToInt(cell+1)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell+1));
        }
        
        //Check West neighbour
        if (cell % size.x != 0 && !dungeon[Mathf.FloorToInt(cell - 1)].bIsVisited)
        {
            neighbors.Add(Mathf.FloorToInt(cell-1));
        }
        
        
        return neighbors;
    }
}

