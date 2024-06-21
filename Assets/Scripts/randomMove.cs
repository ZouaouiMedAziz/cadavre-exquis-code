using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class randomMove : MonoBehaviour
{
    public float speed = 5.0f;              // Vitesse de déplacement
    public Vector2 minBounds;               // Limites minimales de la zone
    public Vector2 maxBounds;               // Limites maximales de la zone

    private Vector2 direction;              // Direction actuelle
    private Vector2 startPosition;          // Point de départ
    private bool isReturning;               // Indique si l'objet est en train de revenir

   
    public int ScoreUnity = 0, ScoreVs = 0;


    public TextMeshProUGUI UnityText, VsText, finUnityScore, finVsScore;

    
    public float changeInterval = 1.0f;  // Intervalle de changement de direction

    private Vector2 movementDirection;

    public static randomMove RandomInstance;
    void Start()
    {
        if (RandomInstance == null)
        {
            RandomInstance = this;
        }
        Respawn();
    }

    void Update()
    {

        Move();
        CheckBounds();
    }

    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void CheckBounds()
    {
        if (transform.position.x <= minBounds.x || transform.position.x >= maxBounds.x ||
            transform.position.y <= minBounds.y || transform.position.y >= maxBounds.y)
        {
            if (!isReturning)
            {
                // Changer de direction pour retourner
                direction = -direction;
                isReturning = true;
            }
            else
            {
                // Si en train de revenir et atteint les limites, respawn
                Respawn();
            }
        }

        // Détruire l'objet si il revient au point de départ
        if (isReturning && Vector2.Distance(transform.position, startPosition) < 0.1f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Définir une position aléatoire sur les bords
        Vector2 spawnPosition = GetRandomEdgePosition();

        // Définir une direction aléatoire en ligne droite
        direction = GetRandomDirection();

        // Placer l'objet à la nouvelle position
        transform.position = spawnPosition;
        startPosition = spawnPosition;
        isReturning = false; 
    }

    Vector2 GetRandomEdgePosition()
    {
        // Choisir un côté au hasard
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: 
                return new Vector2(Random.Range(minBounds.x, maxBounds.x), minBounds.y);
            case 1: 
                return new Vector2(Random.Range(minBounds.x, maxBounds.x), maxBounds.y);
            case 2: 
                return new Vector2(minBounds.x, Random.Range(minBounds.y, maxBounds.y));
            case 3: 
                return new Vector2(maxBounds.x, Random.Range(minBounds.y, maxBounds.y));
            default:
                return new Vector2(minBounds.x, minBounds.y); 
        }
    }

    Vector2 GetRandomDirection()
    {
        // Générer une direction en ligne droite
        switch (Random.Range(0, 4))
        {
            case 0: return Vector2.up;       
            case 1: return Vector2.down;     
            case 2: return Vector2.left;     
            case 3: return Vector2.right;    
            default: return Vector2.up;      
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Vs"))
        {
            ScoreVs++;
            print(ScoreVs);
            VsText.text = ScoreVs.ToString();
            finVsScore.text = ScoreVs.ToString();

        }
        if (other.gameObject.CompareTag("Unity"))
        {
            ScoreUnity++;
            print(ScoreUnity);
            UnityText.text = ScoreUnity.ToString();
            finUnityScore.text = ScoreUnity.ToString();
        }

    }
}