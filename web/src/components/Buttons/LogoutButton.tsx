import { useAuth0 } from "@auth0/auth0-react";
import React from "react";
import {Button} from "@mui/material";

export const LogoutButton: React.FC = () => {
    const { logout } = useAuth0();

    const handleLogout = () => {
        logout({
            logoutParams: {
                returnTo: window.location.origin,
            },
        });
    };

    return (
        <Button variant="contained" disableElevation  onClick={handleLogout}>
            Log Out
        </Button>
    );
};
