import React, {useEffect, useState} from 'react';
import {ping} from "../../api/controllers/TestClient";
import {useAuth0} from "@auth0/auth0-react";
import {useNavigate} from "react-router-dom";
import LandingPage from "../LandingPage/LandingPage";

const Root = () => {
    const {  isAuthenticated } = useAuth0();

    const navigate = useNavigate();

    // if (!isAuthenticated){
    //     return <LandingPage/>
    // }
    // else {
    //     navigate("/reservations")
    // }

    return (
        <div>Root</div>
    );
};

export default Root;
