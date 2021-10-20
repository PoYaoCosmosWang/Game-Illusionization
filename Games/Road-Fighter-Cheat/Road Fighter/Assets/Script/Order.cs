using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Material _material;
    public string _pattern;
    public int _number;
    public Order(Material material, string pattern, int number)
    {
        _material = material;
        _pattern = pattern;
        _number = number;
    }

}
