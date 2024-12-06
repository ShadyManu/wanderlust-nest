export interface Result<T> {
  data: T | null;
  error: ResultError | null;
}

export interface ResultError {
  message: string;
  innerException?: string;
}

export interface CreateNoteDto {
  text: string;
}

export interface UpdateNoteDto {
  noteId: string;
  text: string;
  isFavourite: boolean;
}

export interface LoginResponse {
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
  tokenType: string;
}
