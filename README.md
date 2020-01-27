# WeightedRoundRobin

Weighted Round Robin with CPU Usage pattern Implementation

* Dot Core Web Api
* Console App

Postman Collection for Api: https://www.getpostman.com/collections/59d69862ec95dfa4fa7c

Below are the apis:
* **Get Server List**: (Get) http://ec2-3-93-5-187.compute-1.amazonaws.com:5001/server/list
* **Get Server**: (Get) http://ec2-3-93-5-187.compute-1.amazonaws.com:5001/server
* **Add Server(s)**: (Post) http://ec2-3-93-5-187.compute-1.amazonaws.com:5001/server
  * Post Body: 
  ```
  [
    {
      "Weight": 100,
      "CpuThreshold": 80
    },
    {
      "Weight": 50,
      "CpuThreshold": 90
    },
    {
      "Weight": 25,
      "CpuThreshold": 25
    }
  ]
* **Update Server**: (Put) http://ec2-3-93-5-187.compute-1.amazonaws.com:5001/server
  * Put Body: 
  ```
  {
    "id": 1,
    "CpuUsage": 85
  }
