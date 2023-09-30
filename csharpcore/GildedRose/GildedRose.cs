using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    const string AgedBrie = "Aged Brie";
    const string BackStagePasses = "Backstage passes to a TAFKAL80ETC concert";
    const string Sulfuras = "Sulfuras, Hand of Ragnaros";

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        // loop through the items
        foreach(Item item in _items)
        //for (var i = 0; i < _items.Count; i++)
        {
            // make sure the item isn't aged brie or backstage passes
            if (item.Name != AgedBrie && item.Name != BackStagePasses)
            {
                // make sure the quality > 0
                if (item.Quality > 0)
                {
                    // make sure the item isn't Sulfuras
                    if (item.Name != Sulfuras)
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
                    if (item.Name == BackStagePasses)
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
            if (item.Name != Sulfuras)
            {
                item.SellIn -= 1;
            }
            // if sell in is less than 0
            if (item.SellIn < 0)
            {
                if (item.Name != AgedBrie)
                {
                    if (item.Name != BackStagePasses)
                    {
                        if (item.Quality > 0)
                        {
                            if (item.Name != Sulfuras)
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
        }
    }
}