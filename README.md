# ValistUnitySDK

This is a submission for a bounty on LearnWeb3

## Getting Started

To get started, import the `.unitypackage` into your Unity project. You will notice that there is an example scene showing how to utilize the functions from the SDK!

First, initialize an instance of the SDK in your variables with:

```bash
ValistUnitySDK ValistSDK = new ValistUnitySDK();
```

After initializing, you can call functions from the SDK inside your script!

# Examples: 

## For checking if a user owns your game's NFT license

```bash
bool verified = ValistSDK.checkLicense(accountName, projectName)
```

## Purchasing an NFT license from your game with the native token, MATIC

```bash
bool purchase = await ValistSDK.purchaseProduct(accountName, projectName)
```

## Purchasing an NFT license from your game with an ERC20 token

```bash
bool purchaseToken = await ValistSDK.purchaseProductToken(accountName, projectName, preferredToken)
```
