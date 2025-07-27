import type { UserProfile } from "../features/auth/schemas/userProfileSchema";

// UserDto TypeScript interface matching your C# record
export interface UserDto {
  id: string;
  name: string;
  userName: string;
  email: string;
  address: string;
  phone: string;
  city: string;
  country: string;
  profileImage: string;
  role: string;
}

export interface RefreshToken {
  accessToken: string;
  refreshToken: string;
}

export interface ApiUserResponse {
  user: UserDto | null;
  accessToken: string;
  refreshToken: string;
  role: string;
}
// Token key and user key constants
const TOKEN_KEY = "token";
const USER_KEY = "user";
const REFRESH_TOKEN_KEY = "refreshToken";

// Token functions
export function addToken(token: string) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function getToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}

export function removeToken() {
  localStorage.removeItem(TOKEN_KEY);
}

export function addRefreshToken(token: string) {
  localStorage.setItem(REFRESH_TOKEN_KEY, token);
}

export function getRefreshToken(): string | null {
  return localStorage.getItem(REFRESH_TOKEN_KEY);
}

export function removeRefreshToken() {
  localStorage.removeItem(REFRESH_TOKEN_KEY);
}

// User functions
export function addUser(user: UserDto | null) {
  localStorage.setItem(USER_KEY, JSON.stringify(user));
}

export function getUser(): UserDto | null {
  const user = localStorage.getItem(USER_KEY);
  return user ? (JSON.parse(user) as UserDto) : null;
}

export function removeUser() {
  localStorage.removeItem(USER_KEY);
}

export function addPasswordResetToken(token: string) {
  localStorage.setItem("passwordResetToken", token);
}

export function getPasswordResetToken(): string | null {
  return localStorage.getItem("passwordResetToken");
}

export function removePasswordResetToken() {
  localStorage.removeItem("passwordResetToken");
}

export function getUserRole(): string | null {
  const user = getUser();
  return user ? user.role : null;
}

export function isAdmin(): boolean {
  const role = getUserRole();
  return role === "Admin";
}

export function getUserName(): string | null {
  const user = getUser();
  return user ? user.name : null;
}

export function getUserId(): string | null {
  const user = getUser();
  return user ? user.id : null;
}

export function updateUserProfile(updatedUser: UserProfile) {
  const user = getUser();
  if (user) {
    const updated: UserDto = {
      ...user,
      name: updatedUser.name,
      userName: updatedUser.userName,
      email: updatedUser.email,
      address: updatedUser.address || "",
      phone: updatedUser.phone || "",
      city: updatedUser.city || "",
      country: updatedUser.country || "",
      profileImage: updatedUser.profileImage || "",
    };
    addUser(updated);
  }
}

export function getBasketUserName(): string {
  const key = "basketUsername";
  const existing = localStorage.getItem(key);

  if (existing) return existing;

  // Generate a unique username (e.g., basket_<timestamp>_<random number>)
  const uniqueUsername = `basket_${Date.now()}_${Math.floor(
    Math.random() * 10000
  )}`;
  localStorage.setItem(key, uniqueUsername);

  return uniqueUsername;
}

export function removeBasketUserName(): void {
  localStorage.removeItem("basketUsername");
}
