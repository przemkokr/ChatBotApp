import {inject, Injectable } from "@angular/core";
import { environment } from "../../../environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { PostMessageCommand, PostMessageResponse } from "../models/post-message-command.model";

@Injectable({
  providedIn: "root",
})
export class ChatBotAppService {
    private api_url = environment.API_URL;
    private http: HttpClient = inject(HttpClient);

    public postMessage(postMessageCommand: PostMessageCommand): Observable<PostMessageResponse> {
      const conversationId = localStorage.getItem('conversationId');
      if (conversationId !== 'undefined' && conversationId !== null) {
        postMessageCommand.conversationId = conversationId;
      }

      return this.http.post<PostMessageResponse>(`${this.api_url}/conversation`, postMessageCommand)
    }

    public rateMessage(messageId: string, rating: number): Observable<boolean> {
      let conversationId = localStorage.getItem('conversationId');
      if (conversationId !== 'undefined' && conversationId !== null) {
        conversationId = conversationId as string;
      }

      return this.http.post<boolean>(`${this.api_url}/conversation/${conversationId}/messages/${messageId}/rate`, { rating: rating });
    }
}
