using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Editor : MonoBehaviour {

    // Objects list
    private ListaObjetos listaObjetos;

    public Transform canvas;

    public GameObject prefabObjeto;
    public GameObject bloqueSeleccionado;
    public GameObject lastObject;

    public Transform objetosMapa;

    void Start()
    {
        listaObjetos = GetComponent<ListaObjetos>();
        seleccionarObjeto(0);
        canvas.FindChild(menu).GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bloqueSeleccionado.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f);
        if(bloqueSeleccionado.transform.position.y > 2 + Camera.main.transform.position.y)
        {
            bloqueSeleccionado.transform.position = new Vector3(Mathf.Round(pos.x), 2f + + Camera.main.transform.position.y, 0f);
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                bloqueSeleccionado.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                bloqueSeleccionado.SetActive(true);
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.CircleCast(new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f), 0.01f, Vector2.up, 0.1f);
                if (hit.collider != null) 
                {
                    if(hit.transform.tag == "Objeto")
                    {
                        return;
                    }
                }
                else
                {
                    GameObject bloque = Instantiate(prefabObjeto, new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f), Quaternion.identity) as GameObject;
                    bloque.GetComponent<SpriteRenderer>().sprite = objeto.sprite;
                    bloque.GetComponent<ID>().id = objeto.id;
                    bloque.transform.SetParent(objetosMapa);
                    lastObject = bloque;
                }
            }
            else if (Input.GetMouseButton(1))
            {
                RaycastHit2D hit = Physics2D.CircleCast(new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f), 0.01f, Vector2.up, 0.1f);
                if (hit.collider != null)
                {
                    if (hit.transform.tag == "Objeto")
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    private string menu;

    public void openMenu(string menu)
    {
        canvas.FindChild(menu).GetChild(0).gameObject.SetActive(true);
        this.menu = menu;
    }
    public void closeMenu(string menu)
    {
        canvas.FindChild(menu).GetChild(0).gameObject.SetActive(false);
        this.menu = "";
    }
    public void openSubMenu(string submenu)
    {
        canvas.FindChild(menu).GetChild(0).FindChild(submenu).GetChild(0).gameObject.SetActive(true);
    }
    public void closeSubMenu(string submenu)
    {
        canvas.FindChild(menu).GetChild(0).FindChild(submenu).GetChild(0).gameObject.SetActive(false);
    }

    Objeto objeto;

    public void seleccionarObjeto(int id)
    {
        if(lastObject != null)
        {
            Destroy(lastObject);
        }

        objeto = listaObjetos.objetos[id];
        objeto.id = id;

        switch (objeto.type)
        {
            case "Suelo":
                bloqueSeleccionado.GetComponent<SpriteRenderer>().sprite = objeto.sprite;
                break;
        }

        canvas.FindChild(menu).GetChild(0).gameObject.SetActive(false);
        this.menu = "";
    }

    public void GuardarMapa(string nombre)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream mapa = File.Open(Application.persistentDataPath + "/" + nombre + ".map", FileMode.OpenOrCreate);

        MapData mapData = new MapData();

        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Objeto");

        for (int i = 0; i < objetos.Length; i++)
        {
            mapData.xPos.Insert(i, (int)objetos[i].transform.position.x);
            mapData.yPos.Insert(i, (int)objetos[i].transform.position.y);

            mapData.id.Insert(i, objetos[i].GetComponent<ID>().id);
        }

        bf.Serialize(mapa, mapData);

        mapa.Close();
    }
    public void CargarMapa(string nombre)
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/" + nombre + ".map"))
        {
            FileStream mapa = File.Open(Application.persistentDataPath + "/" + nombre + ".map", FileMode.Open);

            MapData mapData = bf.Deserialize(mapa) as MapData;

            Debug.Log(mapData.id.Count);

            GameObject[] objetos = GameObject.FindGameObjectsWithTag("Objeto");

            foreach(GameObject ano in objetos)
            {
                Destroy(ano);
            }

            for (int i = 0; i < mapData.id.Count; i++)
            {
                GameObject bloque = Instantiate(prefabObjeto, new Vector3(mapData.xPos[i], mapData.yPos[i], 0f), Quaternion.identity) as GameObject;
                bloque.GetComponent<SpriteRenderer>().sprite = listaObjetos.objetos[mapData.id[i]].sprite;
                bloque.GetComponent<ID>().id = mapData.id[i];
                bloque.transform.SetParent(objetosMapa);
            }

            mapa.Close();
        }
        else
        {
            Debug.LogError("'" + nombre + ".dat' no encontrado");
        }
    }

    [Serializable]
    class MapData
    {
        public List<int> xPos = new List<int>();
        public List<int> yPos = new List<int>();

        public List<int> id = new List<int>();
    }
}
