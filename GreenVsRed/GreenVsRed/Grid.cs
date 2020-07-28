using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


public class Grid
{
    private int x;
    private int y;

    private int[,] array;
    
    public int Count { get; private set; }

    private int row;
    private int col;


    /// <summary>
    /// constructor if you want the array to be filled with random numbers
    /// </summary>
    public Grid(int x,int y)
    {
        this.x = x;
        this.y = y;

        this.FillArray();
        this.PrintGrid();
        this.Count = 0;
    }

    /// <summary>
    /// constructor if you want the array to be filled with user input
    /// </summary>
    public Grid(int x, int y,int[,] array)
    {
        this.x = x;
        this.y = y;
        this.array = array;

        this.PrintGrid();
        this.Count = 0;
    }

    /// <summary>
    /// assigns cordinates
    /// </summary>
    public void AssignCordinates(int row,int col)
    {
        this.row = row;
        this.col = col;
    }

    /// <summary>
    /// Simulates the amout of generations the user inputs and counts the times the chosen element is green(1)
    /// </summary>
    public void Simulate(int N)
    {
        Count += array[row, col];
        for (int i = 1; i <= N; i++)
        {
            SumOfNeibours(array, x, y);
            Count += array[row, col];
        }
    }

    /// <summary>
    /// formating and pring a 2d array
    /// </summary>
    public void PrintGrid() 
    {
        int rowLength = this.array.GetLength(0);
        int colLength = this.array.GetLength(1);
        Console.Write(Environment.NewLine);
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", this.array[i, j]));
            }
            Console.Write(Environment.NewLine);
        }
    }

    /// <summary>
    /// fills the array with random numbers between 0 and 1
    /// </summary>
    public void FillArray()
    {
        this.array = new int[this.x, this.y];
        Random rnd = new Random();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = rnd.Next(0, 2);
            }
        }
    }

    /// <summary>
    /// does all the looping around the array and executes the changes for each element in the array
    /// </summary>
    public static void SumOfNeibours(int[,] array, int x, int y)
    {
        int[,] arrayPlaceholder = new int[x, y];

        //used to track the 8 neighbours of each element
        int[] rowDirection = { -1, -1, 0, 1, 1, 1, 0, -1 };
        int[] colDirection = { 0, 1, 1, 1, 0, -1, -1, -1 };

        int rowLength = array.GetUpperBound(0);
        int colLength = array.GetUpperBound(1);
        for (int i = 0; i <= rowLength; i += 1)
        {
            for (int j = 0; j <= colLength; j += 1)
            {
                int sum = 0;
                for (int k = 0; k < rowDirection.Length; k += 1)
                {
                    int row = i + rowDirection[k];
                    int col = j + colDirection[k];
                    //checks if the element is in the bounds of the array
                    if (RowInBounds(row, array) && ColumnInBounds(col, array))
                    {
                        sum += array[row, col];
                    }
                }

                //applying the rules for each element and storing them in a placeholder array
                if (array[i, j] == 0)
                {
                    if (sum == 3 || sum == 6)
                    {
                        arrayPlaceholder[i, j] = 1;
                    }
                    else
                    {
                        arrayPlaceholder[i, j] = 0;
                    }
                }
                else if (array[i, j] == 1)
                {
                    if (sum == 2 || sum == 3 || sum == 6)
                    {
                        arrayPlaceholder[i, j] = 1;
                    }
                    else
                    {
                        arrayPlaceholder[i, j] = 0;
                    }
                }
            }
        }

        //assigning the converted array to the main array
        for (int i = 0; i <= rowLength; i += 1)
        {
            for (int j = 0; j <= colLength; j += 1)
            {
                array[i, j] = arrayPlaceholder[i, j];
            }
        }

    }

    /// <summary>
    ///  checks if the element is in the bounds of the row in the array
    /// </summary>
    private static bool RowInBounds(int index, int[,] array)
    {
        return (index >= 0) && (index <= array.GetUpperBound(0));
    }

    /// <summary>
    ///  checks if the element is in the bounds of the column in the array
    /// </summary>
    private static bool ColumnInBounds(int index, int[,] array)
    {
        return (index >= 0) && (index <= array.GetUpperBound(1));
    }
}

