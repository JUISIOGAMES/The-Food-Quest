using UnityEngine;
using System.Collections;

[System.Serializable]
public class Objeto{
    public string nombre;
    public string type;
    public Sprite sprite;
    public int id;

    public Objeto(string nombre, string type, Sprite sprite)
    {
        this.nombre = nombre;
        this.type = type;
        this.sprite = sprite;
    }
}
