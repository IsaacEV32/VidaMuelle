using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class CambiarescenaMenu : MonoBehaviour
{
    public void Cambiarescena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
