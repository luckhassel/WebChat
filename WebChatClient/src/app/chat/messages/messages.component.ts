import { Component, OnInit } from '@angular/core';
import * as signalr from '@microsoft/signalr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from 'src/app/login/login.service';
import * as signalR from '@microsoft/signalr';
import { IHttpConnectionOptions } from '@microsoft/signalr';

interface Message  {
  user: string,
  content: string
  date: Date
}

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})

export class MessagesComponent implements OnInit {
  title = "Chat Messages"

  options: IHttpConnectionOptions = {
    accessTokenFactory: () => {
      return this.login.getToken();
    }
  };

  messages: Message[] = [];
  firstMessages: Message[] = [];
  
  messageToSend: string = "";
  connection = new signalr.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("https://localhost:44331/chatHub", this.options)
    .build();

  constructor(private http: HttpClient, private login: LoginService) 
  {
    this.getFirstMessages(); 
    this.startConnection();
  }

  ngOnInit(): void {
  }

  header = {
    headers: new HttpHeaders()
      .set('Authorization',  `Bearer ${this.login.getToken()}`)
  }
  
  getFirstMessages(){
    this.http.get<Message[]>("https://localhost:44331/api/chat", this.header)
              .subscribe(response => {
                for (var i = 0; i < response.length; i++){
                  this.messages.push(response[i]);
                }
              });
  }

  startConnection(){
    this.connection.start();

    this.connection.on("SendMessage", (user: string, content: string, date: Date) => {
      if(this.messages.length >= 50) delete this.messages[0];
      this.messages.push({
        user: user,
        content: content,
        date: date
      });
    });
  }

  sendMessage(){
    this.connection.invoke("SendMessage", this.login.getUsername(), this.messageToSend, );
    this.messageToSend = "";
  }
}
