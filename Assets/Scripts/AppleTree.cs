using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    public GameObject appleMediumPrefab;
    public GameObject appleHardPrefab;
    public GameObject appleImpossiblePrefab;

    public float baseSpeed = 10f;
    public float curSpeed;

    public float leftAndRightEdge = 20f;

    public float chanceToChangeDirections = 0.02f;

    public float secondsBetweenAppleDrops = 1f;

    private Dictionary<GameObject, int> initialForce = new Dictionary<GameObject, int>();

	// Use this for initialization
	void Start () {
        initialForce.Add(applePrefab, 0);
        initialForce.Add(appleMediumPrefab, 8);
        initialForce.Add(appleHardPrefab, 16);
        initialForce.Add(appleImpossiblePrefab, 32);
		Invoke("DropApple", 2f);
	}

    void DropApple()
    {
        // Based on score, randomly decide to choose apple that drops.
        Dictionary<GameObject, int> weight = new Dictionary<GameObject, int>();
        int scoreWeight = Basket.score / 100;
        Console.WriteLine(scoreWeight.ToString());
        weight.Add(applePrefab, 1);
        weight.Add(appleMediumPrefab, scoreWeight - 2 >= 0 ? scoreWeight - 2 : 0); // 10
        weight.Add(appleHardPrefab, scoreWeight - 4 >= 0 ? scoreWeight - 4 : 0); // 20
        weight.Add(appleImpossiblePrefab, scoreWeight - 8 >= 0 ? scoreWeight - 8 : 0); // 30
        GameObject appleRef = GetRandomApple(weight);
        GameObject apple = Instantiate<GameObject>(appleRef);
        var rb = apple.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * initialForce[appleRef], ForceMode.VelocityChange);
        //GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    GameObject GetRandomApple(Dictionary<GameObject, int> weight)
    {
        int totalWeight = weight.Sum(c => c.Value);
        System.Random r = new System.Random(); // Unity has it's own random, which forces the System.Random;
        int choice = r.Next(totalWeight);
        GameObject rv = applePrefab; // Default value, should be overwritten in the foreach.
        foreach (var x in weight)
        {
            if (choice - x.Value > 0)
            {
                choice = choice - x.Value;
                continue;
            }

            rv = x.Key; 
            break;
        }

        return rv;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    curSpeed = ((curSpeed + 1)/Mathf.Abs(curSpeed + 1)) * (baseSpeed + (Basket.score / 100));
	    Vector3 pos = transform.position;
	    pos.x += curSpeed * Time.deltaTime;
	    transform.position = pos;

	    if (pos.x < -leftAndRightEdge)
	    {
	        curSpeed = Mathf.Abs(curSpeed);
	    }
	    else if (pos.x > leftAndRightEdge)
	    {
	        curSpeed = -Mathf.Abs(curSpeed);
	    } 
	}

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {
            curSpeed *= -1;
        }
    }
}
