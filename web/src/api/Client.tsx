import axios from "axios";
import data from "../config.json"

export default axios.create({
    baseURL: data.ApiUrl,
    headers: {
        "Content-type": "application/json"
    },
});
