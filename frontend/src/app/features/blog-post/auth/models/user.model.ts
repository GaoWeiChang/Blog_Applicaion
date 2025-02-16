export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    email: string;
    roles: string[];
}

export interface RegisterRequest {
    email: string;
    password: string;
}

export interface User {
    email: string;
    roles: string[];
}