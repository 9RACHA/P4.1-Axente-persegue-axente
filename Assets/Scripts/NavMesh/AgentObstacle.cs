using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentObstacle : MonoBehaviour {
    // Factor de amortiguación para controlar la suavidad del movimiento
    public float damp = 2;
    // Magnitud de desplazamiento a lo largo del eje z (forward)           
    public float displacement = 40f; 
    // Valor interpolado suavemente entre -1 y 1
    float pingpong;  
    // Posición inicial en el eje y (altura) del agente                
    float height;                    

    void Start() {
        // Almacena la altura inicial del agente
        height = transform.position.y;  
    }

    void Update() {
        // Calcula un valor interpolado suavemente
        pingpong = Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / damp, 1));  
        // Actualiza la posición del agente
        transform.localPosition = Vector3.up * height + transform.forward * pingpong * displacement; 
    }
}
