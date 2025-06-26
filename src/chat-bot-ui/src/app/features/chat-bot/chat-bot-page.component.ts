import { Component, inject, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { ChatBotAppService } from '../../services/chat-bot-app.service';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { PostMessageCommand } from '../../models/post-message-command.model';

export interface Message {
  id: string;
  text: string;
  isUser: boolean;
  rating?: number;
}

@Component({
  selector: 'app-chat-bot-page',
  imports: [
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    NgClass,
    NgFor,
    NgIf,
    FormsModule],
  templateUrl: './chat-bot-page.component.html',
  styleUrl: './chat-bot-page.component.scss'
})
export class ChatBotPageComponent implements OnInit {
  public messages: Message[] = [];
  public userInput: string = '';
  public isTyping: boolean = false;
  public paused: boolean = false;
  private typingInterval: any;
  private currentBotMessage: string = '';
  private currentIndex: number = 0;

  private chatService: ChatBotAppService = inject(ChatBotAppService);

  ngOnInit(): void {
    localStorage.clear();
  }

  sendMessage(): void {
    if (!this.userInput.trim()) return;

    let message: Message = { text: this.userInput, isUser: true, id: '' };

    this.messages.push(message);

    const prompt = this.userInput;
    this.userInput = '';
    const command: PostMessageCommand = {
      message: prompt,
      isPrompt: true
    }

    this.chatService.postMessage(command).subscribe({
        next: (response) => {
          if (response.response.length) {
            this.startTyping(response.response);
          }
          localStorage.setItem('conversationId', response.conversationId);
        },
        error: (error) => {
            console.dir(error)
        }
    });
  }

  rateMessage(message: Message, rating: number): void {
    this.chatService.rateMessage(message.id, rating).subscribe({
      next: (success) => {
        message.rating = success ? rating : -rating;
        console.log(`Message ${message.id} rated successfully with ${rating}`);
      },
      error: (error) => {
        console.error(`Error rating message ${message.id}:`, error);
      }
    });
  }

  startTyping(botMessage: string,): void {
    if (!botMessage) {
      console.error('Bot message is undefined or null.');
      this.isTyping = false;
      return;
    }

    this.isTyping = true;
    this.paused = false;
    this.currentBotMessage = botMessage;
    this.currentIndex = 0;

    const botMessageObj: Message = { text: '', isUser: false, id: '' };
    this.messages.push(botMessageObj);

    this.typingInterval = setInterval(() => {
      if (this.paused) return;

      if (this.currentIndex < this.currentBotMessage.length) {
        botMessageObj.text += this.currentBotMessage[this.currentIndex++];
      } else {
        this.stopTyping();
      }
    }, 40);
  }

  stopTyping(): void {
    clearInterval(this.typingInterval);
    this.isTyping = false;

    const lastBotMessage = this.messages.find((message) => !message.isUser && !message.id);
    if (!lastBotMessage) {
      console.error('No bot message found to stop typing.');
      return;
    }

    const command: PostMessageCommand = {
      message: lastBotMessage.text,
      isPrompt: false
    };

    this.chatService.postMessage(command).subscribe({
      next: (response) => {
        lastBotMessage.id = response.messageId
        console.log('Message saved:', response);
      },
      error: (error) => {
        console.error('Error saving message:', error);
      }
    });
  }

  pauseTyping(): void {
    this.paused = true;
  }

  resumeTyping(): void {
    this.paused = false;
  }
}
