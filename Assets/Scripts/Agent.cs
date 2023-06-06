using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour{

    //El Script del Agente que sera perseguidor del agente jugador

    public Transform target;
    NavMeshAgent agent;
    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update(){
        agent.destination = target.position;
    }
}
