using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CirculoVision : MonoBehaviour
{
    //tag P.4.1.2
    //2.O axente persegue se o target está dentro dunha distancia determinada en 360º e ten liña de visión (se un obxecto se interpón entre eles deixa de perseguilo)

    public Transform jugador; //Jugador 1
    public Transform casa; //Casa o retorno, Objetivo 2.1
    public float distanciaRay; // Proyecta un rayo
    
    NavMeshAgent agentePerseguidor; //Jugador 2
    RaycastHit hit; //Referencia al Raycast

    void Start()
    {
        agentePerseguidor = GetComponent<NavMeshAgent>(); //Acceder al componente NavMeshAgent
    }
    
    void FixedUpdate()
    {
        //Se realiza un raycast desde la posición del agente hacia adelante utilizando
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,-1, QueryTriggerInteraction.Ignore) ){ 
            //Si el raycast golpea un objeto, se dibuja un rayo blanco desde la posición del agente hasta el punto de impacto
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);          
        } else {
            //Si el raycast no golpea ningún objeto, se dibuja un rayo negro
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.black); //Se dibuja uno negro 
        }
        //Determinar la distancia entre el agente y el jugador y realizar la persecución en consecuencia
        DistanciaJugadorRay();

    }

    private void DistanciaJugadorRay(){
        //Se calcula la distancia entre el agente y el jugador
        float distanciaToJugador = Vector3.Distance(jugador.position, transform.position);
        //Si la distancia al jugador es menor a la distancia del ray Y ademas contiene el tag Jugador
        if(distanciaToJugador < distanciaRay && hit.collider.tag == "Jugador"){ 
            agentePerseguidor.destination = jugador.position; //el agente perseguidor ira a la posicion del jugador
        } else { 
            agentePerseguidor.destination = casa.position; //el agente perseguidor ira al punto inicial casa
        } 
    }
}