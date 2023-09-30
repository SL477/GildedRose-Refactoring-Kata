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
        /*//for (var i = 0; i < _items.Count; i++)
        {
            // make sure the item isn't aged brie or backstage passes
            if (!IsAgedBrie(item) && !IsBackstagePass(item))
            {
                // make sure the quality > 0
                if (item.Quality > 0)
                {
                    // make sure the item isn't Sulfuras
                    if (!IsSulfuras(item))
                    {
                        item.Quality -= 1;
                    }
                }
            }
            else
            {
                // quality can't exceed 50
                if (item.Quality < 50)
                {
                    // increase the quality
                    item.Quality++;
                    // back stage passes Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
                    // Quality drops to 0 after the concert
                    if (IsBackstagePass(item))
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }
                    }
                }
            }
            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            if (!IsSulfuras(item))
            {
                item.SellIn -= 1;
            }
            // if sell in is less than 0
            if (item.SellIn < 0)
            {
                if (!IsAgedBrie(item))
                {
                    if (!IsBackstagePass(item))
                    {
                        if (item.Quality > 0)
                        {
                            if (!IsSulfuras(item))
                            {
                                // Once the sell by date has passed, Quality degrades twice as fast
                                item.Quality -= 1;
                            }
                        }
                    }
                    else
                    {
                        // after the concert backstage passes quality is 0
                        item.Quality = 0;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        // bries quality increaces twice as fast when its sellin is negative
                        item.Quality++;
                    }
                }
            }
        }*/
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