using UnityEngine;
using System.Collections;

public class IntPair
{
	
	public int a, b;

    public IntPair()
    {
        this.a = -1;
        this.b = -1;
        
    }

    public IntPair(int a, int b)
	{
		this.a = a;
		this.b = b;
	}
	
	public bool containsBoth(int x1, int x2)
	{
		if (a == x1 && b == x2)
			return true;
		else if (a == x2 && b == x1)
			return true;
		else
			return false;
	}
}