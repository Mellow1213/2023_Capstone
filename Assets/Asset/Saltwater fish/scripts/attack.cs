using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    Animator animator;
    [SerializeField] Terrain terrain;
    [SerializeField] Transform waterPlane;
    [SerializeField] float attackDistance =10f;
    public static int movespeed = 2;
    public float speed = 2f;
    public int rotSpeed = 1;
    public Vector3 userDirection = Vector3.forward;
    private Vector3 target;
    private Vector3 direction;
    private Transform player;
    private Transform t;
    private Vector3 goal;
    private float timer = 10;
    private bool attacking = false;
    private bool outOfWater = false;
    public int damage = 10;
    public Vector3 swimLimits = new Vector3(10, 5, 10);
    [Tooltip("Increase this to increase the shark's attack distance")]
    [SerializeField]
    float distanceOffset = 2f;
    [Tooltip("Increase this to decrease the shark's height relative to the player")]
    [SerializeField]
    float heightOffset = 0.75f;
    [Tooltip("Increase this to keep the fish a certain distance below the water level")]
    [SerializeField] float waterHeightOffset = 1;
    private float waterLevel;
    [Tooltip("Increase this to increase the minimum height above the terrain")]
    [SerializeField] float terrainOffset = 1f;
    private float terrainHeight;
    private bool lookAtShark = false;
    
    // Start is called before the first frame update

   private void Awake() 
        {
            
            t = this.transform;
            player  = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    
    void Start()
    {
        
        animator = GetComponent<Animator>();
        waterLevel = waterPlane.transform.position.y - waterHeightOffset -5;
        goal = new Vector3(Random.Range(-swimLimits.x, swimLimits.x), Random.Range(terrainHeight, waterLevel), Random.Range(-swimLimits.z, swimLimits.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (goal.y < terrainHeight)
        {
            goal.y = terrainHeight + terrainOffset;
        }
        
        if (!lookAtShark)
        {
            player.LookAt(this.transform);
            lookAtShark = true;
        }
        terrainHeight = Terrain.activeTerrain.SampleHeight(goal) + terrainOffset;
        
        timer -= Time.deltaTime;
        if (timer <= 0 || GoalDistance() < 2)
        {
            NewGoal();
            timer = 10;
        }
        direction = goal - this.transform.position;
        
        if (transform.position.y > 48)
        {
            outOfWater = true;
        }
        if(transform.position.y < 48)
        {
            outOfWater = false;
        }
        target = new Vector3(player.position.x, player.position.y - heightOffset, player.position.z);
        if (!attacking)
        {
            if (Distance() > attackDistance)
            {
            Move();
            }
        }
        if (Distance() < attackDistance)
        {
            attacking = true;
        }
        if (attacking)
            {
                if (Distance() > 2 + distanceOffset)
                {
                MoveToPlayer();
                }
                if(Distance() <= 2+distanceOffset)
                {
                Attacking();
                }

            }
    }

    private void Move()
    {
        animator.SetBool("biting",false);
        transform.Translate(0, 0, Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        if (transform.position.y < Terrain.activeTerrain.SampleHeight(transform.position) + terrainOffset)
        {
            direction.y = 5;
        }
    }

    private void MoveToPlayer()
    {
        animator.SetBool("biting", false);
        // transform.LookAt(target);
        var rotationAngle = Quaternion.LookRotation(player.position - transform.position); // we get the angle has to be rotated
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 5); // we rotate the rotationAngle 
        transform.Translate(userDirection * movespeed * Time.deltaTime);
        if (outOfWater)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
        
    }

    private float Distance()
    {
        return Vector3.Distance(t.position, player.position);
    }

    private float GoalDistance()
    {
        return Vector3.Distance(goal, transform.position);
    }

    private void Attacking()
    {
        if (Distance() > 3)
        {
            animator.SetBool("biting", false);
        }
        if (Distance() < 3)
        {
            animator.SetBool("biting", true);
        }
         var rotationAngle = Quaternion.LookRotation(target - transform.position); // we get the angle has to be rotated
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 5); // we rotate the rotationAngle 
        //transform.LookAt(target);
        
        if(Distance() > 2 + distanceOffset)
        {
            transform.Translate(userDirection * movespeed * Time.deltaTime);
        }

        if (Distance() <= 2 + distanceOffset)
        {
            transform.Translate(-userDirection * movespeed * Time.deltaTime);
        }
    }

    private void NewGoal()
    {
        goal = new Vector3(Random.Range(-swimLimits.x, swimLimits.x), Random.Range(terrainHeight, waterLevel), Random.Range(-swimLimits.z, swimLimits.z));
    }

    public void DealDamage()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
