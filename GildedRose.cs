using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }
        public string ShowItems()
        {
            string output = "SellIn\tQuality\tName\n";

            foreach (Item item in Items)
            {
                output += item.SellIn + "\t" + item.Quality + "\t" + item.Name + "\n";
            }
            return output;
        }
        void UpdateItem(ref Item item,int sellInValueFactor,int qualityValueFactor)
        {
            item.SellIn     += sellInValueFactor;
            item.Quality    = Math.Max(0, Math.Min(50, item.Quality + qualityValueFactor));

        }
        bool IsExpired(Item item)
        {
            bool output = true;
            if(item.SellIn > 0) 
                output = false;
            return output;
        }
        Item GetNewAgedBrie(Item item)
        {
            if (IsExpired(item))
                UpdateItem(ref item, -1, 2);
            else
                UpdateItem(ref item, -1, 1);
            return item;
        }
        Item GetNewBackstagesPasses(Item item)
        {
            if (IsExpired(item))
                item.Quality = 0;
            else
            {
                if (item.SellIn <= 10 && item.SellIn > 5)
                {
                    UpdateItem(ref item, -1, 2);
                }
                else if (item.SellIn <= 5)
                {
                    UpdateItem(ref item, -1, 3);
                }
                else
                {
                    UpdateItem(ref item, -1, 1);
                }
            }
            return item;
        }
        Item GetNewConjured(Item item)
        {
            if (IsExpired(item))
            {
                UpdateItem(ref item, -1, -4);
            }
            else
            {
                UpdateItem(ref item, -1, -2);
            }
            return item;
        }
        Item GetNewDefault(Item item)
        {
            if (IsExpired(item))
            {
                UpdateItem(ref item, -1, -2);
            }
            else
            {
                UpdateItem(ref item, -1, -1);
            }
            return item;
        }
        public void UpdateQuality()
        {
            for(int itemIndex = 0; itemIndex < Items.Count; itemIndex++)
            {
                switch (Items[itemIndex].Name)
                {
                    case "Aged Brie":
                        Items[itemIndex]=GetNewAgedBrie(Items[itemIndex]);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        Items[itemIndex] = GetNewBackstagesPasses(Items[itemIndex]);
                        break;
                    case "Conjured Mana Cake":
                        Items[itemIndex] = GetNewConjured(Items[itemIndex]);
                        break;
                    default:
                        Items[itemIndex] = GetNewDefault(Items[itemIndex]);
                        break;
                }
            }
        }
        private void UpdateQualityOld()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
