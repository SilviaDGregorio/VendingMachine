# Decisions

* I'm assuming that the user's coins are available for returning.
* You can't insert negative coins
* In the web page you can insert the real coins(2€,1€,50cent,20cent,10cent,5cent,2cent,1cent)
* I assume that the coins are all €
* The stock of products and the stock of coins last until the application stops.
* I'm not storing the state of the application


# Run in Visual Studio

* Open two visual studio:
  * one with the web as starting project
  * another with the api as starting project
* Make sure that you run the Api before the web.

# Run in a Terminal
```
$ cd src/Machine.Api && dotnet run 
```
```
$ cd src/Web && dotnet run
```
ApiUrl -> https://localhost:5000

WebUrl -> https://localhost:5002