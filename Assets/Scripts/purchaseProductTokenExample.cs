using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine.UI;
using Valist;
using TMPro;


public class purchaseProductTokenExample : MonoBehaviour
{   

    public TMP_Dropdown preferredToken;

    // public string accountName = "<your-account-name-here>";

    // public string projectName = "<your-project-name-here>";
	 
	 public TMP_InputField accountNameInput;

	 public TMP_InputField projectNameInput;

	 private string accountName;
	 private string projectName;	
    ValistUnitySDK ValistSDK = new ValistUnitySDK();
      
    
   public Button yourButton;

	void Start () {
	    
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

     
  public async void TaskOnClick(){
        string accountName = accountNameInput.text;
        string projectName = projectNameInput.text;
        
		bool purchase = await ValistSDK.purchaseProductToken(accountName, projectName, preferredToken.options[preferredToken.value].text);
	    
        if (purchase == false)
        {
            Debug.Log("Purchase Failed");
        }
        else
        {
            Debug.Log("Purchase Successful");
        }
        
    
    
    
    }
}
