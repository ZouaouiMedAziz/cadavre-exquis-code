using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float rotationspeed;
    public Transform pos1, pos2;
    public float S;
    public Transform startpos;
    private int ScoreUnity =0, ScoreVs = 0;

    Vector3 nextpos;

    public TextMeshProUGUI UnityText, VsText;

    public float speed = 2.0f;       // Vitesse de déplacement
    public float changeInterval = 1.0f;  // Intervalle de changement de direction

    private Vector2 movementDirection;

    void Start()
    {
        //  nextpos = startpos.position;
        InvokeRepeating("ChangeDirection", 0, changeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        /*  transform.Rotate(0, 0, rotationspeed);
          if (transform.position == pos1.position)
          {
              nextpos = pos2.position;
              rotationspeed *= -1;

          }
          if (transform.position == pos2.position)
          {
              nextpos = pos1.position;
              rotationspeed *= -1;
          }
          transform.position = Vector3.MoveTowards(transform.position, nextpos, S * Time.deltaTime);*/
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized; // Direction aléatoire
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Vs"))
        {
            ScoreVs++;
            print(ScoreVs);
            VsText.text = ScoreVs.ToString();

        }
        if (other.gameObject.CompareTag("Unity"))
        {
            ScoreUnity++;
            print(ScoreUnity);
            UnityText.text = ScoreUnity.ToString();
        }

    }
        }
