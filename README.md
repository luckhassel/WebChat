# WebChat

## Client
In order to install client dependencies, you should access "WebChatClient" folder and run "npm install" command.
Once you have all done, just run the project using "ng serve". It will start the server on port "4200".

## Server
To have the server up and running, you should just restore all the package dependencies for WebChat folder (it will probably happen when building the project).
This project will run on port "44331".
The database used is SQL local database. To create the database for the project, in the Nuget Package Manager console, run the command "Update-Database".
There are some unit tests that are testing services, that contains the main business logic.

## Bot
To have the bot up and running, you should just restore all the package dependencies for Bot folder (it will probably happen when building the project).
This project will run on port "44308".
There are some unit tests that are testing services, that contains the main business logic.

## Message broker
The message broker used in this project is rabbitMQ. So, to get this properly working, you should install rabbitMQ in your machine and start it's service.

## General instructions
Once the front-end, back-end, bot and rabbitMQ are running, you can acess "localhost:4200". Then, you can register a new user and login with this user. If the login succeed, you will have access to the chat room. First, you should select your Group and join (you can type any room name). Then, you will be able to see the last 50 messages, ordered by date descending, sent in that group, and, of course, send and receive instante messages. There, you will be able to interact with the bot as well, just typing "/stock=<stock_name>"
