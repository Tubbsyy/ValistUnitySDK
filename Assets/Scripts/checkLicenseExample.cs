using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using Valist;

public class checkLicenseExample : MonoBehaviour
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
		bool verified = await ValistSDK.checkLicense(accountName, projectName);
		if (verified == true)
		{
			// Do something, change scene, instantiate object, etc.
			Debug.Log("License Verified");
		}
		else
		{
			Debug.Log("License Not Verified");
		}
	}
}
