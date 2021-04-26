using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCave : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects = new GameObject[10];

    public int[,] Map1;
    public int[,] Map2;
    public int[,] Map3;
    public int[,] Map4;
    public int[,] Map5;

    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public int PercentAreWalls { get; set; }

    void Start()
    {
        MapWidth = 41;
        MapHeight = 241;
        PercentAreWalls = 40;

        //Map1 = BlankMap(Map1);
        Map1 = RandomFillMap(Map1);
        Map1 = MakeCaverns(Map1, 0);
        Map1 = MakeCaverns(Map1, 0);
        Map1 = MakeCaverns(Map1, 0);
        Map1 = MakeCaverns(Map1, 0);
        Map1 = MakeCaverns(Map1, 0);


        //MakeCaverns(1);
        //MakeCaverns(1);
        //MakeCaverns(1);
        //MakeCaverns(1);

        Map1 = MakeCaverns(Map1, 2);
        //Map1 = MakeCaverns(Map1, 2);
        //MakeCaverns(2);
        //MakeCaverns(2);


        InstantiateObjects(Map1, 2);

        Map2 = new int[MapWidth, MapHeight];
        Map2 = Map1;
        Map2 = MakeTop(Map2);
        InstantiateObjects(Map2, 6);

        Map3 = new int[MapWidth, MapHeight];
        Map3 = Map2;
        Map3 = MakeTop(Map3);
        InstantiateObjects(Map3, 10);

        //Map4 = new int[MapWidth, MapHeight];
        //Map4 = Map3;
        //Map4 = MakeTop(Map4);
        //InstantiateObjects(Map4, 14);

        //Map5 = new int[MapWidth, MapHeight];
        //Map5 = Map4;
        //Map5 = MakeTop(Map5);
        //InstantiateObjects(Map5, 18);
    }

    private int[,] MakeTop(int[,] Map)
    {
        int[,] tempMap = Map.Clone() as int[,];

        // By initilizing column in the outter loop, its only created ONCE
        for (int column = 0, row = 0; row <= MapHeight - 1; row++)
        {
            for (column = 0; column <= MapWidth - 1; column++)
            {


                tempMap[column, row] = PlaceWallLogicTop(column, row, Map);

            }
        }
        return tempMap;
    }

    private int PlaceWallLogicTop(int x, int y, int[,] Map)
    {
        int numWalls = GetAdjacentWalls(x, y, 1, 1, Map);

        if (Map[x, y] == 1)
        {
            if (numWalls >= 8)
            {
                
                return 1;
                
            }
        }
        return 0;
    }

    public void InstantiateObjects(int[,] Map, int level)
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int z = 0; z < MapHeight; z++)
            {
                if (Map[x, z] == 1)
                {
                   Map = CullLone(x, z, 1, 1, Map);

                }
                else
                {

                }
            }
        }
        for (int x = 0; x < MapWidth; x++)
        {
            for (int z = 0; z < MapHeight; z++)
            {
                if (Map[x, z] == 1)
                {
                    GetWhichWall(x, z, 1, 1, Map, level);

                } else
                {

                }
            }
        }
    }
    public int[,] CullLone(int x, int z, int scopeX, int scopeZ, int[,] Map)
    {
        float[,] walls = new float[3, 3];
        walls[1, 1] = 1;
        int startX = x - scopeX;
        int startZ = z - scopeZ;
        int endX = x + scopeX;
        int endZ = z + scopeZ;

        int iX = startX;
        int iZ = startZ;

        int wallcounter = 0;


        for (iZ = startZ; iZ <= endZ; iZ++)
        {
            for (iX = startX; iX <= endX; iX++)
            {
                if (!(iX == x && iZ == z))
                {
                    if (IsWall(iX, iZ, Map))
                    {
                        walls[x - iX + 1, z - iZ + 1] = 1;
                        wallcounter += 1;
                    }
                    else
                    {
                        walls[x - iX + 1, z - iZ + 1] = 0;
                    }
                }
            }
        }
        if (wallcounter == 3)
        {

            //cin

            if (walls[0, 0] == 1 && walls[1, 0] == 1 && walls[0, 1] == 1)
            {

            }
            else if (walls[0, 2] == 1 && walls[1, 2] == 1 && walls[0, 1] == 1)
            {

            }
            else if (walls[2, 0] == 1 && walls[1, 0] == 1 && walls[2, 1] == 1)
            {

            }
            else if (walls[2, 2] == 1 && walls[1, 2] == 1 && walls[2, 1] == 1)
            {

            }
            else
            {
                Map[x, z] = 0;
            }

        }
        if(wallcounter == 2)
        {
            Map[x, z] = 0;
        }
        if(wallcounter ==1 )
        {
            Map[x, z] = 0;
        }
        return Map;
    }
    
    public void GetWhichWall(int x, int z, int scopeX, int scopeZ, int[,] Map, int level)
    {
        float[,] walls = new float[3, 3];
        walls[1, 1] = 1;
        int startX = x - scopeX;
        int startZ = z - scopeZ;
        int endX = x + scopeX;
        int endZ = z + scopeZ;

        int iX = startX;
        int iZ = startZ;

        int wallcounter = 0;


        for (iZ = startZ; iZ <= endZ; iZ++)
        {
            for (iX = startX; iX <= endX; iX++)
            {
                if (!(iX == x && iZ == z))
                {
                    if (IsWall(iX, iZ, Map))
                    {
                        walls[x - iX +1  , z - iZ+1] = 1;
                        wallcounter += 1;
                    } else
                    {
                        walls[x - iX+1,z - iZ+1] = 0;
                    }
                }
            }
        }
        if (wallcounter == 0)
        {
            Instantiate(objects[4], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
        }
        else if (wallcounter == 7)
        {
            
            if (walls[0, 0] == 0)
            {
                GameObject gam = Instantiate(objects[2], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 270+180, 0);
            }
            else if (walls[0, 2] == 0)
            {
                GameObject gam = Instantiate(objects[2], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 0+180, 0);
            }
            else if (walls[2, 0] == 0)
            {
                GameObject gam = Instantiate(objects[2], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180+180, 0);
            }
            else if (walls[2,2] == 0)
            {
                GameObject gam = Instantiate(objects[2], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 90+180, 0);
            } else if (walls[0,1] == 0)
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0,180, 0);
            }
            else if (walls[1, 2] == 0)
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0,  180 - 90 + 180, 0);
            }
            else if (walls[2, 1] == 0)
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180 + 180, 0);
            }
            else if (walls[1, 0] == 0)
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 90, 0);
            }



        } else if (wallcounter == 3)
        {

            //cin
            
            if (walls[0, 0] == 1 && walls[1,0] == 1 && walls[0,1] == 1)
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180 + 180, 0);
            }
            else if (walls[0, 2] == 1 && walls[1,2] == 1 && walls[0, 1] == 1)
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 270 + 180, 0);
            }
            else if (walls[2, 0] == 1 && walls[1,0] == 1 && walls[2, 1] == 1)
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 90 + 180, 0);
            }
            else if (walls[2,2] == 1 && walls[1,2] == 1 && walls[2, 1] == 1)
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 0 + 180, 0);
            } else
            {
                Map[x, z] = 0;
            }

        }
        else if (wallcounter == 4)
        {

            //crout
            GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);

            if (walls[0, 2] == 1 && walls[2,2] == 1) //top 2 
            {
                if(walls[2,1] == 0) // right
                {
                    gam.transform.rotation = Quaternion.Euler(0,90, 0);
                } else
                {
                    gam.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                
            } else if (walls[0, 0] == 1 && walls[2, 0] == 1) // bottom 2
            {
                if (walls[2, 1] == 0) //right
                {
                    gam.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    gam.transform.rotation = Quaternion.Euler(0, 270, 0 );
                }

            }
            else if (walls[2, 0] == 1 && walls[2, 2] == 1) // right 2
            {
                if (walls[1, 2] == 0) // up
                {
                    gam.transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                else
                {
                    gam.transform.rotation = Quaternion.Euler(0, 180, 0);
                }

            } else // left 2
            {
                if (walls[1,2] == 0) //up
                {
                    gam.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    gam.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }


        } else if (wallcounter == 6)
        {
            //fl
            GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);

            if (walls[0, 0] == 0 && walls[0, 2] == 0) //fl vä
            {
                gam.transform.rotation = Quaternion.Euler(0,0, 0);
            }
            else if (walls[2, 0] == 0 && walls[2, 2] == 0) // fl hö
            {
                gam.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (walls[0, 2] == 0 && walls[2, 2] == 0) //fl upp
            {
                gam.transform.rotation = Quaternion.Euler(0,0, 0);
            }
            else if (walls[0, 0] == 0 && walls[2, 0] == 0) //fl ned
            {
                gam.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if (walls[0, 1] == 0)
            {
                gam.transform.rotation = Quaternion.Euler(0, 0 + 180, 0);
            }
            else if (walls[1, 2] == 0)
            {
                gam.transform.rotation = Quaternion.Euler(0, 90 + 180, 0);
            }
            else if (walls[2, 1] == 0)
            {
                gam.transform.rotation = Quaternion.Euler(0, 180 + 180, 0);
            }
            else
            {
                gam.transform.rotation = Quaternion.Euler(0, 270 + 180, 0);
            }
        }
        else if (wallcounter == 5)
        {
            
            if (walls[0, 0] == 0 && walls[1, 0] == 0 && walls[2, 0] == 0) // FL NER
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 270+180, 0);
            }
            else if (walls[0, 2] == 0 && walls[1, 2] == 0 && walls[2, 2] == 0) // FL UPP
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 90 + 180, 0);
            }
            else if (walls[0, 0] == 0 && walls[0, 1] == 0 && walls[0, 2] == 0) // FL VÄ
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (walls[2, 0] == 0 && walls[2, 1] == 0 && walls[2, 2] == 0) // FL HÖ
            {
                GameObject gam = Instantiate(objects[3], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180+180, 0);
            }
            else if (walls[0, 1] == 0 && walls[0, 2] == 0 && walls[1, 2] == 0) //crOut  NV
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 90 + 180, 0);
            }
            else if (walls[1, 2] == 0 && walls[2, 2] == 0 && walls[2, 1] == 0) //crOut  NÖ
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 180 + 180, 0);
            }
            else if (walls[2, 1] == 0 && walls[2, 0] == 0 && walls[1, 0] == 0) //crOut  SÖ
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 270 + 180, 0);
            }
            else // crout SV
            {
                GameObject gam = Instantiate(objects[1], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
                gam.transform.rotation = Quaternion.Euler(0, 0 + 180, 0);
            }
        }
        else
        { 
         //  Instantiate(objects[0], new Vector3(x * 2, level, z * 2), Quaternion.identity, this.transform);
        }
            


    }

    public int[,] MakeCaverns(int[,] Map, int iteration)
    {
        // By initilizing column in the outter loop, its only created ONCE
        for (int column = 0, row = 0; row <= MapHeight - 1; row++)
        {
            for (column = 0; column <= MapWidth - 1; column++)
            {
                Map[column, row] = PlaceWallLogic(column, row, iteration, Map);
            }
        }
        return Map;
    }

    public int PlaceWallLogic(int x, int y, int iteration, int[,] Map)
    {
        int numWalls = GetAdjacentWalls(x, y, 1, 1, Map);


        if (Map[x, y] == 1)
        {
            if (numWalls >= 4)
            {
                return 1;
            }
        }
        else
        {
            if ((y == 5 && x == 19) ||
                        (y == 5 && x == 20) ||
                        (y == 5 && x == 21) ||
                        (y == 6 && x == 19) ||
                        (y == 6 && x == 20) ||
                        (y == 6 && x == 21) ||
                        (y == 7 && x == 19) ||
                        (y == 7 && x == 20) ||
                        (y == 7 && x == 21) ||

                        (y == 237 && x == 19) ||
                        (y == 237 && x == 20) ||
                        (y == 237 && x == 21) ||
                        (y == 236 && x == 19) ||
                        (y == 236 && x == 20) ||
                        (y == 236 && x == 21) ||
                        (y == 235 && x == 19) ||
                        (y == 235 && x == 20) ||
                        (y == 235 && x == 21))
            {
                return 0;
            }
            

            if (iteration == 0)
            {
                if (numWalls >= 5 || numWalls <= 1)
                {
                    return 1;
                }
            }
            else if (iteration == 1)
            {
                if (numWalls >= 5 ||  numWalls <= 2)
                {
                    return 1;
                }
            }
            else
            {
                if (numWalls >= 5)
                {
                    return 1;
                }
            }

        }
        return 0;
    }

        public int GetAdjacentWalls(int x, int y, int scopeX, int scopeY, int[,] Map)
    {
        int startX = x - scopeX;
        int startY = y - scopeY;
        int endX = x + scopeX;
        int endY = y + scopeY;

        int iX = startX;
        int iY = startY;

        int wallCounter = 0;

        for (iY = startY; iY <= endY; iY++)
        {
            for (iX = startX; iX <= endX; iX++)
            {
                if (!(iX == x && iY == y))
                {
                    if (IsWall(iX, iY, Map))
                    {
                        wallCounter += 1;
                    }
                }
            }
        }
        return wallCounter;
    }

    bool IsWall(int x, int y, int[,] Map)
    {
        // Consider out-of-bound a wall
        if (IsOutOfBounds(x, y))
        {
            return true;
        }

        if (Map[x, y] == 1)
        {
            return true;
        }

        if (Map[x, y] == 0)
        {
            return false;
        }
        return false;
    }

    bool IsOutOfBounds(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return true;
        }
        else if (x > MapWidth - 1 || y > MapHeight - 1)
        {
            return true;
        }
        return false;
    }


    public int[,] BlankMap(int[,] Map)
    {
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                Map[column, row] = 0;
            }
        }
        return Map;
    }

    public int[,] RandomFillMap(int[,] Map)
    {
        // New, empty map
        Map = new int[MapWidth, MapHeight];

        int mapMiddle = 0; // Temp variable
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                // If coordinants lie on the the edge of the map (creates a border)
                if (column == 0)
                {
                    Map[column, row] = 1;
                }
                else if (row == 0)
                {
                    Map[column, row] = 1;
                }
                else if (column == MapWidth - 1)
                {
                    Map[column, row] = 1;
                }
                else if (row == MapHeight - 1)
                {
                    Map[column, row] = 1;
                }
                // Else, fill with a wall a random percent of the time
                else
                {
                    mapMiddle = (4);

                    if ( (row == 19 && column == 5 ) ||
                        (row == 20 && column == 5) ||
                        (row == 21 && column == 5) ||
                        (row == 19 && column == 6) ||
                        (row == 20 && column == 6) ||
                        (row == 21 && column == 6) ||
                        (row == 19 && column == 7) ||
                        (row == 20 && column == 7) ||
                        (row == 21 && column == 7) ||

                        (row == 19 && column == 238) ||
                        (row == 20 && column == 238) ||
                        (row == 21 && column == 238) ||
                        (row == 19 && column == 237) ||
                        (row == 20 && column == 237) ||
                        (row == 21 && column == 237) ||
                        (row == 19 && column == 236) ||
                        (row == 20 && column == 236) ||
                        (row == 21 && column == 236) )
                    {
                        Map[column, row] = 0;
                    }
                    else
                    {
                        Map[column, row] = RandomPercent(PercentAreWalls);
                    }
                }
            }
        }
        return Map;
    }

    int RandomPercent(int percent)
    {
        if (percent >= UnityEngine.Random.Range(1, 100))
        {
            return 1;
        }
        return 0;
    }

    //public MapHandler(int mapWidth, int mapHeight, int[,] map, int percentWalls = 40)
    //{
    //    this.MapWidth = mapWidth;
    //    this.MapHeight = mapHeight;
    //    this.PercentAreWalls = percentWalls;
    //    this.Map = new int[this.MapWidth, this.MapHeight];
    //    this.Map = map;
    //}
}
