Apple Receipt Verifier
======================
The goal of this project is to provide a strongly typed method of returning the information returned from the apple receipt verification process outlined here: http://developer.apple.com/library/ios/documentation/NetworkingInternet/Conceptual/StoreKitGuide/VerifyingStoreReceipts/VerifyingStoreReceipts.html.

How to use
==========
1. Install package using nuget.
2. Where ever you want to use the code reference the AppleReceiptVerifier namespace.
3. Use the following code to validate a receipt

```
ReceiptManager receiptManager = new ReceiptManager();
var result = receiptManager.ValidateReceipt(AppleReceiptVerifier.Environments.Production, "Your Receipt String Here");
```

The result will be a stronly typed version of the receipt json you get back from Apple.  Properties returned:

* result.Status
* result.RawResponse
* result.Receipt.AppItemId
* result.Receipt.ApplicationVersionNumber
* result.Receipt.BundleIdentifier
* result.Receipt.ItemId
* result.Receipt.OriginalPurchaseDateMilliseconds
* result.Receipt.OriginalPurchaseDatePst
* result.Receipt.OriginalPurchaseDateUtc
* result.Receipt.OriginalTransactionId
* result.Receipt.ProductId
* result.Receipt.PurchaseDateMilliseconds
* result.Receipt.PurchaseDatePst
* result.Receipt.PurchaseDateUtc
* result.Receipt.Quantity
* result.Receipt.TransactionId
* result.Receipt.UniqueIdentifier
* result.Receipt.UniqueVendorIdentifier
* result.Receipt.VersionExternalIdentifier
