using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Patrol : MonoBehaviour {
    // Array de puntos de patrulla
    public Transform[] points; 
    // Índice del próximo punto de destino 
    private int destPoint = 0;  
    // Referencia al componente NavMeshAgent del objeto
    private NavMeshAgent agent;  

    void Start () {
        // Obtiene el componente NavMeshAgent adjunto al objeto
        agent = GetComponent<NavMeshAgent>();  
        // Desactiva el frenado automático del agente, permitiendo movimiento continuo entre los puntos de patrulla
        agent.autoBraking = false;  
        // Llama a la función para ir al siguiente punto de patrulla en total habra 3 
        GotoNextPoint();  
    }

    void GotoNextPoint() {
        // Retorna si no se han configurado puntos de patrulla
        if (points.Length == 0)  
            return;
        // Establece el punto de destino del agente patrullero como la posición del próximo punto de patrulla
        agent.destination = points[destPoint].position;  
        // Elige el siguiente punto en el arreglo como el destino, ciclando al inicio si es necesario
        destPoint = (destPoint + 1) % points.Length;  
    }

    void Update () {
        // Verifica si el agente ha llegado lo suficientemente cerca del punto de destino
        if (!agent.pathPending && agent.remainingDistance < 0.5f)  
        // Llama a la función para ir al siguiente punto de patrulla 3 en total
            GotoNextPoint();  
    }
}
