using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Talk : MonoBehaviour
{
    GameObject speakingTo = null;
    Dictionary<int, int[]> speakingOutcomeChances = new Dictionary<int, int[]>();
    
    // Start is called before the first frame update
    void Start()
    {
        CreateConversationChancelist();
    }

    // Update is called once per frame
    void Update()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random <= 30 && speakingTo == null)
        {
            List<GameObject> nearbyPawns = Utils.GetAllGameObjects().FindAll(p => p != this && p.GetComponent<Talk>() != null && Utils.IsNearPosistion(this.gameObject.transform.position, p.transform.position, 3) && p != this.gameObject);
            if (nearbyPawns.Count > 0 && nearbyPawns != null && speakingTo == null)
            {
                this.speakingTo = nearbyPawns[UnityEngine.Random.Range(0, nearbyPawns.Count - 1)];
            }
        }

        if(speakingTo != null)
        {
            startSmallTalk();
            this.GetComponent<Needs>().social = this.GetComponent<Needs>().social + 10;
            speakingTo.GetComponent<Needs>().social = speakingTo.GetComponent<Needs>().social + 10;
            speakingTo = null;
        }
    }


    private void startSmallTalk()
    {
        Belief currentPawnBelief = this.GetComponent<Personality>().GetRandomBelief();
        Belief otherPawnBelief = this.GetComponent<Personality>().GetBelief(currentPawnBelief.beliefs);
        int currentCoversationOutcome = CalculateConversationChance(currentPawnBelief, otherPawnBelief);
        int otherConversationOutcome = CalculateConversationChance(currentPawnBelief, otherPawnBelief);
        if(this.gameObject.GetComponent<Relationship>().relationships.ContainsKey(speakingTo.GetComponent<Properties>().id))
        {
            this.gameObject.GetComponent<Relationship>().relationships[speakingTo.GetComponent<Properties>().id] = this.gameObject.GetComponent<Relationship>().relationships[speakingTo.GetComponent<Properties>().id] + currentCoversationOutcome;
        }
        else
        {
            this.gameObject.GetComponent<Relationship>().relationships.Add(speakingTo.GetComponent<Properties>().id, currentCoversationOutcome);
        }

        if (this.speakingTo.GetComponent<Relationship>().relationships.ContainsKey(this.gameObject.GetComponent<Properties>().id))
        {
            this.gameObject.GetComponent<Relationship>().relationships[speakingTo.GetComponent<Properties>().id] = this.gameObject.GetComponent<Relationship>().relationships[speakingTo.GetComponent<Properties>().id] + currentCoversationOutcome;
        }
        else
        {
            this.gameObject.GetComponent<Relationship>().relationships.Add(this.gameObject.GetComponent<Properties>().id, currentCoversationOutcome);

        }
    }

    private void CreateConversationChancelist()
    {
        for (int i = 0; i < 6; i++)
        {
            int[] chances = new int[6];
            switch (i)
            {
                case 0:
                    chances[0] = 1;
                    chances[1] = 3;
                    chances[2] = 6;
                    chances[3] = 50;
                    chances[4] = 25;
                    chances[5] = 15;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 1:
                    chances[0] = 3;
                    chances[1] = 6;
                    chances[2] = 11;
                    chances[3] = 40;
                    chances[4] = 15;
                    chances[5] = 10;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 2:
                    chances[0] = 5;
                    chances[1] = 10;
                    chances[2] = 15;
                    chances[3] = 30;
                    chances[4] = 10;
                    chances[5] = 5;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 3:
                    chances[0] = 10;
                    chances[1] = 20;
                    chances[2] = 20;
                    chances[3] = 20;
                    chances[4] = 20;
                    chances[5] = 10;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 4:
                    chances[0] = 5;
                    chances[1] = 10;
                    chances[2] = 30;
                    chances[3] = 15;
                    chances[4] = 10;
                    chances[5] = 5;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 5:
                    chances[0] = 10;
                    chances[1] = 15;
                    chances[2] = 40;
                    chances[3] = 11;
                    chances[4] = 6;
                    chances[5] = 3;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;
                case 6:
                    chances[0] = 15;
                    chances[1] = 25;
                    chances[2] = 50;
                    chances[3] = 6;
                    chances[4] = 3;
                    chances[5] = 1;
                    this.speakingOutcomeChances.Add(i, chances);
                    break;

            }
        }
    }
    private int CalculateConversationChance(Belief belief1, Belief belief2)
    {

        int beliefDistance = Math.Abs(belief1.beliefStrengh - belief2.beliefStrengh);
        int[] chanceList = speakingOutcomeChances[beliefDistance];
        int outcome = -3;
        foreach(int chance in chanceList)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if(chance>= random)
            {
                return outcome;
            }
            outcome++;
        }

        return 0;
    }
    
}
