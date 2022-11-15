using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Util;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Numerics;


namespace Valist
{
public class ValistUnitySDK : MonoBehaviour
 {

    private static readonly string address = "0x3cE643dc61bb40bB0557316539f4A93016051b81";

    

    

    private string network = "polygon";
    private string chain = "ethereum";
    
    


    public async Task<bool> checkLicense(string accountname, string projectname)
    {
      
        try
        {
            var verified = await Verify(accountname, projectname);
            if (verified)
            {
                return await Task.FromResult(true);
                print("License Verified");
            }
            else
            {
                return await Task.FromResult(false);
                print("License Not Verified");
            }
        }
        catch (Exception ex)
        {
           return await Task.FromResult(false);
           Debug.Log(ex.Message);
           
        }
    }
    

    private async Task<bool> Verify(string accountName, string projectName)
    {
        // sign a message to prove ownership
        var message = "Valist License verification";
        var signature = await Web3Wallet.Sign(message);

        // EC recover the signer account
        var signer = new EthereumMessageSigner();
        var account = signer.EncodeUTF8AndEcRecover(message, signature);

        
            // generate project id
            var chainId = "0x0000000000000000000000000000000000000000000000000000000000000089";
            var accountId = GenerateId(chainId, accountName);
            var projectId = GenerateId(accountId, projectName);
            var tokenId = HexToDecimalString(projectId);
 
            // check license balance
            var balance = await ERC1155.BalanceOf("polygon", "mainnet", address, account, tokenId);
            if (balance.IsZero)
            {
                return false;
            }
            else
            {
                return true;
            }
            
    }

    


    private string HexToDecimalString(string hex)
    {
        var number = HexBigIntegerConvertorExtensions.HexToBigInteger(hex, false);
        return number.ToString();
    }

    private string GenerateId(string parentId, string name)
    {
        var nameHash = Sha3Keccack.Current.CalculateHash(name);
        return Sha3Keccack.Current.CalculateHashFromHex(parentId, nameHash);
    }
    

    public async Task<bool> purchaseProduct(string accountName, string projectName)
     {

        string chain = "ethereum";
        string network = "polygon";

         // abi in json format
         string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_projectID\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"_recipient\",\"type\":\"address\"}],\"name\":\"purchase\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"}]";
 
         string contract = "0x3cE643dc61bb40bB0557316539f4A93016051b81";
         // method you want to write to
         string method = "purchase";

         // generate token id
         var chainNum = "0x0000000000000000000000000000000000000000000000000000000000000089";
         var accountId = GenerateId(chainNum, accountName);
         var projectId = GenerateId(accountId, projectName);
         var tokenId = HexToDecimalString(projectId);
            
        // Get the account address of the user
         var message = "Valist License verification";
         var signature = await Web3Wallet.Sign(message);
         var signer = new EthereumMessageSigner();
         var account = signer.EncodeUTF8AndEcRecover(message, signature);
         var _recipient = account;

         // get the price of your project license in MATIC
         var price = await getProductPrice(tokenId);
         
         // array of arguments for contract       
        string args = "[\""+tokenId+"\",\""+_recipient+"\"]";
        // create data for contract interaction
        string data = await EVM.CreateContractData(abi, method, args);
        // send transaction
         // string gaslimit = Optional;
         // string gasPrice = Optional
        var chainId = "137";
        
        try {
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, contract, price, data, "", "");
        // display response in Unity console
        if (response.Contains("error"))
        {
            Debug.Log(response);
           return await Task.FromResult(false);
        }
        else
        {
            Debug.Log(response);
            return await Task.FromResult(true);
        }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return await Task.FromResult(false);
        }
     }


public async Task<bool> purchaseProductToken(string accountName, string projectName, string preferredToken)
     {
        
        var tokenAddress = "";
        if (preferredToken == "Wrapped Eth") {tokenAddress = "0x7ceB23fD6bC0adD59E62ac25578270cFf1b9f619";};    
        if (preferredToken == "Wrapped Matic") {tokenAddress = "0x0d500B1d8E8eF31E21C99d1Db9A6444d3ADf1270";};
        if (preferredToken == "USDC") {tokenAddress = "0x2791Bca1f2de4661ED88A30C99A7a9449Aa84174";};
        if (preferredToken == "USDT") {tokenAddress = "0xc2132D05D31c914a87C6611C10748AEb04B58e8F";};
        if (preferredToken == "DAI") {tokenAddress = "0x8f3Cf7ad23Cd3CaDbD9735AFf958023239c6A063";};
        if (preferredToken == "QuickSwap") {tokenAddress = "0xB5C064F955D8e7F38fE0460C556a72987494eE17";};
        if (preferredToken == "AAVE") {tokenAddress = "0xD6DF932A45C0f255f85145f286eA0b292B21C90B";};
        if (preferredToken == "Chainlink") {tokenAddress = "0xb0897686c545045aFc77CF20eC7A532E3120E0F1";};
        string chain = "ethereum";
        string network = "polygon";


        // abi in json format
        string abi = "[{\"inputs\":[{\"internalType\":\"contract IERC20\",\"name\":\"_token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"_projectID\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"_recipient\",\"type\":\"address\"}],\"name\":\"purchase\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        // Valist License contract address
        string contract = "0x3cE643dc61bb40bB0557316539f4A93016051b81";
        
        // method you want to write to
        string method = "purchase";


         // Generates the token Id for your project's license, used to get the price of the license and to purchase the license
         var chainNum = "0x0000000000000000000000000000000000000000000000000000000000000089";
         var accountId = GenerateId(chainNum, accountName);
         var projectId = GenerateId(accountId, projectName);
         var tokenId = HexToDecimalString(projectId);
         
         // Get the account address of the user
         var message = "Valist License verification";
         var signature = await Web3Wallet.Sign(message);
         var signer = new EthereumMessageSigner();
         var account = signer.EncodeUTF8AndEcRecover(message, signature);
         var _recipient = account;
         // Get the price of the license in the token the user selected
         var price = await getProductPriceToken(tokenAddress, tokenId);
        
         
         // Approves the VAlist License contract to spend the token the user selected from the User's balance. Only allocates the amount needed to purchase the license
         var approve = await approveERC20Tokens(tokenAddress, price);
         if (approve == false)
         {
                return await Task.FromResult(false);
            
             Debug.Log("Failed to approve tokens to be spent");
         } 
         else 
         {
         
         // array of arguments for contract you can also add a nonce here as optional parameter   
         string args = "[\""+tokenAddress+"\",\""+tokenId+"\",\""+_recipient+"\"]";
         
         // create data for contract interaction
         string data = await EVM.CreateContractData(abi, method, args);
         
         // string gaslimit = optional;
         // string gasPrice = optional;
         
         // Network ID for Polygon
         var chainId = "137";

         // Send transaction
         try {
         string response = await Web3Wallet.SendTransaction(chainId, contract, "0", data, "", "");
          if (response.Contains("error"))
          {
              Debug.Log(response);
              return await Task.FromResult(false);
          }
          else
          {
              Debug.Log(response);
              return await Task.FromResult(true);
          }
         

         }
            catch (Exception e)
            {
                 return await Task.FromResult(false);
                Debug.Log(e);
            }
         }

     }


public async Task<string> getProductPrice(string tokenId)
    {
     
       // set chain
       
      
       // set network
       
       string method = "getPrice";
      
       // abi in json format
       string abi = "[ { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_projectID\", \"type\": \"uint256\" } ], \"name\": \"getPrice\", \"outputs\": [{ \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" }], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
     
       // address of contract
       string contract = "0x3cE643dc61bb40bB0557316539f4A93016051b81";
      
       // array of arguments for contract
       string args = "[\""+tokenId+"\"]";
       string rpc = "https://polygon-rpc.com/"; // network rpc
     
       // connects to user's browser wallet to call a transaction
       string response = await EVM.Call(chain, network, contract, abi, method, args, rpc);
        return await Task.FromResult(response);
        

}

public async Task<string> getProductPriceToken(string _token, string tokenId)
   {
       
       
       
      
       // set network
       
       string method = "getPrice";
      
       // abi in json format
       string abi = "[ { \"inputs\": [ { \"internalType\": \"contract IERC20\", \"name\": \"_token\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"_projectID\", \"type\": \"uint256\" } ], \"name\": \"getPrice\", \"outputs\": [{ \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" }], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
      
       // address of contract
       string contract = "0x3cE643dc61bb40bB0557316539f4A93016051b81";
      
       // array of arguments for contract
       string[] obj = {_token, tokenId};
       string args = JsonConvert.SerializeObject(obj);
       string rpc = "https://polygon-rpc.com/"; // network rpc
     
       // connects to user's browser wallet to call a transaction
       string response = await EVM.Call(chain, network, contract, abi, method, args, rpc);
         return await Task.FromResult(response);
   }


public async Task<bool> approveERC20Tokens(string _token, string price)
   {
        
        string chainId = "137"; // polygon chain id 
         
        string contract = _token; // address of token user wants to use
        
        string value = "0"; // we're just sending an approve function to the wallet so this needs to be 0
        
        // abi in json format
        string abi = "[ { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_sender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"_value\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [{ \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" }], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";
        
        // smart contract method to call
        string method = "approve"; // This will set the allowance for the contract to spend the user's tokens in the purchaseProductToken function
        
        // account to send erc20 to
        string toAccount = "0x3cE643dc61bb40bB0557316539f4A93016051b81"; // address of the Valist contract
        
        // amount of erc20 tokens to send
        string amount = price; // price of your product in the token the user selected
        
        // array of arguments for contract
        string[] obj = {toAccount, amount};
        string args = JsonConvert.SerializeObject(obj);
        
        // create data to interact with smart contract
        string data = await EVM.CreateContractData(abi, method, args);
        
        // gas limit OPTIONAL
        string gasLimit = "";
        
        // gas price OPTIONAL
        string gasPrice = "";
        
        try {
        var response = await Web3Wallet.SendTransaction(chainId, contract, value, data, gasLimit, gasPrice);
        
        if (response.Contains("error"))
        {
            Debug.Log(response);
            return await Task.FromResult(false);
        }
        else
        {
            Debug.Log(response);
            return await Task.FromResult(true);
        }
        }
        catch (Exception e)
        {
             return await Task.FromResult(false);
            Debug.Log(e);
        }
   }


 }

}
