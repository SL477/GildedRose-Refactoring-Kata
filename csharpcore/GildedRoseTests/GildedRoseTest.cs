using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using System.IO;
using System.Text;
using System;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void Foo()
    {
        IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(items);
        app.UpdateQuality();
        Assert.Equal("foo", items[0].Name);
    }
    [Fact]
    public void ThirtyDayOutput()
    {
        // run the test
        StringBuilder fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        TextTestFixture.Main(new string[] { });
        string output = fakeOutput.ToString();

        // get the approved test
        StreamReader sr = new StreamReader("../../../ApprovalTest.ThirtyDays.approved");
        string i = sr.ReadToEnd();

        Assert.Equal(i, output);
    }
    [Fact]
    public void IsAgedBrie()
    {
        IList<Item> items = new List< Item> { new Item { Name = "Aged Brie", SellIn=0, Quality = 0 }, new Item { Name = "hi", SellIn = 0, Quality = 0 } };
        Assert.True(GildedRose.IsAgedBrie(items[0]));
        Assert.False(GildedRose.IsAgedBrie(items[1]));
    }
    [Fact]
    public void IsBackstagePass()
    {
        Assert.True(GildedRose.IsBackstagePass(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn=0, Quality=0 }));
        Assert.False(GildedRose.IsBackstagePass(new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 }));
    }
    [Fact]
    public void IsSulfuras()
    {
        Assert.True(GildedRose.IsSulfuras(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 0 }));
        Assert.False(GildedRose.IsSulfuras(new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 }));
    }
    [Fact]
    public void IsConjured()
    {
        Assert.True(GildedRose.IsConjured(new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 0 }));
        Assert.False(GildedRose.IsConjured(new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 }));
    }
}