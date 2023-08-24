import React from 'react';
import {useAuth0} from "@auth0/auth0-react";
import {Button} from "@mui/material";

const LoginButton = () => {
    const { loginWithRedirect } = useAuth0();

    const handleLogin = async () => {
        await loginWithRedirect({
            appState: {
                returnTo: "/reservations",
            },
            authorizationParams: {
                prompt: "login",
            },
        });
    };

    return (
        <Button variant="contained" onClick={handleLogin}>
            Log In
        </Button>
    );
};

export default LoginButton;
