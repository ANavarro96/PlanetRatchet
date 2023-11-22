using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Adjust the speed as needed
    public float veloicidadRotacion = 5f; // Adjust the rotation speed as needed

    public Camera camara; // Reference to the player camera
    public Transform planeta;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.mass = 0.5f;
        rb.freezeRotation = true; // Prevent physics engine from affecting rotation
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // Saco el vector que apunte del cubo al centro del planta.
        // Al normalizar estoy obtieniendo únicamente la dirección del mismo, con valores de -1, 0 o 1.
        Vector3 distanciaCentro = (transform.position - planeta.transform.position).normalized;

        // Aplico la gravedad
        Vector3 gravedad = -distanciaCentro * 9.8f;

        // Uso aceleración en vez de impulse, ya que esta no tiene en cuenta la masa
        rb.AddForce(gravedad, ForceMode.Force);

        // Con esta operación estoy alineando el eje y del cubo (transform.up) con el centro del planeta.
        // Multiplico este valor por la rotación para pasar desde donde estoy, hacia el eje alineado.
        transform.rotation = Quaternion.FromToRotation(transform.up, distanciaCentro) * transform.rotation;

        // Sacamos el input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Lo muevo
        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
        // Lo muevo
        rb.velocity = moveDirection * velocidadMovimiento;

        
    }

    private void OnDrawGizmos()
    {
        Vector3 origen = camara.transform.position;
        Vector3 direccion = camara.transform.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(origen, direccion * 10);

    }
}
