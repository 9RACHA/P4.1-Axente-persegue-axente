using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Circulo : MonoBehaviour{

    //tag P.4.1.1
    //1. O axente persegue se o target está dentro dunha distancia determinada en 360º (non importa que haxa obxectos entres eles, continúa a persecución)

    public Transform AgenteObjetivo;
    public Transform PuntoInicial;
    NavMeshAgent agente;

    void Start()
    {
        // Se obtiene el componente NavMeshAgent del objeto al que se adjunta este script y se asigna a la variable "agent"
        agente = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate() {
        //Se calcula la dirección desde la posición del agente hacia la posición del objetivo normalizando el vector resultante
        Vector3 directionToTarget = (AgenteObjetivo.position - transform.position).normalized;
        int distanceToAgent = 15; // Distancia del rayo
        //Se calcula la distancia entre el agente y el objetivo
        float agenteDistance= Vector3.Distance(AgenteObjetivo.position, transform.position); // Distancia entre los dos agentes
        //Se dibuja un rayo desde la posición del agente en la dirección del objetivo multiplicado por la distancia máxima del rayo
        Debug.DrawRay(transform.position, directionToTarget * distanceToAgent, Color.yellow);
        // Si la distancia entre el agente y el objetivo es menor que la distancia máxima del rayo
        if(agenteDistance < distanceToAgent){
            // El agente se movera a la posicion del objetivo
            agente.destination = AgenteObjetivo.position;
            Debug.Log("Va al Agente");
        } else {
            Debug.Log("Va al punto inicial");
            agente.destination = PuntoInicial.position;
        }
    }
}
