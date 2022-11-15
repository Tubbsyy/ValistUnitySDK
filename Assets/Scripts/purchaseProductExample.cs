using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using Valist;
public class purchaseProductExample : MonoBehaviour
{
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
		bool purchase = await ValistSDK.purchaseProduct(accountName, projectName);
        if (purchase == true)
        {
            //Do something
            Debug.Log("Purchase Successful");
        }
        else
        {
            Debug.Log("Purchase Failed");
        }


      
	}
}
