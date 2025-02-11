using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belief
{
    public Personality.Beliefs beliefs;
    public Personality.BeliefStrengh beliefStrengh;
    public int resolve = 5;

    public Belief(Personality.Beliefs beliefs, Personality.BeliefStrengh beliefStrengh, int resolve)
    {
        this.beliefs = beliefs;
        this.beliefStrengh = beliefStrengh;
        this.resolve = resolve;
    }
}
