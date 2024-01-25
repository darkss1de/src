using System;
using System.Collections.Generic;
using System.Xml;

public class Item
{
    public string Title { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Order
{
    public string Address { get; set; }
    public List<Item> Items { get; set; }

    public Order()
    {
        Items = new List<Item>();
    }
}

public static class XmlConverter
{
    public static Order ConvertXmlToOrder(string xml)
    {
        Order order = new Order();

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);

        XmlNodeList shipToNodes = doc.GetElementsByTagName("ship To");
        if (shipToNodes.Count > 0)
        {
            XmlNode shipToNode = shipToNodes[0];

            XmlNodeList addressNodes = shipToNode.ChildNodes;
            foreach (XmlNode addressNode in addressNodes)
            {
                if (addressNode.Name == "address")
                {
                    order.Address = addressNode.InnerText;
                    break;
                }
            }
        }

        XmlNodeList itemNodes = doc.GetElementsByTagName("item");
        foreach (XmlNode itemNode in itemNodes)
        {
            Item item = new Item();

            XmlNodeList itemChildNodes = itemNode.ChildNodes;
            foreach (XmlNode itemChildNode in itemChildNodes)
            {
                if (itemChildNode.Name == "title")
                {
                    item.Title = itemChildNode.InnerText;
                }
                else if (itemChildNode.Name == "quantity")
                {
                    int quantity;
                    if (int.TryParse(itemChildNode.InnerText, out quantity))
                    {
                        item.Quantity = quantity;
                    }
                }
                else if (itemChildNode.Name == "price")
                {
                    decimal price;
                    if (decimal.TryParse(itemChildNode.InnerText, out price))
                    {
                        item.Price = price;
                    }
                }
            }

            order.Items.Add(item);
        }

        return order;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string xml = "<?xml version=\"1.0\" ?><shipOrder><ship To><name>Tove Svendson</name><street>Ragnhildvei 2</street><address>4000 Stavanger</address><country>Norway</country></ship To><items><item><title>Empire Burlesque</title><quantity>1</quantity><price>10.90</price></item><item><title>Hide your heart</title><quantity>1</quantity><price>9.90</price></item></items></shipOrder>";

        Order order = XmlConverter.ConvertXmlToOrder(xml);

        Console.WriteLine($"Address: {order.Address}");

        foreach (Item item in order.Items)
        {
            Console.WriteLine($"Title: {item.Title}, Quantity: {item.Quantity}, Price: {item.Price}");
        }
    }
} World!");