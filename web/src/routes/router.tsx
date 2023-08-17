import React from 'react';
import {createBrowserRouter} from "react-router-dom";
import Root from "./Root/Root";
import ErrorPage from "./ErrorPage/ErrorPage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <Root/>,
        errorElement: <ErrorPage/>
    }
]);
