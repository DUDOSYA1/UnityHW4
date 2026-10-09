using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public event Action<int> ScoreChanged;
    public event Action ThrowStart;

    private HashSet<GameObject> dices;
    private HashSet<CubeThrower> dicesCT;
    private HashSet<Rigidbody> dicesRigidbody;

    private int landedCounter;
    private bool shouldChange;
    private int score;

    public int DiceAmount
    {
        get { return dices.Count; }
        set { }
    }

    public int Score
    {
        get { return score; }
        set { }
    }

    private void Start()
    {
        landedCounter = 0;
        shouldChange = false;
        dices = new HashSet<GameObject>();
        dicesRigidbody = new HashSet<Rigidbody>();
        dicesCT = new HashSet<CubeThrower>();
    }

    private void FixedUpdate()
    {
        if (shouldChange && IsMoving())
        {
            return;
        }

        if (shouldChange && landedCounter == DiceAmount)
        {
            shouldChange = false;
            SetInProcess(false);
            var sum = 0;

            foreach (var dice in dicesCT)
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
            ScoreChanged.Invoke(score);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!dices.Contains(collision.gameObject))
        {
            dices.Add(collision.gameObject);
            dicesCT.Add(collision.gameObject.GetComponent<CubeThrower>());
            dicesRigidbody.Add(collision.gameObject.GetComponent<Rigidbody>());
        }
        landedCounter++;
        shouldChange = true;
        SetInProcess(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        landedCounter--;
        shouldChange = true;
        SetInProcess(true);
        ThrowStart.Invoke();
    }

    private void SetInProcess(bool state)
    {
        if(dicesCT.Count == 0)
            return;
        foreach (var dice in dicesCT)
            dice.InProcess = state;
    }

    private bool IsMoving()
    {
        if(dicesRigidbody.Count == 0) 
            return false;
        foreach (var dice in dicesRigidbody)
        {
            var velocity = dice.linearVelocity;
            if (velocity.x > 0.01f || velocity.y > 0.01f || velocity.z > 0.01f)
                return true;
        }
        return false;
    }

    public void ClearDicesInfo()
    {
        if(dices!=null)
            dices.Clear();
        if(dicesCT!=null) 
            dicesCT.Clear();
        if(dicesRigidbody!=null) 
            dicesRigidbody.Clear();
        landedCounter = 0;
        shouldChange = false;
    }
}
