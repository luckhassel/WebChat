# WebChat

## Client
In order to install client dependencies, you should access "WebChatClient" folder and run "npm install" command.
Once you have all done, just run the project using "ng serve". It will start the server on port "4200".

## Server
To have the server up and running, you should just restore all the package dependencies (it will probably happen when building the project).
This project will run on port "44331".
This will consume and store data in your local SQL database.
There are some unit tests that are testing messages controller and login controller.

## Message broker
The message broker used in this project is rabbitMQ. So, to get this properly working, you should install rabbitMQ in your machine and start it's service.
