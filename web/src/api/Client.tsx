import axios from "axios";
import data from "../config.json"

const httpClient = axios.create({
    baseURL: data.ApiUrl,
    headers: {
        "Content-type": "application/json"
    },
});

export const addAuthInterceptor = (accessToken: string) => {
    console.log("Adding interceptors")
    httpClient.interceptors.request.use( (config) => {
        if (config && config.headers) {
            config.headers["Authorization"] = `Bearer ${accessToken}`;
        }
        return config;
    });
};

export default httpClient;
