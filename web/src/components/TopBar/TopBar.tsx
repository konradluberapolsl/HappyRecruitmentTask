import React from 'react';
import {AppBar, Box, Button, Toolbar, Typography} from "@mui/material";
import {Link, useNavigate} from "react-router-dom";

const TopBar = () => {
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

                    <Button variant="contained" disableElevation onClick={() => navigate("/reservations")}>
                        Your reservations
                    </Button>
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default TopBar;
