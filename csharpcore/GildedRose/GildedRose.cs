using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        // loop through the items
        foreach(Item item in _items)
        {
            (int QualityLost, int SellinLost) = GetAmountsToMinus(item);
            item.Quality -= QualityLost;
            item.SellIn -= SellinLost;
        }
    }
    /// <summary>
    /// Check if the item is aged brie
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool IsAgedBrie(Item item)
    {
        return item.Name.ToLower().Contains("aged brie");
    }
    /// <summary>
    /// Check if the item is a backstage pass
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool IsBackstagePass(Item item)
    {
        return item.Name.ToLower().Contains("backstage pass");
    }
    /// <summary>
    /// Check if the item is sulfuras
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool IsSulfuras(Item item)
    {
        return item.Name.ToLower().Contains("sulfuras");
    }
    public static bool IsConjured(Item item)
    {
        return item.Name.ToLower().Contains("conjured");
    }
    /// <summary>
    /// Get the amount of quality and sellin to take off of the item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static (int QualityLost, int SellinLost) GetAmountsToMinus(Item item)
    {
        if (IsSulfuras(item))
        {
            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            return (0, 0);
        }
        else if(IsAgedBrie(item))
        {
            // "Aged Brie" actually increases in Quality the older it gets
            (int QualityLost, int SellinLost) ret;
            if (item.Quality == 50)
            {
                ret = (0, 1);
            }
            // Once the sell by date has passed, Quality degrades twice as fast
            else if (item.SellIn <= 0)
            {
                ret = (-2, 1);
            }
            else
            {
                ret = (-1, 1);
            }
            // The Quality of an item is never more than 50
            if (item.Quality - ret.QualityLost > 50)
            {
                ret = (item.Quality - 50, 1);
            }
            return ret;
        }
        else if (IsBackstagePass(item)) 
        {
            //  "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
            // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            // Quality drops to 0 after the concert
                    (int QualityLost, int SellinLost) ret;
            if (item.SellIn <= 0)
            {
                ret = (item.Quality, 1);
            }
            else if (item.SellIn <= 5)
            {
                ret = (-3, 1);
            }
            else if (item.SellIn <= 10)
            {
                ret = (-2, 1);
            }
            else
            { 
                ret = (-1, 1); 
            }

            if (item.Quality - ret.QualityLost > 50)
            {
                ret = (item.Quality - 50, 1);
            }
            return ret;
        }
        else if (IsConjured(item))
        {
            // Conjured items degrade at twice the rate of normal items
            (int QualityLost, int SellinLost) ret;
            if (item.SellIn <= 0)
            {
                // Once the sell by date has passed, Quality degrades twice as fast
                ret = (4, 1);
            }
            else
            {
                ret = (2, 1);
            }
            // The Quality of an item is never negative
            if (item.Quality - ret.QualityLost < 0)
            {
                ret = (item.Quality, 1);
            }
            return ret;
        }
        else
        {
            (int QualityLost, int SellinLost) ret;
            if (item.SellIn <= 0)
            {
                // Once the sell by date has passed, Quality degrades twice as fast
                ret = (2, 1);
            }
            else
            {
                ret = (1, 1);
            }
            // The Quality of an item is never negative
            if (item.Quality - ret.QualityLost < 0)
            {
                ret = (item.Quality, 1);
            }
            return ret;
        }
    }
}