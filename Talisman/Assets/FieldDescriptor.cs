using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDescriptor
{
    public FieldDescriptor(Field[] outerRing, Field[] middleRing, Field[] innerRing)
    {
        descriptOuterRing(outerRing);
        //descriptMiddleRing(middleRing);
        //descriptInnerRing(innerRing);
    }
	private void descriptOuterRing(Field[] outerRing)
    {
        for (int i = 0; i < 24; i++)
        {
            switch (i)
            {
                case 0:
                    outerRing[i].set_fieldDescription("historycznie pierwsze opisane pole");
                    break;
                case 1:
                    outerRing[i].set_fieldDescription("historycznie drugie opisane pole");
                    break;
                case 2:
                    outerRing[i].set_fieldDescription("historycznie trzecie opisane pole");
                    break;
            }
        }
    }
    private void descriptMiddleRing(Field[] middleRing)
    {
        for (int i = 0; i < 16; i++)
        {
            switch (i)
            {
                case 0:
                    break;
            }
        }
    }
    private void descriptInnerRing(Field[] innerRing)
    {
       for (int i=0; i<8; i++)
       {
            switch (i)
            {
                case 0:
                    break;
            }
       }

        
    }
}
