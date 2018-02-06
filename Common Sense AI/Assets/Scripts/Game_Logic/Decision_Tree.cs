using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision_Tree : MonoBehaviour
{


    public bool ProcessAttackOrder(float piercing, float armour)
    {
        if (piercing >= armour) // if their attack is able tp penetrate then good order
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool ProcessMoveOrder(float disapline, float fatigue)
    {
        if (disapline < fatigue) // if their attack is able tp penetrate then good order
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Update()
    {
    }

    void  ProcessMoraleCheck()
    {


        for (int i = 0; i < GetComponent<Mouse_Input>().selectedUnits.Count; i++) // will do a for loop thro all of the selected units
        {
            int morale = GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().GetMorale();// will set the morale to the one it is looking at

            int squadSize = GetComponent<Mouse_Input>().selectedUnits.Count;// will set the current unit size to that of the number of objects in the mouse input array

            int numberAlive = 0; // number of alive set to 0 

            for (int j = 0; j < squadSize; j++)// will go through the entire squad to 
            {
                if (GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().GetIsDead()) // do an if
                {
                    numberAlive++;// in order to see how many r alive 
                }
            }
            int losses = squadSize - numberAlive; // will then take away the number of alive from the squadsize to work out how many were killed 


            if (morale < losses) // if they loss more then what their morale can take
            {
                // they will run away from the enemy
                GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().SetIsRetreating(true);

            }

            else
            {
                // else carry on as normal 
                //return false;
            }

            /*
            Vector3 avgEnemyPos;

            for (int n = 0; n < GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits.Count; n++)
            {

                avgEnemyPos.x =+ GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits[n].transform.position.x;
                avgEnemyPos.z =+ GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits[n].transform.position.z;
                avgEnemyPos.y =+ GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits[n].transform.position.y;

                avgEnemyPos.x /= GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits.Count;
                avgEnemyPos.z /= GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits.Count;
                avgEnemyPos.y /= GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().engagedUnits.Count;
            }

            Vector3 retreatPos;
            retreatPos = (GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().transform.position + avgEnemyPos)
            GetComponent<Mouse_Input>().selectedUnits[i].GetComponent<Unit_Movement>().SetUnitGoal(retreatPos);

           // return true;

        }


    }

   // return false;
   */
        }
    }
}



