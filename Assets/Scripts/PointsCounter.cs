using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private CubeThrower[] dices;

    private Rigidbody[] dicesRigidbody;
    private int diceAmount;
    private int landedCounter;
    private bool shouldChange;
    private int score;

    public int Score
    {
        get { return score; }
        set { }
    }

    private void Start()
    {
        if(dices.Length == 0)
        {
            Debug.LogError("Dices are not included in counter");
        }

        diceAmount = dices.Length;
        landedCounter = 0;
        shouldChange = false;
        dicesRigidbody = new Rigidbody[diceAmount];

        for(int i = 0; i < diceAmount; i++)
        {
            dices[i].InProcess = false;
            dicesRigidbody[i] = dices[i].transform.GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        if (shouldChange && IsMoving())
        {
            return;
        }

        if (shouldChange && landedCounter == diceAmount)
        {
            shouldChange = false;
            SetInProcess(false);
            var sum = 0;

            foreach (var dice in dices)
            {
                var maxY = 0f;
                var topSide = "";
                for (int i = 0; i < 6; i++)
                {
                    var currSide = dice.transform.GetChild(i);
                    if (currSide.position.y > maxY)
                    {
                        topSide = currSide.name;
                        maxY = currSide.position.y;
                    }
                    
                }
                switch (topSide)
                {
                    case "1": sum += 1; break;
                    case "2": sum += 2; break;
                    case "3": sum += 3; break;
                    case "4": sum += 4; break;
                    case "5": sum += 5; break;
                    case "6": sum += 6; break;
                }
            }

            score = sum;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        landedCounter++;
        shouldChange = true;
        SetInProcess(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        landedCounter--;
        shouldChange = true;
        SetInProcess(true);
    }

    private void SetInProcess(bool state)
    {
        foreach (var dice in dices)
            dice.InProcess = state;
    }

    private bool IsMoving()
    {
        foreach (var dice in dicesRigidbody)
        {
            var velocity = dice.linearVelocity;
            if (velocity.x > 0.01f || velocity.y > 0.01f || velocity.z > 0.01f)
                return true;
        }
        return false;
    }
}
