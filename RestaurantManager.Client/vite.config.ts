import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

const target =
    process.env.ASPNETCORE_HTTPS_PORT
        ? `https://localhost:${process.env.ASPNETCORE_HTTPS_PORT}`
        : (process.env.ASPNETCORE_URLS?.split(";")[0] ?? "https://localhost:5001");

export default defineConfig({
    plugins: [react()],
    server: {
        port: 5173,
        proxy: {
            "/api": {
                target,
                changeOrigin: true,
                secure: false,
            },
            // optional:
            "/swagger": { target, changeOrigin: true, secure: false },
        },
    },
});