import React, { createContext, useContext, useEffect, useMemo, useState } from "react";
import { authStorage } from "./authStorage";
import type { AuthEndpointResponse, AuthState, AuthUser, LoginRequest, LoginResponse } from "./types";
import { apiFetch } from "../api/apiFetch";

const CLAIMS = {
    id: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
    email: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
    role: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
    name: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
} as const;

type AuthContextValue = AuthState & {
    login: (req: LoginRequest) => Promise<void>;
    logout: () => void;
};

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

type JwtLike = Record<string, any>;

function toArray(v: any): string[] {
    if (!v) return [];
    return Array.isArray(v) ? v : [v];
}

function mapJwtToUser(payload: JwtLike) {
    return {
        id: payload.sub ?? payload[CLAIMS.id] as string,
        email: payload.email ?? payload[CLAIMS.email] as string,
        name: payload.unique_name ?? payload[CLAIMS.name] as string,
        roles: toArray(payload.role ?? payload[CLAIMS.role]),
        exp: payload.exp ? payload.exp * 1000 : 0,
    };
}

function parseJwt<T = any>(token: string): T {
    const parts = token.split(".");

    const base64Url = parts[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const padded = base64.padEnd(base64.length + (4 - (base64.length % 4)) % 4, "=");

    const json = atob(padded);
    debugger

    return JSON.parse(json) as T;
}

async function loginApi(req: LoginRequest): Promise<LoginResponse> {
    const data: AuthEndpointResponse = await apiFetch("api/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(req)
    })
    const payload = parseJwt<AuthUser>(data.accessToken);
    const mappedUser = mapJwtToUser(payload);
    
    // TODO: Call /api/me endpoint and set the user

    // const res = await fetch("https://localhost:5055/api/auth/me", {
    //     credentials: "include",
    // });

    // if (res.status === 401) return null; // not logged in / expired
    // if (!res.ok) throw new Error("Failed to load user");

    // return res.json() as Promise<{ userId: string; email: string; name: string; roles: string[] }>;

    return {
        token: data.accessToken,
        tokenExpiration: data.accessTokenExpiration,
        user: mappedUser,
    };
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [token, setToken] = useState<string | null>(null);
    const [user, setUser] = useState<AuthUser | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    // Load persisted session once on startup
    useEffect(() => {
        const t = authStorage.getToken();
        const u = authStorage.getUser();
        setToken(t);
        setUser(u);
        setIsLoading(false);
    }, []);

    async function login(req: LoginRequest) {
        const result = await loginApi(req);
        debugger
        authStorage.setToken(result.token);
        authStorage.setUser(result.user);
        setToken(result.token);
        setUser(result.user);
    }

    function logout() {
        authStorage.clearAll();
        setToken(null);
        setUser(null);
    }

    const value = useMemo<AuthContextValue>(
        () => ({ token, user, isLoading, login, logout }),
        [token, user, isLoading]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used inside AuthProvider");
    return ctx;
}