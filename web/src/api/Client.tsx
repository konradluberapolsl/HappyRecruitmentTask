import axios from "axios";
import data from "../config.json"

const httpClient = axios.create({
    baseURL: data.ApiUrl,
    headers: {
        "Content-type": "application/json"
    },
});

export const addInterceptor = (getAccessTokenSilently: any) => {
    httpClient.interceptors.request.use(async (config) => {
        const token = await getAccessTokenSilently();
        config.headers.Authorization = `Bearer ${token}`;
        config.baseURL = data.ApiUrl;
        return config;
    });
};

export default httpClient;
