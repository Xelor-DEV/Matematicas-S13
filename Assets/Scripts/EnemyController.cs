using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float translateTime;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private int damage;
    private int currentPoint;
    private float journeyLength;
    private float startTime;
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    void Start()
    {
        startTime = Time.time;
        currentPoint = 0;
        journeyLength = Vector3.Distance(patrolPoints[currentPoint].position, patrolPoints[GetNextPatrolIndex()].position);
    }

    void Update()
    {
        float distanceTraveled = (Time.time - startTime) / translateTime;
        float percentageTraveled = Mathf.PingPong(distanceTraveled, journeyLength) / journeyLength;
        transform.position = Vector3.Lerp(patrolPoints[currentPoint].position, patrolPoints[GetNextPatrolIndex()].position, animationCurve.Evaluate(percentageTraveled));

        if (Vector3.Distance(transform.position, patrolPoints[GetNextPatrolIndex()].position) < 0.1f)
        {
            currentPoint = GetNextPatrolIndex();
            startTime = Time.time;
            journeyLength = Vector3.Distance(patrolPoints[currentPoint].position, patrolPoints[GetNextPatrolIndex()].position);
        }
    }
    private int GetNextPatrolIndex()
    {
        if (currentPoint + 1 < patrolPoints.Length)
        {
            return currentPoint + 1;
        }
        else
        {
            return 0;
        }
    }
}
