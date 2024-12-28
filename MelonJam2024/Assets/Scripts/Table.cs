using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] int tableSize = 4;
    [SerializeField] GameObject[] peopleAtTable;
    [SerializeField] GameObject winningScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(winningScreen != null)
           winningScreen.SetActive(false);

        peopleAtTable = new GameObject[tableSize];
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckEveryonesContraintsMet()){
                Debug.Log("Everyone at the table is happy");
                if(winningScreen != null)
                   winningScreen.SetActive(true);
        }        
    }

    public void AddPersonToTable(GameObject person, int position){
        if(position < tableSize){
            peopleAtTable[position] = person;
        }
    }

    public void RemovePersonFromTable(int position){
        if(position < tableSize){
            peopleAtTable[position] = null;
        }       
    }

    bool CheckEveryonesContraintsMet(){

        for(int i = 0; i < peopleAtTable.Length;i++){
            if(peopleAtTable[i] == null){
                //Debug.Log("Table not full");
                return false;
            } 

            var constaints = peopleAtTable[i].GetComponent<Constraints>();

            var leftNeighbor = peopleAtTable[(i - 1 + peopleAtTable.Length) % peopleAtTable.Length];
            var rightNeighbor = peopleAtTable[(i + 1) % peopleAtTable.Length];

            if (constaints.GetPeopleCantSitNextTo().Length != 0)
            {
                foreach (string p in constaints.GetPeopleCantSitNextTo())
                {
                    if (leftNeighbor != null && leftNeighbor?.GetComponent<Constraints>()?.GetID() == p){
                        return false;
                    } else if(rightNeighbor != null && rightNeighbor?.GetComponent<Constraints>()?.GetID() == p){
                        return false;
                    }
                }
            }
            
            if(constaints.GetPeopleMustSitNextTo().Length != 0){
                foreach (string p in constaints.GetPeopleMustSitNextTo())
                {
                    if (leftNeighbor != null && leftNeighbor.GetComponent<Constraints>()?.GetID() == p){
                        return false;
                    } else if(rightNeighbor != null && rightNeighbor.GetComponent<Constraints>()?.GetID() == p){
                        return false;
                    }
                }
            }

            GameObject oppositePerson;

            if(i == 0) {
                oppositePerson = peopleAtTable[tableSize/2];
            } else if( i == tableSize/2){
                oppositePerson = peopleAtTable[0];
            } else {
                oppositePerson = peopleAtTable[tableSize-i];
            }

            if (constaints.GetPeopleCannotFace().Length != 0){
                foreach (string p in constaints.GetPeopleCannotFace())
                {
                    if (oppositePerson != null && oppositePerson.GetComponent<Constraints>()?.GetID() == p){
                        return false;
                    }
                }
            }

            if(constaints.GetPeopleMustFace().Length != 0){
                foreach (string p in constaints.GetPeopleMustFace())
                {
                    if (oppositePerson != null && oppositePerson.GetComponent<Constraints>()?.GetID() != p){
                        return false;
                    }
                }
            }

            if(constaints.MustSitAtHead()){
                if(i != 0 && i != tableSize/2){
                    Debug.Log("Must be at head of table");
                    return false;
                }
            }       
        }

        return true;
    }
}
