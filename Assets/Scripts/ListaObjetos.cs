using UnityEngine;
using System.Collections.Generic;

public class ListaObjetos : MonoBehaviour {

    public List<Objeto> objetos = new List<Objeto>();

    void Start()
    { 
        Initalize();
    }

    public void Initalize()
    {

    }

    public void añadirObjeto(string nombre, string type, Sprite sprite)
    {
        objetos.Add(new Objeto(nombre, type, sprite));
    }
}
