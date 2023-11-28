export interface BaseCommandResponse {
  id: string;
  success: boolean;
  message: string;
  errors: string[];
}

export interface DonViNguoiDaiDienResponse {
  response_1: BaseCommandResponse;
  response_2: BaseCommandResponse;
}