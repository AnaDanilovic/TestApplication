## WPF test app
Enables user to upload json file in following format:
```json
{
    "identifier": "SG9999000152",
    "navHistory": {
                    "navHistory": [
                        {
                            "countryOfQuotation": "SG",
                            "currency": "USD",
                            "nav": ".8568",
                            "navDate": "20221017",
                            "receivedTime": "2022-10-18 03:49:57",
                            "sourcePriority": "2",
                            "volumeRank": " 9999"
                        },
                        {
                            "countryOfQuotation": "SG",
                            "currency": "SGD",
                            "nav": "1.2183",
                            "navDate": "20221017",
                            "receivedTime": "2022-10-18 03:49:59",
                            "sourcePriority": "2",
                            "volumeRank": " 9999"
                        }
                      ]
                }
}
```
App removes duplicates based on navDate, keeping ones that have bigger value in sourcePriority, volumeRand fields and have most recent date in receivedTime field.
Prints out the results nav, navDate, and receivedTIme to the screen, enabling user to save result to csv document on local.

## How to run localy
Clone repository, make sure it builds, hit F5.
