export interface BaseCommandResponse {
  id: string;
  success: boolean;
  message: string;
  errors: string[];
}
