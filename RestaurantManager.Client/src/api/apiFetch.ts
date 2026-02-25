const API_BASE_URL = import.meta.env.VITE_SERVER_BASE_URL as string

const apiFetch = async (endpoint: string, options: RequestInit = { }) => {
  const fullEndpoint = new URL(endpoint, API_BASE_URL).toString();

  const headers = new Headers(options.headers);
  headers.set("Accept", "application/json");

  const res = await fetch(fullEndpoint, { 
    credentials: "include", // include http only cookie
    headers,
    ...options,
   });

  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(`HTTP ${res.status} ${res.statusText} ${text}`);
  }

  return res.json();
}

export {
    apiFetch
};