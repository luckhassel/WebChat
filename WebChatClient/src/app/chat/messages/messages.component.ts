import { Component, OnInit } from '@angular/core';
import * as signalr from '@microsoft/signalr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from 'src/app/login/login.service';
import * as signalR from '@microsoft/signalr';
import { IHttpConnectionOptions } from '@microsoft/signalr';
import { BaseurlService } from 'src/app/services/baseurl.service';

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
  lowerlimit = 0;
  joined = false;
  
  messageToSend: string = "";
  connection = new signalr.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl(`${this.baseUrl.getBaseUrl()}/chatHub`, this.options)
    .build();

  constructor(private http: HttpClient, private login: LoginService, private baseUrl: BaseurlService) 
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
    this.http.get<Message[]>(`${this.baseUrl.getBaseUrl()}/api/chat/message?amount=50`, this.header)
              .subscribe(response => {
                for (var i = (response.length - 1); i >= 0; i--){
                  this.messages.push(response[i]);
                }
              });
  }

  startConnection(){
    this.connection.start().then(() => { this.joined = true; }, (error) => console.log("Failed to connect to Hub"));

    this.connection.on("SendMessage", (user: string, content: string, date: Date) => {
      this.lowerlimit++;
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
