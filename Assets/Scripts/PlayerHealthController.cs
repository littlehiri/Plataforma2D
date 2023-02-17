using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //Variables para controlar la vida actual del jugador y el maximo de vida que puede tener
    public int currentHealth, maxHealth;
    //Variables para el contador de tiempo e invencibilidad
    public float invincibleLength; //Valor que tendrá el contador de tiempo
    private float invincibleCounter; //Contador de tiempo

    private SpriteRenderer theSR;
    // Start is called before the first frame update
    public static PlayerHealthController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
        //Obtenemos el SpriteRenderer del jugador
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si el contador de invencibilidad esta lleno
        if (invincibleCounter > 0)
        {
            //Le restamos 1 cada segundo a ese contador independientemente del dispositivo
            
        }
    }

    //Metodo para manejar el daño
    public void DealWithDamage()
    {
        //Restamos 1 de la vida que tengamos
        currentHealth--; //Currenthealth -=1, currenthealth = currenthealth - 1;

        //Si la vida está en 0 o por debajo (para asegurarnos de tener en cuenta solo valores
        if (currentHealth <=0)
        {
            //Hacemos cero la vida si fuera negativa
            currentHealth = 0;

            //Hacemos desaparecer de momento al jugador
            gameObject.SetActive(false);
        }
        //Si el jugador recibe daño pero no ha muerto
        else
        {
            //inicializamos el contador de invencibilidad
            invincibleCounter = invincibleLength;
            //Cambiamos el color del sprite, mantenemos el RGB y ponemos la opacidad a la
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 5f);
        }

        //Actualizamos la UI
        UICotroller.sharedInstance.UpdateHealthDisplay();
    }
}
