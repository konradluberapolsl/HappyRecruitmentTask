import React from 'react';
import {AppBar, Box, Button, Toolbar, Typography} from "@mui/material";
import {Link, useNavigate} from "react-router-dom";
import {useAuth0} from "@auth0/auth0-react";
import {LogoutButton} from "../Buttons/LogoutButton";

const TopBar = () => {
    const { isAuthenticated } = useAuth0();
    const navigate = useNavigate();

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                    <Typography
                        variant="h5"
                        component="div"
                        sx={{ flexGrow: 1, color: '#FFFFFF' }}
                        onClick={() => navigate('/createReservation')}
                    >
                        TeslaRent
                    </Typography>



                    {isAuthenticated && (
                        <>
                            <Button variant="contained" disableElevation onClick={() => navigate("/reservations")}>
                                Your reservations
                            </Button>
                            <LogoutButton />
                        </>
                    )}

                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default TopBar;
