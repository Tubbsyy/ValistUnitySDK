# ValistUnitySDK

Submission for Valist's Unity SDK bounty on LearnWeb3

To get started, import the .unitypackage into your Unity project and there are examples of how to utilize the functions from the SDK!

First initialize an instance of the SDK in your variables with

## ValistUnitySDK ValistSDK = new ValistUnitySDK();
now you can call functions from the SDK into your script!

### Examples:
For checking if a user owns your game's NFT license
## bool verified = ValistSDK.checkLicense(accountName, projectName)

Purchasing a NFT license from your game with the native token, MATIC
## bool purchase = await ValistSDK.purchaseProduct(accountName, projectName)

Purchasing a NFT license from your game with an ERC20 token
## bool purchaseToken = await ValistSDK.purchaseProductToken(accountName, projectName, preferredToken)
