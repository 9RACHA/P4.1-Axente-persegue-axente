
using UnityEngine;
using UnityEngine.AI; //Importa entre otras la clase NavMeshAgent

public class MoveTo : MonoBehaviour {

   public Transform goal; //Posicion del objetivo que perseguira el agente

    //Al incrementar la velocidad el comportamiento se vuelve mas erratico, solo mejora en las rectas
    // public float speed = 15f;

   void Start () {
      //Se crea una variable local llamada "agent" de tipo NavMeshAgent y se asigna el componente NavMeshAgent
      //GetComponent permite acceder a los componentes adjuntos a un objeto.
      NavMeshAgent agent = GetComponent<NavMeshAgent>();
      //establece la posición de destino del agente en la posición del objetivo especificado.
      // El agente utilizará la navegación basada en malla (NavMesh) de Unity para calcular la ruta más corta y moverse hacia el objetivo. 
      agent.destination = goal.position;

     // agent.speed = speed; // Incrementa la velocidad del agente
   }
}
