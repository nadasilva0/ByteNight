using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BasicEnemyMovement : MonoBehaviour
{
    public float speed;
    private PathCreator pathCreator;
    private PlayerLivesController livesController;
    public float distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        pathCreator = FindFirstObjectByType<PathCreator>();
        livesController = FindFirstObjectByType<PlayerLivesController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
        if (distanceTraveled > 38)
        {
            livesController.health -= 1;
            livesController.hasLostLives = true;
            Destroy(gameObject);
        }
    }


}
