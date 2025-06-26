export interface PostMessageCommand {
  conversationId?: string;
  message: string;
  isPrompt: boolean;
}

export interface PostMessageResponse {
  conversationId: string;
  response: string;
  messageId: string;
}
