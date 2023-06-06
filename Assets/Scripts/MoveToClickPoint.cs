using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour {
    // Referencia al componente NavMeshAgent del objeto
    NavMeshAgent agent;  
    
    void Start() {
        // Obtiene el componente NavMeshAgent adjunto al objeto
        agent = GetComponent<NavMeshAgent>();  
    }
    
    void Update() {
        // Verifica si se ha presionado el botón izquierdo del raton
        if (Input.GetMouseButtonDown(0)) {  
            // Almacena información sobre el punto de impacto del rayo
            RaycastHit hit;  
            // Lanza un rayo desde la cámara hacia la posición del mouse en pantalla y verifica si colisiona con algún objeto en un rango de 100 unidades
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                // Establece el punto de destino del agente como el punto de impacto del rayo
                agent.destination = hit.point;  
            }
        }
    }
}

