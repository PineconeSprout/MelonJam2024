using UnityEngine;

public class Constraints : MonoBehaviour
{
    [SerializeField] private string nameID;
    [SerializeField] private string[] cannotFace; 
    [SerializeField] private string[] cannotSitNextTo; 
    [SerializeField] private string[] mustFace; 
    [SerializeField] private string[] mustSitNextTo; 
    [SerializeField] private bool mustSitAtHead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public string GetID(){
        return nameID;
    }
    public string[] GetPeopleCannotFace(){
        return cannotFace;
    }

    public string[] GetPeopleCantSitNextTo(){
        return cannotSitNextTo;
    }

    public string[] GetPeopleMustFace(){
        return mustFace;
    }
    
    public string[] GetPeopleMustSitNextTo(){
        return mustSitNextTo;
    }

    public bool MustSitAtHead(){
        return mustSitAtHead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
