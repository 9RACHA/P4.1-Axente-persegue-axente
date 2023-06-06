using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [Range(0,1)]
    // Índice del botón del mouse a utilizar (0 para el botón izquierdo, 1 para el derecho)
    public int mouseButtonIndex = 0;  

    void Update() {
        // Verifica si se ha presionado el botón del mouse especificado
        if (Input.GetMouseButtonDown(mouseButtonIndex)) { 
            // Almacena información sobre el punto de impacto del rayo lanzado 
            RaycastHit hitData;  
            // Crea un rayo desde la cámara hacia la posición del mouse en pantalla, invisible
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            // Lanza el rayo y comprueba si colisiona con algún objeto
            if (Physics.Raycast(ray, out hitData, 1000)) {  
                // Establece la posición de este objeto en el punto de impacto del rayo
            }   transform.position = hitData.point;  
        }   
    }
}
