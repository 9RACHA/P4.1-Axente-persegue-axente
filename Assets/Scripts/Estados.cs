using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Estados : MonoBehaviour
{   
    //[tag P.4.1.4]
    // El agente persigue al objetivo si está dentro de una distancia determinada y dentro de un ángulo de visión determinado utilizando estados

    public Transform objetivo; // Jugador 1
    public Transform casa; // Punto de retorno inicial

    public float proximidadRadio; // Distancia del rayo
    public float anguloVision; // Ángulo de visión que tendrá el agente

    NavMeshAgent agente; // Jugador 2
    AgentState estado; // Estado para los 3 tipos 
    RaycastHit hit; // Referencia al raycast
    
    void Start(){
        agente = GetComponent<NavMeshAgent>(); // El agente accede al componente NavMeshAgent
        casa = GameObject.FindGameObjectWithTag("Casa").transform; // Acceder al objeto con el tag "Casa" y asignarlo a la variable 'casa'
        objetivo = GameObject.FindGameObjectWithTag("Jugador").transform; // Acceder al objeto con el tag "Jugador" y asignarlo a la variable 'objetivo'
        estado = AgentState.Idle; // El estado del agente se establece en "Idle" (parado)
    }

    void FixedUpdate(){
        Vector3 RayI = transform.forward * -30; // Vector del rayo izquierdo igual a la posición hacia adelante por -30
        Vector3 RayD = transform.forward * 30; // Vector del rayo derecho igual a la posición hacia adelante por 30
    
        Debug.DrawLine(transform.position, objetivo.transform.position, Color.black, 1f); // Dibuja una línea negra desde la posición del agente hacia la posición del objetivo
        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow, 1f); // Dibuja el rayo hacia adelante
        Debug.DrawRay(transform.position, RayI * 10, Color.blue, 1f); // Dibuja el rayo izquierdo
        Debug.DrawRay(transform.position, RayD * 10, Color.white, 1f); // Dibuja el rayo derecho
        
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position); // Calcula la distancia al objetivo
        // Si se produce una colisión con el rayo dentro de la proximidadRadio
        if (Physics.Raycast(transform.position, objetivo.transform.position - transform.position, out hit, proximidadRadio)){
            Sigo();
        } switch (estado) { // Elige el estado del agente en función del estado actual
            case AgentState.Idle:// Caso 1: El agente está parado, esperando
                Debug.Log(estado); // Imprime el estado actual en la consola
                if (Sigo()) // Si 'Sigo' devuelve verdadero
                    SetState(AgentState.Chasing); // Cambia el estado a "Chasing" (persiguiendo)
                break;
            case AgentState.Chasing:// Caso 2: El agente está persiguiendo al jugador
                Debug.Log(estado); // Imprime el estado actual en la consola
                if (Sigo()) // Si 'Sigo' devuelve verdadero
                    agente.destination = objetivo.position; // Establece el destino del agente en la posición del objetivo
                else
                    SetState(AgentState.Returning); // Cambia el estado a "Returning" (volviendo)
                break;    
            case AgentState.Returning:// Caso 3: El agente está volviendo a casa
                Debug.Log(estado); // Imprime el estado actual en la consola
                if (Sigo()) // Si 'Sigo' devuelve verdadero
                    SetState(AgentState.Chasing); // Cambia el estado a "Chasing" (persiguiendo)
                else if (agente.destination == casa.position) // Si el destino del agente es igual a la posición de la casa
                    SetState(AgentState.Idle); // Cambia el estado a "Idle" (parado)
                break;
        }
    }
    
    public void SetState(AgentState newState){ // Establece el nuevo estado del agente
        if (newState != estado){
            estado = newState; // Asigna el nuevo estado
            switch (newState){
                case AgentState.Idle:// Caso 1: El agente está parado
                    break;
                case AgentState.Chasing:// Caso 2: El agente está persiguiendo
                    break; 
                case AgentState.Returning:// Caso 3: El agente está volviendo
                    break;
            }
        }
    }
    
    public bool Sigo(){
        // Determina si el agente debe seguir persiguiendo al objetivo o volver a casa
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position);
        if (distanciaAlObjetivo < proximidadRadio && Veo()){// Si la distancia al objetivo es menor a proximidadRadio y 'Veo' devuelve verdadero
            agente.destination = objetivo.position; // Establece el destino del agente en la posición del objetivo
            return true; 
        } else {
            agente.destination = casa.position; // Establece el destino del agente en la posición de la casa (punto de inicio)
            return false;
        }
    }
    
    public bool Veo(){
        // Determina si el agente tiene al objetivo dentro de su ángulo de visión
        Vector3 direccionObjetivo = objetivo.transform.position - transform.position; // Calcula la dirección hacia el objetivo
        anguloVision = Vector3.Angle(transform.forward, direccionObjetivo); // Calcula el ángulo de visión
        if (anguloVision > -40 && anguloVision < 40){// Si el ángulo de visión está entre -40 y 40 grados
            return true; 
        } else if (hit.transform.tag == "Obstaculo"){ // Si el raycast colisiona con un objeto que tiene el tag "Obstaculo"
            Debug.Log("No veo"); // Imprime un mensaje en la consola
            agente.destination = casa.position; // Establece el destino del agente en la posición de la casa (punto de inicio)
        } 
        return false; 
    }
}

public enum AgentState{ // Enumeración que define los estados posibles del agente
    Idle, // Parado
    Chasing, // Persiguiendo
    Returning // Volviendo
}

