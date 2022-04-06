# WebChat

## Client
In order to install client dependencies, you should access "WebChatClient" folder and run "npm install" command.
Once you have all done, just run the project using "ng serve". It will start the server on port "4200".

## Server
To have the server up and running, you should just restore all the package dependencies (it will probably happen when building the project).
This project will run on port "44331".
The database used is SQL local database. To create de database for the project, in the Nuger Package Manager console, run the command "Update-Database".
There are some unit tests that are testing messages controller and login controller.

## Message broker
The message broker used in this project is rabbitMQ. So, to get this properly working, you should install rabbitMQ in your machine and start it's service.

## General instructions
Once the front, back-end and rabbitMQ are running, you can acess "localhost:4200". Then, you can register a new user and login with this user. If the login suceed, you will have access to chat room. First, you should select your Group and join. Then, you will be able to send receive messages that are just being sent to that specific room.
