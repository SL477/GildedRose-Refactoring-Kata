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

        Assert.Equal(output, i);
    }
}