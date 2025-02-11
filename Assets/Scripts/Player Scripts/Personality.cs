using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality : MonoBehaviour
{

    
    public enum BeliefStrengh
    {
        ABHORRENT,
        DISTAINS,
        DISLIKES,
        NUTRAL,
        LIKES,
        RESPECTS,
        BELIVER
    }
    
    public enum Beliefs
    {
        LAWFUL = 0,
        LOYALTY,
        FAIMLY,
        FRIENDSHIP,
        POWER,
        TRUTH,
        CUNNING,
        EQLOQUINCE,
        FIARNESS,
        DECORUM,
        TRADITION,
        ARTWORK,
        COOPERATION,
        STOICISM,
        INTROSPECTION,
        SELFCONTROL,
        TRANQUILITY,
        HARMONY,
        MERRIMENT,
        CRAFTSMANSHIP,
        MARTIALPROWESS,
        SKILL,
        HARDWORK,
        SACRIFICE,
        COMPETITION,
        PERSERVERANCE,
        LEISURETIME,
        COMMERCE,
        ROMANCE,
        NATURE,
        PEACE,
        KNOWLEDGE
    }
    public BeliefStrengh lawful = BeliefStrengh.NUTRAL;
    public BeliefStrengh loyalty = BeliefStrengh.NUTRAL;
    public BeliefStrengh family = BeliefStrengh.NUTRAL;
    public BeliefStrengh friendship = BeliefStrengh.NUTRAL;
    public BeliefStrengh power = BeliefStrengh.NUTRAL;
    public BeliefStrengh truth = BeliefStrengh.NUTRAL;
    public BeliefStrengh cunning = BeliefStrengh.NUTRAL;
    public BeliefStrengh eqloquince = BeliefStrengh.NUTRAL;
    public BeliefStrengh fiarness = BeliefStrengh.NUTRAL;
    public BeliefStrengh decorum = BeliefStrengh.NUTRAL;
    public BeliefStrengh tradition = BeliefStrengh.NUTRAL;
    public BeliefStrengh artwork = BeliefStrengh.NUTRAL;
    public BeliefStrengh cooperation = BeliefStrengh.NUTRAL;
    public BeliefStrengh stoicism = BeliefStrengh.NUTRAL;
    public BeliefStrengh introspection = BeliefStrengh.NUTRAL;
    public BeliefStrengh selfControl = BeliefStrengh.NUTRAL;
    public BeliefStrengh tranquility = BeliefStrengh.NUTRAL;
    public BeliefStrengh harmony = BeliefStrengh.NUTRAL;
    public BeliefStrengh merriment = BeliefStrengh.NUTRAL;
    public BeliefStrengh craftsmanship = BeliefStrengh.NUTRAL;
    public BeliefStrengh martialProwess = BeliefStrengh.NUTRAL;
    public BeliefStrengh skill = BeliefStrengh.NUTRAL;
    public BeliefStrengh hardWork = BeliefStrengh.NUTRAL;
    public BeliefStrengh sacrifice = BeliefStrengh.NUTRAL;
    public BeliefStrengh competition = BeliefStrengh.NUTRAL;
    public BeliefStrengh perserverance = BeliefStrengh.NUTRAL;
    public BeliefStrengh leisureTime = BeliefStrengh.NUTRAL;
    public BeliefStrengh commerce = BeliefStrengh.NUTRAL;
    public BeliefStrengh romance = BeliefStrengh.NUTRAL;
    public BeliefStrengh nature = BeliefStrengh.NUTRAL;
    public BeliefStrengh peace = BeliefStrengh.NUTRAL;
    public BeliefStrengh knowledge = BeliefStrengh.NUTRAL;

    public const int beliefMax = 31;
    public List<Belief> beliefsList = new List<Belief>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < beliefMax; i++)
        {
            this.beliefsList.Add(new Belief((Beliefs)i, BeliefStrengh.NUTRAL, 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Belief GetRandomBelief()
    {
        int rand = UnityEngine.Random.Range(0, beliefMax);
        return this.beliefsList[rand];
    }

    public Belief GetBelief(Beliefs beliefs)
    {
        return this.beliefsList[(int)beliefs];
    }
}
