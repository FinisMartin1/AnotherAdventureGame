using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop 
{
    public int DropRate = 0;
    public int NumberOfDrop = 0;
    public int ItemId = 1;

    public Drop(int dropRate, int numberOfDrop, int itemId)
    {
        DropRate = dropRate;
        NumberOfDrop = numberOfDrop;
        ItemId = itemId;
    }
}
