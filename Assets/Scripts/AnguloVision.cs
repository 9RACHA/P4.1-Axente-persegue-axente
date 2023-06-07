using UnityEngine;
using UnityEngine.AI;

public class AnguloVision : MonoBehaviour
{
    //3.O axente persegue se o target está dentro dunha distancia determinada e nun ángulo de visión determinado
    //tag P.4.1.3

    // Jugador Objetivo a perseguir
    public Transform jugador; 
    // Punto de retorno inicial
    public Transform casa; 

    // Distancia del raycast
    public float distanciaRay; 
    // Ángulo de visión que tendrá el agente
    public float anguloVision; 

    // Posición de la dirección al objetivo
    Vector3 direccionToObjetivo; 
    // Agente Jugador 
    NavMeshAgent agentePerseguidor; 
    // Referencia al raycast
    RaycastHit hit; 

    void Start(){
        // El agentePerseguidor accede al componente NavMeshAgent
        agentePerseguidor = GetComponent<NavMeshAgent>(); 
        // Inicializar la variable direccionToObjetivo
        direccionToObjetivo = jugador.transform.position - transform.position; 
        // El ángulo de visión será igual a un ángulo de mirar hacia delante y la dirección al objetivo
        anguloVision = Vector3.Angle(transform.forward, direccionToObjetivo); 
    }

    void FixedUpdate(){
        // Normalizar el vector para mantener la misma dirección pero con longitud 1.0
        Vector3 direccionToJugador = (jugador.position - transform.position).normalized; 
        //se utiliza para lanzar un rayo desde un punto en una dirección específica y determinar si ese rayo colisiona con algún objeto en la escena
        if (Physics.Raycast(transform.position, direccionToJugador, out hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore)){ //Infinity, el rayo puede alcanzar cualquier objeto sin importar la distancia.
            // Si hay una colisión con el raycast mirando hacia delante //especifica cómo se deben manejar los objetos marcados como "trigger" al lanzar el rayo.Ignore,indica que los objetos trigger no deben generar colisiones
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue); // Dibujar un rayo azul hacia delante
        } else {
            // Si no hay colisión
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.yellow); // Dibujar un rayo amarillo
        }

        EnRango(); // Método para verificar si el objetivo está en rango de visión
    }


    private bool Sigo(){
        float distanciaToJugador = Vector3.Distance(jugador.position, transform.position);
        // Si la distancia al jugador es menor que distanciaRay y el raycast golpea un objeto con etiqueta "Jugador"
        if (distanciaToJugador < distanciaRay && hit.collider.tag == "Jugador"){
            // El agentePerseguidor se dirige a la posición del jugador
            agentePerseguidor.destination = jugador.position; 
            return true;
        } else {
            // El agentePerseguidor se dirige a la posición de la casa o punto inicial
            agentePerseguidor.destination = casa.position; 
            return false;
        }
    }

    private bool EnRango(){
        // Si el ángulo de visión es mayor que -30 y menor que 30
        if (anguloVision > -30 && anguloVision < 30){
            // Si el raycast golpea un objeto con etiqueta "Jugador"
            if (hit.collider.tag == "Jugador"){
                Sigo(); 
                return true;
            } else {
                // El agentePerseguidor se dirige a la posición de la casa o punto inicial
                agentePerseguidor.destination = casa.position; 
                return false;
            }
        }
        return true; 
    }
}
